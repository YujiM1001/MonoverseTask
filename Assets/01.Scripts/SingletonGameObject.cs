using UnityEngine;

public class SingletonGameObject<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(object.ReferenceEquals(instance, null))
            {
                GameObject obj = new GameObject(typeof(T).ToString());
                instance = obj.AddComponent<T>();

                if(Application.isPlaying)
                {
                    DontDestroyOnLoad(obj);
                }
            }
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
