using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public enum Game_states{
        GameStart = 0, Player1Turn = 1, Player2Turn = 2, GameEnd = 3
    };
    
    private DateTime _sessionStartTime;
    private DateTime _sessionEndTime;

    // �÷��̾� 1,2�� ���� ��
    private double player1_territory_num;
    private double player2_territory_num;

    private int player_turn; // ���� �÷��̾� ����
    private int player_pass_num; // �÷��̾ �������� �н��Ͽ����� üũ. (�� �÷��̾ �����ؼ� �н� �ϸ� ���� ����) 

    [SerializeField]
    private List<GameObject> gameBoard;

    public double Player1_territory_num { get; set; }
    public double Player2_territory_num { get; set; }

    public int Player_turn { get; set; }
    public int Player_pass_num { get; set; }

    public List<GameObject> GameBoard { get; set; }

    private void Start()
    {
        _sessionStartTime = DateTime.Now;

        Debug.Log("Game Session started @: " + DateTime.Now);
    }

    void OnApplicationQuit()
    {
        _sessionEndTime = DateTime.Now;

        TimeSpan timediff = _sessionEndTime.Subtract(_sessionStartTime);

        Debug.Log("Game Session ended @: " + DateTime.Now);
        Debug.Log("Game session lasted: " + timediff);
    }

}
