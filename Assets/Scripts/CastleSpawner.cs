using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleSpawner : MonoBehaviour
{
    private readonly float X_DIFF = -200f;
    private readonly float Y_DIFF = -1040f;

    [SerializeField]
    private GameObject panel_GameBoard;

    [SerializeField]
    private GameObject player1_castlePrefab;

    [SerializeField]
    private GameObject player2_castlePrefab;

    public bool SpawnCastle(Vector3 vector3, Game_states game_state)
    {
        // 게임판과 castle 생성 위치 맞추기.
        vector3.x += X_DIFF;
        vector3.y += Y_DIFF;

        GameObject newCastle;
        if (game_state == Game_states.Player1Turn)
        {
            newCastle = Instantiate(player1_castlePrefab, vector3, Quaternion.identity);
            newCastle.transform.SetParent(panel_GameBoard.transform, false);
            newCastle.transform.SetAsLastSibling();
            return true;
        }

        else if (game_state == Game_states.Player2Turn)
        {
            newCastle = Instantiate(player2_castlePrefab, vector3, Quaternion.identity);
            newCastle.transform.SetParent(panel_GameBoard.transform, false);
            newCastle.transform.SetAsLastSibling();
            return true;
        }
        return false;
    }

}
