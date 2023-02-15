using UnityEngine;

public class SingletonGameObject<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    public static void CreateInstance(GameObject obj)
    {
        instance = obj.GetComponent<T>();

        if(Application.isPlaying)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
