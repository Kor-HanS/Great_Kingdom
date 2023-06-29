# Great_Kingdom
게임 클라이언트 개발 (김 한승) / 게임 서버 API 개발 (김 진수) 

![image](https://github.com/Kor-HanS/Great_Kingdom/assets/99121615/e03ab13b-5f18-41e6-a950-5fb39dda2ffe)
![image](https://github.com/Kor-HanS/Great_Kingdom/assets/99121615/7282dcfe-4fc7-43d0-9133-e946eec23315)
![image](https://github.com/Kor-HanS/Great_Kingdom/assets/99121615/2864cec6-9db4-4267-90ab-981336494da1)
![image](https://github.com/Kor-HanS/Great_Kingdom/assets/99121615/99714ca5-f182-4dcf-99b2-d50184c3d644)



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


Q. 게임판에 게임말을 놓는 동작에 구현
- 이전에 헬포커 디펜스를 통해서는 RayCastHit을 통해서 구현하였으나, 버튼 UI를 통해
  OnClick.AddListenr로 버튼 클릭시 리스너 함수를 달아주어 구현하였습니다.
- 타일 클래스에서 정의된 상태중 0번 blank 상태일 경우만 게임판에 게임말을 놓을 수 있습니다.

Q. 게임판에 게임말을 놓은 후, 영토 계산 알고리즘
- Tile 클래스에서 상태를 10가지로 정의 했습니다.(빈칸, 파랑성, 빨강성, 위쪽벽, 오른쪽벽, 아랫벽, 왼쪽벽, 중간성, 파랑 땅, 빨강땅)
- BFS 탐색을 통해 빈칸 영역을 구하고, 해당 빈칸 영역을 구하면서 만난 벽을 체크합니다.
이때, 4가지 가장자리 벽을 모두 만났거나, 파랑/빨강 성을 동시에 만났거나, 파랑/빨강 성을 만나지 못했을 경우 영토로 인정하지 않습니다.
(4가지 가장자리를 낀 경우, 내 영토에 다른 성이 끼어있거나, 내 성과 다른성이 둘러싼 영토)
