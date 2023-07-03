using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardManager : MonoBehaviour
{
    public static readonly (int, int)[] directions = new (int, int)[]
    {
        (-1, 0), // 위쪽
        (1, 0),  // 아래쪽
        (0, -1), // 왼쪽
        (0, 1)   // 오른쪽
    };

    public static List<GameObject> Clear_gameBoard(List<GameObject> gameBoard)
    {
        // 지역 변수 : GameManager가 가진 (Tile 클래스를 컴포넌트로 가진 GameObject)들을 초기화 시키기 위함.
        Tile tileComponent;

        // Castle 태그의 모든 플레이어 성 삭제.
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
                tileComponent.Tile_state = Tile_states.blank; // 모든 게임판의 상태를 빈칸으로 만든다.
            }
        }

        return gameBoard;
    }

    public static List<GameObject> Calculate_gameBoard(List<GameObject> gameBoard)
    { 
        // 9x9 내부 게임판을 읽을 때 쓸 변수
        int current_index = 0;

        // BFS 탐색때 쓸 변수
        List<(int, int)> now_territory = new();
        Queue<(int, int)> queue = new();
        (int,int) now;
        (int,int) next;
        bool meet_wall_up, meet_wall_right, meet_wall_down, meet_wall_left;
        bool meet_blue_castle, meet_red_castle, meet_middle_castle;

        // 11 x 11 판 제작  9x9 판을 둘러싼 가장자리 영역 까지 표현
        Tile_states[,] gameBoard_states = new Tile_states[11,11];
        bool[,] visit = new bool[11,11];

        for(int i = 0; i < 11; i++)
        {
            //  11x11 의 꼭짓점 자리 4칸은 어떤 상태던지 상관없음. 9x9 내부 칸에서 도달이 불가능하므로
            //  내부 9x9 칸에서 BFS로 도달 가능한 가장자리 칸의 상태 초기화가 중요
            gameBoard_states[0,i] = Tile_states.wall_up;
            gameBoard_states[i,0] = Tile_states.wall_left;
            gameBoard_states[i,10] = Tile_states.wall_right;
            gameBoard_states[10,i] = Tile_states.wall_down;
        }
        
        // 내부 9x9 판의 상태를 가져온다.
        for(int i = 1; i <= 9; i++)
        {
            for(int j = 1; j <= 9; j++)
            {
                gameBoard_states[i,j] = gameBoard[current_index++].GetComponent<Tile>().Tile_state;
            }
        }

        // 붙어있는 빈칸 영역을 찾는다.
        // 해당 빈칸 영역이 가장자리 4칸을 다 써서 둘러 싸여져 있는가? 영역 인정 x
        // 해당 영역이 파랑/ 빨강 성으로만 둘러 싸여져 잇는가? 파랑 성 영역일 경우 빨강성 만나거나, 빨강 성 영역일 경우 파랑성 만나면 영역 인정 x 

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
                    // BFS 를 통해 붙어있는 빈칸 영역들이 둘러싸인 상태를 확인한다.
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

                    // BFS 탐색을 통해 구한 빈칸 영역을 계산한다.
                    bool isTerritory = true;

                    if ((meet_blue_castle && meet_red_castle) || (!meet_blue_castle && !meet_red_castle))
                        isTerritory = false;

                    else if (meet_wall_down && meet_wall_left && meet_wall_right && meet_wall_up)
                        isTerritory = false;


                    if(isTerritory)
                    {
                        // 영역 처리
                        for(int ground = 0; ground < now_territory.Count; ground++)
                        {
                            gameBoard_states[now_territory[ground].Item1, now_territory[ground].Item2] = (meet_blue_castle ? Tile_states.territory_player1 : Tile_states.territory_player2);
                        }

                    }

                    now_territory.Clear();

                }
            }
        }

        // 현재 게임판 상태를 gameBoard 객체에 입힌다.
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

        // 테스트용 LOG 
        
        for(int i = 1; i <= 9; i++)
        {
            Debug.Log($"{gameBoard_states[i, 1]}{gameBoard_states[i, 2]}{gameBoard_states[i, 3]}{gameBoard_states[i, 4]}{gameBoard_states[i, 5]}{gameBoard_states[i, 6]}{gameBoard_states[i, 7]}{gameBoard_states[i, 8]}{gameBoard_states[i, 9]}");
        }
        

        return gameBoard;
    }

    public static bool Check_castle_surrounded(List<GameObject> gameBoard, Tile_states tile_State)
    {
        // 9x9 내부 게임판을 읽을 때 쓸 변수
        int current_index = 0;


        // 현재 어떤 성이 둘러싸인것을 확인 할 것인가?
        // 플레이어 1의 턴 일 경우, 플레이어 2의 성이 둘러싸였는지 탐색 , 플레이어 2의 턴 일 경우, 플레이어 1의 성이 둘러싸였는지 탐색 후 게임 종료 조건인지 확인
        var now_search = Tile_states.castle_player1;

        now_search = tile_State;

        // BFS 탐색때 쓸 변수
        List<(int, int)> now_territory = new();
        Queue<(int, int)> queue = new();
        (int, int) now;
        (int, int) next;
        bool meet_blue_castle, meet_red_castle,meet_red_territory, meet_blue_territory, meet_blank;

        // 11 x 11 판 제작  9x9 판을 둘러싼 가장자리 영역 까지 표현
        Tile_states[,] gameBoard_states = new Tile_states[11, 11];
        bool[,] visit = new bool[11, 11];

        for (int i = 0; i < 11; i++)
        {
            //  11x11 의 꼭짓점 자리 4칸은 어떤 상태던지 상관없음. 9x9 내부 칸에서 도달이 불가능하므로
            //  내부 9x9 칸에서 BFS로 도달 가능한 가장자리 칸의 상태 초기화가 중요
            gameBoard_states[0, i] = Tile_states.wall_up;
            gameBoard_states[i, 0] = Tile_states.wall_left;
            gameBoard_states[i, 10] = Tile_states.wall_right;
            gameBoard_states[10, i] = Tile_states.wall_down;
        }

        // 내부 9x9 판의 상태를 가져온다.
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
                    // BFS 를 통해 붙어있는 빈칸 영역들이 둘러싸인 상태를 확인한다.
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

                    // BFS 탐색을 통해 구한 탐색 영역을 통해 둘러싸였는지 확인한다.
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
