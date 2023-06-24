using UnityEngine;

public class Singleton <T> : MonoBehaviour where T: Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            _instance = FindObjectOfType<T>();

            if(_instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                _instance = obj.AddComponent<T>();
            }

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            // DontDestroyOnLoad(gameObject); // 새로운 씬 로드됐을때 대상 오브젝트가 제거되는 것을 막는다. -> 하지만 이번 게임에서는 단일 씬에서만 게임 매니저가 동작할 것이므로, 삭제.
        }

        else
            Destroy(gameObject); // 싱글톤 객체가 하나로 유지 되도록 한다.
    }

}
