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
    private Button   button_Pass;

    [SerializeField]
    private TMP_Text text_PlayerTurn;

    private bool     is_Player1_done = false;
    private bool     is_Player2_done = false;

    private int      pass_num;
    private bool     is_Passbtn_clicked = false;

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
        get { return pass_num;}
        set { pass_num = value; }
    }

    private void Awake()
    {
        button_Pass.onClick.AddListener(() => StartCoroutine(OnClickButton_Pass()));
    }

    private void Start()
    {
        currentState = Game_states.GameStart;
        StartCoroutine(GameFlow());
    }

    private IEnumerator GameFlow()
    {
        var state_switch = new WaitForSeconds(1.0f);

        currentState = Game_states.GameStart;

        while (currentState != Game_states.GameEnd)
        {
            switch (currentState)
            {
                case Game_states.GameStart:
                    text_PlayerTurn.text = $"5초 후 게임을 시작합니다.";
                    yield return new WaitForSeconds(5.0f); // 1프레임 대기
                    currentState = Game_states.Player1Turn;
                    pass_num = 0;

                    break;

                case Game_states.Player1Turn:
                    button_Pass.gameObject.SetActive(true);
                    text_PlayerTurn.text = $"플레이어 1의 턴입니다.";

                    while (true)
                    {
                        if (is_Passbtn_clicked)
                        {
                            currentState = Game_states.Player2Turn;
                            is_Player1_done = false;
                            is_Passbtn_clicked = false;
                            button_Pass.gameObject.SetActive(false);

                            if (pass_num == 2)
                            {
                                currentState = Game_states.GameEnd;
                                text_PlayerTurn.text = $"게임을 종료합니다.";
                            }
                            yield return state_switch;
                            break;
                        }

                        if (is_Player1_done)
                        {
                            currentState = Game_states.Player2Turn;
                            pass_num = 0;
                            is_Player1_done = false;
                            yield return state_switch;
                            break;
                        }

                        yield return null; // 1프레임 대기
                    }
                    break;

                case Game_states.Player2Turn:
                    button_Pass.gameObject.SetActive(true);
                    text_PlayerTurn.text = $"플레이어 2의 턴입니다.";

                    while (true)
                    {
                        if (is_Passbtn_clicked)
                        {
                            currentState = Game_states.Player1Turn;
                            is_Player2_done = false;
                            is_Passbtn_clicked = false;
                            button_Pass.gameObject.SetActive(false);

                            if (pass_num == 2)
                            {
                                currentState = Game_states.GameEnd;
                                text_PlayerTurn.text = $"게임을 종료합니다.";
                            }
                            yield return state_switch;
                            break;
                        }

                        if (is_Player2_done)
                        {
                            currentState = Game_states.Player1Turn;
                            pass_num = 0;
                            is_Player2_done = false;
                            yield return state_switch;
                            break;
                        }

                        yield return null; // 1프레임 대기
                    }
                    break;

                case Game_states.GameEnd:
                    break;
            }
        }
    }
    private IEnumerator OnClickButton_Pass()
    {
        if(currentState == Game_states.Player1Turn || currentState == Game_states.Player2Turn)
        {
            is_Passbtn_clicked = true;
            pass_num += 1;
            button_Pass.gameObject.SetActive(false);
        }
        yield return null; // 1프레임 대기
    }
}
