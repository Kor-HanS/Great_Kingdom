# Great_Kingdom
게임 클라이언트 개발 (김 한승) / 게임 서버 API 개발 (김 진수) 

- 메인 메뉴 UI
![image](https://github.com/Kor-HanS/Great_Kingdom/assets/99121615/34050b75-8dc6-407f-b72b-a5ddeda27a3d)


![image](https://github.com/Kor-HanS/Great_Kingdom/assets/99121615/e03ab13b-5f18-41e6-a950-5fb39dda2ffe)
![image](https://github.com/Kor-HanS/Great_Kingdom/assets/99121615/2864cec6-9db4-4267-90ab-981336494da1)

- 플레이어 1의 성이 둘러싸인 상황 / 둘러싸이지 않은 상황(빈칸을 포함하여 둘러쌈)
![image](https://github.com/Kor-HanS/Great_Kingdom/assets/99121615/886ca167-fa4c-4482-a1ff-41453563add6)
![image](https://github.com/Kor-HanS/Great_Kingdom/assets/99121615/a3ee60a3-c431-4f92-9799-8d79bc0bf09b)

게임 클라이언트 개발
0. 게임 씬 구성(UI 개발)
1. 클래스 작성
- GameManager.cs (게임 매니저 클래스)
- SingleTon.cs(게임 매니저가 해당 클래스 상속)
- CastleSpawner.cs (게임에 해당 플레이어의 차례가 되고, 플레이어와 상호작용하여 게임판에 해당 플레이어의 성 소환)
- GameFlowController.cs (게임에 흐름을 제어)
- GameBoardManager.cs (게임 보드 와 관련된 static 메소드 클래스)
  - public static List<GameObject> Clear_gameBoard(List<GameObject> gameBoard)
  - public static List<GameObject> Calculate_gameBoard(List<GameObject> gameBoard)
  - public static bool Check_Castle_Surrounded(List<GameObject> gameBoard, Game_States game_state) // 현재 턴에 둘러싸이게된 성이 존재하는가?
2. 네트워크 구현(멀티게임)


Q. 게임판에 게임말을 놓는 동작에 구현
- 이전에 헬포커 디펜스를 통해서는 RayCastHit을 통해서 구현하였으나, 버튼 UI를 통해
  OnClick.AddListenr로 버튼 클릭시 리스너 함수를 달아주어 구현하였습니다.
- 타일 클래스에서 정의된 상태중 0번 blank 상태일 경우만 게임판에 게임말을 놓을 수 있습니다.

Q. 게임판에 게임말을 놓은 후, 영토 계산 알고리즘
- Tile 클래스에서 상태를 10가지로 정의 했습니다.(빈칸, 파랑성, 빨강성, 위쪽벽, 오른쪽벽, 아랫벽, 왼쪽벽, 중간성, 파랑 땅, 빨강땅)
- BFS 탐색을 통해 빈칸 영역을 구하고, 해당 빈칸 영역을 구하면서 만난 벽을 체크합니다.
이때, 4가지 가장자리 벽을 모두 만났거나, 파랑/빨강 성을 동시에 만났거나, 파랑/빨강 성을 만나지 못했을 경우 영토로 인정하지 않습니다.
(4가지 가장자리를 낀 경우, 내 영토에 다른 성이 끼어있거나, 내 성과 다른성이 둘러싼 영토)

Q. 게임판에 게임말을 놓은 후, 성이 다른 플레이어 성에의해 점령 됬을 경우 알고리즘
- 사실 위에 영토계산 함수를 거의 그대로 사용하였다. 다른 점은, 플레이어1의 턴인지 플레이어 2의 턴인지에 따라 탐색을 통해 플레이어2의 성 영역 / 플레이어1의 성 영역을 구할 것인지에 차이 였다.
- 예를들어 플레이어 1의 턴이 종료되기 직전, 플레이어 2의 성이 플레이어 1의 성들로 둘러싸였는지 확인하여야 하므로,
플레이어 1의 턴이 종료되기 전, 붙어있는 플레이어 2 성의 영역들을 탐색하여서 플레이어 1의 성과 벽(위/아래/오른쪽/왼쪽/중간)을 사용하여 둘러싸였는지 체크한다.
이때, 빈칸 / 플레이어1의 영토 / 플레이어 2의 영토를 성의 영역을 구하는 도중 만났다면 둘러싸여진 판정이 아니다.
