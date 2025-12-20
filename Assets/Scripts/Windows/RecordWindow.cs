using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;



public class RecordWindow : WindowRoot
{
    public GameObject[] dataArr;


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


}
