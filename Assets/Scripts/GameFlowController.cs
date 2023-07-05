using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum Game_states
{
    GameStart = 0, Player1Turn = 1, Player2Turn = 2, GameEnd = 3
};
public class GameFlowController : MonoBehaviour
{
    private Game_states currentState;

    [SerializeField]
    private Button button_Pass;

    [SerializeField]
    private Button button_Restart;

    [SerializeField]
    private TMP_Text text_PlayerTurn;

    private bool is_Player1_done = false;
    private bool is_Player2_done = false;

    private int pass_num;
    private bool is_Passbtn_clicked = false;
    private bool is_Restartbtn_clicked = false;

    private bool is_Castle_surrounded_player1 = false;
    private bool is_Castle_surrounded_player2 = false;

    public Game_states CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public bool Is_Player1_done
    {
        get { return is_Player1_done; }
        set { is_Player1_done = value; }
    }
    public bool Is_Player2_done
    {
        get { return is_Player2_done; }
        set { is_Player2_done = value; }
    }

    public int Pass_num
    {
        get { return pass_num; }
        set { pass_num = value; }
    }
    public bool Is_Castle_surrounded_player1
    {
        get { return is_Castle_surrounded_player1; }
        set { is_Castle_surrounded_player1 = value; }
    }

    public bool Is_Castle_surrounded_player2
    {
        get { return is_Castle_surrounded_player2; }
        set { is_Castle_surrounded_player2 = value; }
    }

    private void Awake()
    {
        button_Pass.onClick.AddListener(() => StartCoroutine(OnClickButton_Pass()));
        button_Restart.onClick.AddListener(() => StartCoroutine(OnClickButton_Restart()));
    }

    private void Start()
    {
        currentState = Game_states.GameStart;
        StartCoroutine(GameFlow());
    }

