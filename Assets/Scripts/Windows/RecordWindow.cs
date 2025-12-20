using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;



public class RecordWindow : WindowRoot
{
    public GameObject[] dataArr;

    public GameWindow gameWindow;

    [HideInInspector]
    public int dataChooseNum = 0;



    private void Update()
    {
        ChangeChoose();
        EnterGame();
    }






    protected override void InitWindow()
    {
        base.InitWindow();
        ShowData();
    }

    private void ShowData()
    {
        for (int i = 0; i < dataArr.Length; i++)
        {
            ResourceSvc.SaveData saveData = resourceSvc.GetSaveData(i);
            string state = saveData.state;
            string deathCount = "Death:" + saveData.deathCount.ToString();
            string Time = "Time:" + saveData.time;
            dataArr[i].transform.GetChild(1).GetComponent<Text>().text = state;
            dataArr[i].transform.GetChild(2).GetComponent<Text>().text = deathCount;
            dataArr[i].transform.GetChild(3).GetComponent<Text>().text = Time;
        }
    }



    /// <summary>
    /// 修改选中效果
    /// </summary>
    private void ChangeChoose()
    {
        RectTransform dataChoose = transform.Find("DataChoose").GetComponent<RectTransform>();
        float posx = dataChoose.localPosition.x;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            posx = posx >= 300f ? -300f : posx + 300f;
            dataChooseNum = dataChooseNum >= 2 ? 0 : dataChooseNum + 1;
            dataChoose.localPosition = new Vector2(posx, dataChoose.localPosition.y);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            posx = posx <= -300f ? 300f : posx - 300f;
            dataChooseNum = dataChooseNum <= 0 ? 2 : dataChooseNum - 1;
            dataChoose.localPosition = new Vector2(posx, dataChoose.localPosition.y);
        }

    }

    private void EnterGame()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetWindowState(false);
            gameWindow.SetWindowState(true);
        }
    }



}
