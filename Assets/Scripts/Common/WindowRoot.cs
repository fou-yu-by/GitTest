using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowRoot : MonoBehaviour
{
    protected ResourceSvc resourceSvc;



    public void SetWindowState(bool isActive)
    {
        if(gameObject.activeSelf != isActive)
        {
            gameObject.SetActive(isActive);
        }
        if (isActive)
        {
            InitWindow();
        }
        else
        {
            ClearWindow();
        }

    }

    protected virtual void InitWindow()
    {
        resourceSvc = ResourceSvc.Instance;
    }

    protected virtual void ClearWindow()
    {
        resourceSvc = null;
    }


}

