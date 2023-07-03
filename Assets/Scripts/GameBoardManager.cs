using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardManager : MonoBehaviour
{
    public static readonly (int, int)[] directions = new (int, int)[]
    {
        (-1, 0), // ����
        (1, 0),  // �Ʒ���
        (0, -1), // ����
        (0, 1)   // ������
    };

    public static List<GameObject> Clear_gameBoard(List<GameObject> gameBoard)
    {
        // ���� ���� : GameManager�� ���� (Tile Ŭ������ ������Ʈ�� ���� GameObject)���� �ʱ�ȭ ��Ű�� ����.
        Tile tileComponent;

        // Castle �±��� ��� �÷��̾� �� ����.
        GameObject[] castles = GameObject.FindGameObjectsWithTag("Castle");

        for(int j = 0; j < castles.Length; j++)
        {
            Destroy(castles[j]);
        }


        for(int i = 0; i < 81; i++)
        {
            tileComponent = gameBoard[i].GetComponent<Tile>();

            if(i == 40)
            {
                tileComponent.Tile_state = Tile_states.castle_middle;
            } 
            else
            {
                gameBoard[i].GetComponent<Image>().color = Color.white;
                tileComponent.Tile_state = Tile_states.blank; // ��� �������� ���¸� ��ĭ���� �����.
            }
        }

        return gameBoard;
    }

    public static List<GameObject> Calculate_gameBoard(List<GameObject> gameBoard)
    { 
        // 9x9 ���� �������� ���� �� �� ����
        int current_index = 0;

        // BFS Ž���� �� ����
        List<(int, int)> now_territory = new();
        Queue<(int, int)> queue = new();
        (int,int) now;
        (int,int) next;
        bool meet_wall_up, meet_wall_right, meet_wall_down, meet_wall_left;
        bool meet_blue_castle, meet_red_castle, meet_middle_castle;

        // 11 x 11 �� ����  9x9 ���� �ѷ��� �����ڸ� ���� ���� ǥ��
        Tile_states[,] gameBoard_states = new Tile_states[11,11];
        bool[,] visit = new bool[11,11];

        for(int i = 0; i < 11; i++)
        {
            //  11x11 �� ������ �ڸ� 4ĭ�� � ���´��� �������. 9x9 ���� ĭ���� ������ �Ұ����ϹǷ�
            //  ���� 9x9 ĭ���� BFS�� ���� ������ �����ڸ� ĭ�� ���� �ʱ�ȭ�� �߿�
            gameBoard_states[0,i] = Tile_states.wall_up;
            gameBoard_states[i,0] = Tile_states.wall_left;
            gameBoard_states[i,10] = Tile_states.wall_right;
            gameBoard_states[10,i] = Tile_states.wall_down;
        }
        
        // ���� 9x9 ���� ���¸� �����´�.
        for(int i = 1; i <= 9; i++)
        {
            for(int j = 1; j <= 9; j++)
            {
                gameBoard_states[i,j] = gameBoard[current_index++].GetComponent<Tile>().Tile_state;
            }
        }

        // �پ��ִ� ��ĭ ������ ã�´�.
        // �ش� ��ĭ ������ �����ڸ� 4ĭ�� �� �Ἥ �ѷ� �ο��� �ִ°�? ���� ���� x
        // �ش� ������ �Ķ�/ ���� �����θ� �ѷ� �ο��� �մ°�? �Ķ� �� ������ ��� ������ �����ų�, ���� �� ������ ��� �Ķ��� ������ ���� ���� x 

        for(int i = 1; i <= 9; i++)
        {
            for(int j = 1; j <= 9; j++)
            {
                if (gameBoard_states[i,j] != Tile_states.blank)
                    continue;
                else if (visit[i,j])
                    continue;
                else
                {
                    meet_wall_up = false;
                    meet_wall_right = false;
                    meet_wall_down = false;
                    meet_wall_left = false;
                    meet_blue_castle = false;
                    meet_red_castle = false;
                    meet_middle_castle = false;

                    now_territory.Add((i, j));
                    queue.Enqueue((i,j));
                    // BFS �� ���� �پ��ִ� ��ĭ �������� �ѷ����� ���¸� Ȯ���Ѵ�.
                    while(queue.Count != 0)
                    {
                        now = queue.Dequeue();

                        for(int k = 0; k < 4; k++)
                        {
                            next = (now.Item1 + directions[k].Item1, now.Item2 + directions[k].Item2);

                            if (visit[next.Item1, next.Item2])
                                continue;

                            if (gameBoard_states[next.Item1, next.Item2] == Tile_states.blank)
                            {
                                queue.Enqueue(next);
                                now_territory.Add(next);
                                visit[next.Item1, next.Item2] = true;
                            }
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.castle_player1)
                                meet_blue_castle = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.castle_player2)
                                meet_red_castle = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.castle_middle)
                                meet_middle_castle = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.wall_down)
                                meet_wall_down = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.wall_left)
                                meet_wall_left = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.wall_right)
                                meet_wall_right = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.wall_up)
                                meet_wall_up = true;
                        }
                    }

                    // BFS Ž���� ���� ���� ��ĭ ������ ����Ѵ�.
                    bool isTerritory = true;

                    if ((meet_blue_castle && meet_red_castle) || (!meet_blue_castle && !meet_red_castle))
                        isTerritory = false;

                    else if (meet_wall_down && meet_wall_left && meet_wall_right && meet_wall_up)
                        isTerritory = false;


                    if(isTerritory)
                    {
                        // ���� ó��
                        for(int ground = 0; ground < now_territory.Count; ground++)
                        {
                            gameBoard_states[now_territory[ground].Item1, now_territory[ground].Item2] = (meet_blue_castle ? Tile_states.territory_player1 : Tile_states.territory_player2);
                        }

                    }

                    now_territory.Clear();

                }
            }
        }

        // ���� ������ ���¸� gameBoard ��ü�� ������.
        current_index = 0;
        for(int i = 1; i <= 9; i++)
        {
            for(int j = 1; j <= 9; j++)
            {
                var before_states = gameBoard[current_index].GetComponent<Tile>().Tile_state;

                if(before_states != gameBoard_states[i,j])
                {
                    gameBoard[current_index].GetComponent<Tile>().Tile_state = gameBoard_states[i, j];

                    if(gameBoard_states[i,j] == Tile_states.territory_player1)
                    {
                        gameBoard[current_index].GetComponent<Image>().color = Color.blue;
                    }

                    if (gameBoard_states[i, j] == Tile_states.territory_player2)
                    {
                        gameBoard[current_index].GetComponent<Image>().color = Color.red;
                    }
                }
                current_index++;
            }
        }

        // �׽�Ʈ�� LOG 
        
        for(int i = 1; i <= 9; i++)
        {
            Debug.Log($"{gameBoard_states[i, 1]}{gameBoard_states[i, 2]}{gameBoard_states[i, 3]}{gameBoard_states[i, 4]}{gameBoard_states[i, 5]}{gameBoard_states[i, 6]}{gameBoard_states[i, 7]}{gameBoard_states[i, 8]}{gameBoard_states[i, 9]}");
        }
        

        return gameBoard;
    }

    public static bool Check_castle_surrounded(List<GameObject> gameBoard, Tile_states tile_State)
    {
        // 9x9 ���� �������� ���� �� �� ����
        int current_index = 0;


        // ���� � ���� �ѷ����ΰ��� Ȯ�� �� ���ΰ�?
        // �÷��̾� 1�� �� �� ���, �÷��̾� 2�� ���� �ѷ��ο����� Ž�� , �÷��̾� 2�� �� �� ���, �÷��̾� 1�� ���� �ѷ��ο����� Ž�� �� ���� ���� �������� Ȯ��
        var now_search = Tile_states.castle_player1;

        now_search = tile_State;

        // BFS Ž���� �� ����
        List<(int, int)> now_territory = new();
        Queue<(int, int)> queue = new();
        (int, int) now;
        (int, int) next;
        bool meet_blue_castle, meet_red_castle,meet_red_territory, meet_blue_territory, meet_blank;

        // 11 x 11 �� ����  9x9 ���� �ѷ��� �����ڸ� ���� ���� ǥ��
        Tile_states[,] gameBoard_states = new Tile_states[11, 11];
        bool[,] visit = new bool[11, 11];

        for (int i = 0; i < 11; i++)
        {
            //  11x11 �� ������ �ڸ� 4ĭ�� � ���´��� �������. 9x9 ���� ĭ���� ������ �Ұ����ϹǷ�
            //  ���� 9x9 ĭ���� BFS�� ���� ������ �����ڸ� ĭ�� ���� �ʱ�ȭ�� �߿�
            gameBoard_states[0, i] = Tile_states.wall_up;
            gameBoard_states[i, 0] = Tile_states.wall_left;
            gameBoard_states[i, 10] = Tile_states.wall_right;
            gameBoard_states[10, i] = Tile_states.wall_down;
        }

        // ���� 9x9 ���� ���¸� �����´�.
        for (int i = 1; i <= 9; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                gameBoard_states[i, j] = gameBoard[current_index++].GetComponent<Tile>().Tile_state;
            }
        }


        for (int i = 1; i <= 9; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                if (gameBoard_states[i, j] != now_search)
                    continue;
                else if (visit[i, j])
                    continue;
                else
                {
                    meet_blue_castle = false;
                    meet_red_castle = false;
                    meet_blue_territory = false;
                    meet_red_territory = false;
                    meet_blank = false;

                    now_territory.Add((i, j));
                    queue.Enqueue((i, j));
                    // BFS �� ���� �پ��ִ� ��ĭ �������� �ѷ����� ���¸� Ȯ���Ѵ�.
                    while (queue.Count != 0)
                    {
                        now = queue.Dequeue();

                        for (int k = 0; k < 4; k++)
                        {
                            next = (now.Item1 + directions[k].Item1, now.Item2 + directions[k].Item2);

                            if (visit[next.Item1, next.Item2])
                                continue;

                            if (gameBoard_states[next.Item1, next.Item2] == now_search)
                            {
                                queue.Enqueue(next);
                                now_territory.Add(next);
                                visit[next.Item1, next.Item2] = true;
                            }
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.castle_player1)
                                meet_blue_castle = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.castle_player2)
                                meet_red_castle = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.territory_player1)
                                meet_blue_territory = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.territory_player2)
                                meet_red_territory = true;
                            else if (gameBoard_states[next.Item1, next.Item2] == Tile_states.blank)
                                meet_blank = true;
                        }
                    }

                    // BFS Ž���� ���� ���� Ž�� ������ ���� �ѷ��ο����� Ȯ���Ѵ�.
                    bool isCastleSurrounded = true;

                    if (meet_blank || meet_blue_territory || meet_red_territory)
                        isCastleSurrounded = false;

                    if (now_search == Tile_states.castle_player1 && !meet_red_castle)
                        isCastleSurrounded = false;

                    if (now_search == Tile_states.castle_player2 && !meet_blue_castle)
                        isCastleSurrounded = false;

                    if (isCastleSurrounded)
                    {
                        return true;
                    }

                    now_territory.Clear();

                }
            }
        }
        return false;
    }

}
