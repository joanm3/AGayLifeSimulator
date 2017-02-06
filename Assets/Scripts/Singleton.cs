using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static void Init(Transform parent = null)
    {
        if (Instance == null)
            Instance = FindObjectOfType(typeof(T)) as T;

        if (Instance == null)
        {
            GameObject container = new GameObject(typeof(T).Name);
            Instance = container.AddComponent(typeof(T)) as T;
        }

        if (Instance != null)
        {
            if (parent != null)
                Instance.gameObject.transform.SetParent(parent, false);
            Instance.gameObject.name = typeof(T).Name;
            DontDestroyOnLoad(Instance.gameObject);
            Debug.Log(Instance.gameObject.name + " initialised", Instance.gameObject);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("two instances of singleton detected. Deleting this: " + Instance.gameObject.name, this.gameObject);
            Destroy(gameObject);
        }
        else
            OnAwake();
    }

    protected virtual void OnAwake()
    {
        Init(null);
    }

    public static T Instance
    {
        get;
        private set;
    }
}