using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tile_states
{
    blank = 0,
    castle_player1 = 1,
    castle_player2 = 2,
    wall_up = 3,
    wall_right =4,
    wall_down = 5,
    wall_left = 6,
    castle_middle = 7,
    territory_player1 = 8,
    territory_player2 = 9
}

public class Tile : MonoBehaviour
{
    // ���� Ÿ���� ���´� ��Ѱ�?
    private Tile_states tile_state;

    // ���� �ǿ� ���� �Ͼ� , ���� , �Ķ� 

    public Tile_states Tile_state
    {
        get { return tile_state; }
        set { tile_state = value; }
    }

}
