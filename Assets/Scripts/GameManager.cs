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

    // 플레이어 1,2의 영토 수
    private double player1_territory_num;
    private double player2_territory_num;

    private int player_turn; // 현재 플레이어 차례
    private int player_pass_num; // 플레이어가 연속으로 패스하였는지 체크. (두 플레이어가 연속해서 패스 하면 게임 종료) 

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
