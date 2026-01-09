using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMonoBase<T> : MonoBehaviour where T : SingleMonoBase<T>
{
    public static T Instance;

    protected virtual void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError(this + "不符合单例模式");
        }
        Instance = (T)this;
    }
    protected virtual void OnDestroy()
    {
        Instance = null;
    }


}