    private IEnumerator GameFlow()
    {
        var state_switch = new WaitForSeconds(1.0f);
        var state_before = Game_states.GameStart;
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        int winner = 0;

        while (true)
        {
            switch (currentState)
            {
                case Game_states.GameStart:

                    // ���� �ʱ�ȭ
                    gameManager.GetComponent<GameManager>().Player1_territory_num = 0;
                    gameManager.GetComponent<GameManager>().Player2_territory_num = 0;
                    gameManager.Update_territoryNum();


                    text_PlayerTurn.text = $"5�� �� ������ �����մϴ�.";
                    yield return new WaitForSeconds(5.0f); // 5�� ���
                    currentState = Game_states.Player1Turn;
                    winner = 0;
                    pass_num = 0;
                    is_Player1_done = false;
                    is_Player2_done = false;
                    is_Castle_surrounded_player1 = false;
                    is_Castle_surrounded_player2 = false;

                    break;

                case Game_states.Player1Turn:
                    text_PlayerTurn.text = $"�÷��̾� 1�� ���Դϴ�.";
                    button_Pass.gameObject.SetActive(true);

                    while (true)
                    {
                        // �÷��̾� 1�� �Ͽ� �÷��̾� 2 ���� �ѷ��ο������� �÷��̾� 1�� �¸�
                        if (is_Castle_surrounded_player2)
                        {
                            state_before = Game_states.Player1Turn;
                            currentState = Game_states.GameEnd;
                            break;
                        }

                        // �÷��̾� 1�� �Ͽ� �÷��̾� 2 ���� �ѷ������� ����, �÷��̾� 1�� ���� �ѷ��ο��ٸ� �÷��̾� 2�� �¸�
                        if (is_Castle_surrounded_player1)
                        {
                            state_before = Game_states.Player2Turn;
                            currentState = Game_states.GameEnd;
                            break;
                        }

                        if (is_Passbtn_clicked)
                        {
                            pass_num += 1;
                            currentState = Game_states.Player2Turn;
                            is_Player1_done = false;
                            is_Passbtn_clicked = false;

                            if (pass_num == 2)
                            {
                                currentState = Game_states.GameEnd;
                            }

                            break;
                        }

                        if (is_Player1_done)
                        {
                            currentState = Game_states.Player2Turn;
                            pass_num = 0;
                            is_Player1_done = false;
                            break;
                        }

                        yield return null; // 1������ ���
                    }
                    break;

                case Game_states.Player2Turn:
                    text_PlayerTurn.text = $"�÷��̾� 2�� ���Դϴ�.";
                    button_Pass.gameObject.SetActive(true);

                    while (true)
                    {

                        // �÷��̾� 2�� �Ͽ� �÷��̾� 1���� �ѷ��ο������� �÷��̾� 2�� �¸�
                        if (is_Castle_surrounded_player1)
                        {
                            state_before = Game_states.Player2Turn; // �÷��̾� 2�� �¸�
                            currentState = Game_states.GameEnd;
                            break;
                        }

                        // �÷��̾� 2�� �Ͽ� �÷��̾� 1 ���� �ѷ������� ����, �÷��̾� 2�� ���� �ѷ��ο��ٸ� �÷��̾� 1�� �¸�
                        if (is_Castle_surrounded_player2)
                        {
                            state_before = Game_states.Player1Turn; // �÷��̾� 1�� �¸�
                            currentState = Game_states.GameEnd;
                            break;
                        }

                        if (is_Passbtn_clicked)
                        {
                            pass_num += 1;
                            currentState = Game_states.Player1Turn;
                            is_Player2_done = false;
                            is_Passbtn_clicked = false;

                            if (pass_num == 2)
                            {
                                currentState = Game_states.GameEnd;
                            }

                            break;
                        }

                        if (is_Player2_done)
                        {
                            currentState = Game_states.Player1Turn;
                            pass_num = 0;
                            is_Player2_done = false;
                            break;
                        }

                        yield return null; // 1������ ���
                    }
                    break;

                case Game_states.GameEnd:
                    
                    if(pass_num == 2)
                    {
                        // �� �÷��̾ �������� �н��� �Ͽ��� ���� ��� �� ���� �̰���� Ȯ��.
                        double territoryNum1 = gameManager.Player1_territory_num;
                        double territoryNum2 = gameManager.Player2_territory_num + 2.5;

                        if (territoryNum1 > territoryNum2)
                        {
                            winner = 1;
                        }
                        else if (territoryNum1 < territoryNum2)
                        {
                            winner = 2;
                        }

                    }
                    else if(is_Castle_surrounded_player1 || is_Castle_surrounded_player2)
                    {
                        // ���� �ѷ��ο��� �¸��ϴ� ���
                        if(state_before == Game_states.Player1Turn)
                        {
                            winner = 1;
                        } 
                        else if(state_before == Game_states.Player2Turn)
                        {
                            winner = 2;
                        }
                    }

                    text_PlayerTurn.text = $"���� ���� ��... �Դϴ�.";
                    yield return state_switch;

                    if (winner == 1)
                    {
                        text_PlayerTurn.text = $"�÷��̾� 1�� �¸��Դϴ�.";
                    } 
                    else if(winner == 2)
                    {
                        text_PlayerTurn.text = $"�÷��̾� 2�� �¸��Դϴ�.";
                    } 
                    else
                    {
                        text_PlayerTurn.text = $"���º� �Դϴ�.";
                    }

                    button_Restart.gameObject.SetActive(true);
                    while (true)
                    {
                        if (is_Restartbtn_clicked)
                        {
                            is_Restartbtn_clicked = false;
                            break;
                        }

                        yield return null; // 1������ ���
                    }

                    currentState = Game_states.GameStart;
                    GameBoardManager.Clear_gameBoard(gameManager.GameBoard);

                    break;
            }
        }
    }
    private IEnumerator OnClickButton_Pass()
    {
        if (currentState == Game_states.Player1Turn || currentState == Game_states.Player2Turn)
        {
            is_Passbtn_clicked = true;
            button_Pass.gameObject.SetActive(false);
        }
        yield return null; // 1������ ���
    }

    private IEnumerator OnClickButton_Restart()
    {
        if(currentState == Game_states.GameEnd)
        {
            is_Restartbtn_clicked = true;
            button_Restart.gameObject.SetActive(false);
        }
        yield return null; // 1������ ���
    }

}
