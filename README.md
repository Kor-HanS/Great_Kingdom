# Great_Kingdom
게임 클라이언트 개발 (김 한승) / 게임 서버 API 개발 (김 진수) 

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


Q. 게임판에 게임말을 놓는 동작에 구현
- 이전에 헬포커 디펜스를 통해서는 RayCastHit을 통해서 구현하였으나, 버튼 UI를 통해
  OnClick.AddListenr로 버튼 클릭시 리스너 함수를 달아주어 구현하였습니다.
