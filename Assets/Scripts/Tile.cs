using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // 현재 말을 놓을 수 있는 상태 인가?
    private bool canPlaceCastle = true;

    // 현재 판에 상태 하양 , 빨강 , 파랑 

    public bool CanPlaceCastle
    {
        get { return canPlaceCastle; }
        set { canPlaceCastle = value; }
    }

}
