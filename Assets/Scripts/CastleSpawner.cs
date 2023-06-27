using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject panel_GameBoard;

    [SerializeField]
    private GameObject player1_castlePrefab;

    [SerializeField]
    private GameObject player2_castlePrefab;

    public bool SpawnCastle(Vector3 vector3, Game_states game_state)
    {
        vector3.x -= 200f;
        vector3.y -= 1040f;

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
