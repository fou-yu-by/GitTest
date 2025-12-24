using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private View view;

    private void Start()
    {
        view = this.GetComponent<View>();

        view.UpdateInfo(Model.Instance);

        view.levUpBtn.onClick.AddListener(ClickLevUpBtn);

        EventManager.Instance.AddListener("UpdateView", UpdateView);

    }

    private void ClickLevUpBtn()
    {

        Model.Instance.LevUp();
    }

    private void UpdateView(object sender, EventArgs args)
    {
        var modelarg = args as LevUpArgs;
        if(modelarg != null)
        {
            view.UpdateInfo(modelarg.model);
        }
    }


}
