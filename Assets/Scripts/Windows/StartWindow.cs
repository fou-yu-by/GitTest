using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWindow : WindowRoot
{
    public RecordWindow RecordWindow;
    private void Update()
    {
        EnterRecordWindow();
    }

    private void EnterRecordWindow()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetWindowState(false);
            RecordWindow.SetWindowState(true);
        }

    }

}
