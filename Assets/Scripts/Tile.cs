using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // ���� ���� ���� �� �ִ� ���� �ΰ�?
    private bool canPlaceCastle = true;

    // ���� �ǿ� ���� �Ͼ� , ���� , �Ķ� 

    public bool CanPlaceCastle
    {
        get { return canPlaceCastle; }
        set { canPlaceCastle = value; }
    }

}
