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
            // DontDestroyOnLoad(gameObject); // ���ο� �� �ε������ ��� ������Ʈ�� ���ŵǴ� ���� ���´�. -> ������ �̹� ���ӿ����� ���� �������� ���� �Ŵ����� ������ ���̹Ƿ�, ����.
        }

        else
            Destroy(gameObject); // �̱��� ��ü�� �ϳ��� ���� �ǵ��� �Ѵ�.
    }

}
