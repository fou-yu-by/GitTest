using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


/// <summary>
/// 用于加载资源，提供获取资源的方法
/// </summary>
public class ResourceSvc : MonoBehaviour
{
    public static ResourceSvc Instance;

    public XmlDocument dataDocument;
    public XmlNodeList dataNodeList;


    public void InitSvc()
    {
        Instance = this; 
        InitSaveData();
    }

    private void InitSaveData()
    {
        TextAsset xmlFile = Resources.Load<TextAsset>("Data/saveData");
        dataDocument = new XmlDocument();
        dataDocument.LoadXml(xmlFile.text);
        dataNodeList = dataDocument.SelectSingleNode("data").ChildNodes;
    }

    public struct SaveData
    {
        public string state;
        public int deathCount;
        public string time;
        public string savePosition;
    }

    public SaveData GetSaveData(int dataChooseNum)
    {
        XmlElement element = (XmlElement) dataNodeList[dataChooseNum];
        SaveData saveData = new SaveData();
        saveData.state = element.GetAttribute("state");
        saveData.deathCount = int.Parse(element.GetAttribute("death"));
        saveData.time = element.GetAttribute("time");
        saveData.savePosition = element.GetAttribute("savePosition");
        return saveData;
        
    }

}
