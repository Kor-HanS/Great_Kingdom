using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{   
    // 필드
    private DateTime _sessionStartTime;
    private DateTime _sessionEndTime;
    
    private double   player1_territory_num;
    private double   player2_territory_num;

    [SerializeField]
    private GameFlowController gameFlowController;

    [SerializeField]
    private List<GameObject> gameBoard;

    [SerializeField]
    private CastleSpawner castleSpawner;

    [SerializeField]
    private TMP_Text text_Player1_territory;

    [SerializeField]
    private TMP_Text text_Player2_territory;

    // 프로퍼티
    public double Player1_territory_num { 
        get { return player1_territory_num; } 
        set { player1_territory_num = value; }
    }
    public double Player2_territory_num { 
        get { return player2_territory_num; } 
        set { player2_territory_num = value; }
    }

    public List<GameObject> GameBoard { 
        get { return gameBoard; } 
        set { gameBoard = value; }
    }
    private void Start()
    {
        int count = 0;
        while (count < 81)
        {
            int index = count;
            gameBoard[index].GetComponent<Button>().onClick.AddListener(() => OnClickButton_gameboard(index));
            count += 1;
        }

        GameBoardManager.Clear_gameBoard(gameBoard);


        for(int i = 0; i < 81; i++)
        {
            Debug.Log($"{i}:{gameBoard[i].GetComponent<Tile>().Tile_state}");
        }

        _sessionStartTime = DateTime.Now;

        Debug.Log("Game Session started @: " + DateTime.Now);
    }

    private void OnApplicationQuit()
    {
        _sessionEndTime = DateTime.Now;

        TimeSpan timediff = _sessionEndTime.Subtract(_sessionStartTime);

        Debug.Log("Game Session ended @: " + DateTime.Now);
        Debug.Log("Game session lasted: " + timediff);
    }

    private void OnClickButton_gameboard(int num)
    {
        if (gameFlowController.Is_Player1_done || gameFlowController.Is_Player2_done)
            return;

        if (num == 40)
            return;

        Debug.Log(num);
        bool isSuccess = false;
        var tileComponenet = GameBoard[num].GetComponent<Tile>();

        if(tileComponenet.Tile_state == Tile_states.blank)
        {
            isSuccess = castleSpawner.SpawnCastle(GameBoard[num].transform.position, gameFlowController.CurrentState);
        }

        if(isSuccess)
        {
            if(gameFlowController.CurrentState == Game_states.Player1Turn)
            {
                tileComponenet.Tile_state = Tile_states.castle_player1;
                gameBoard[num].GetComponent<Image>().color = Color.blue;
                gameFlowController.Is_Player1_done = true;
            }
            else if (gameFlowController.CurrentState == Game_states.Player2Turn) 
            {
                tileComponenet.Tile_state = Tile_states.castle_player2;
                gameBoard[num].GetComponent<Image>().color = Color.red;
                gameFlowController.Is_Player2_done = true;
            }
            gameBoard = GameBoardManager.Calculate_gameBoard(gameBoard);
            Update_territoryNum();

            gameFlowController.Is_Castle_surrounded = GameBoardManager.Check_castle_surrounded(gameBoard, gameFlowController.CurrentState);
        }
    }

    private void Update_territoryNum()
    {
        player1_territory_num = 0;
        player2_territory_num = 0;

        for(int i = 0; i < 81; i++)
        {
            var tile_state = gameBoard[i].GetComponent<Tile>().Tile_state;

            if(tile_state == Tile_states.territory_player1)
            {
                player1_territory_num++;
            } 
            else if(tile_state == Tile_states.territory_player2)
            {
                player2_territory_num++;
            }
        }

        text_Player1_territory.text = $"{player1_territory_num}";
        text_Player2_territory.text = $"{player2_territory_num}\n+2.5";
    }

}
