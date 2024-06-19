using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    public static T Instance
    {
        get
        {
            if(Instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if(instance == null)
                {
                    GameObject go = new GameObject(nameof(T),typeof(T));
                    instance = go.AddComponent<T>();
                }
            }
            return Instance;
        }
    }



    public virtual void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}
