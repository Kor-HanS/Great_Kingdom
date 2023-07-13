using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreatKingdomClient;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance = null;
    private static NetworkClient client = null;

    public static NetworkManager Instance
    {
        get{ return instance;}
    }

    public static NetworkClient Client
    {
        get{ return client;}
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            client = new NetworkClient("119.196.19.160", 1234);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
