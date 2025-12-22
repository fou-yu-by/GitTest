using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class Model
{
    //单例模式
    private static Model instance;
    public static Model Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Model();
            }
            return instance;
        }
    }

    public XmlDocument dataDocument;
    public XmlNodeList dataNodeList;

    public void InitModel()
    {
        instance = this;

    }



    private string name;
    private int deathCount;
    private string time;
    private string state;

    public string Name { get { return name; } }
    public int DeathCount { get { return deathCount; } }
    public string Time { get { return time; } }
    public string State { get { return state; } }


    //初始化本地资源
    private void InitSaveData()
    {
        string path = Application.dataPath + "/Resources/Data/saveData.xml";

        dataDocument = new XmlDocument();
        StreamReader xmlFile = new StreamReader(path);
        dataDocument.LoadXml(xmlFile.ReadToEnd());
        dataNodeList = dataDocument.SelectSingleNode("data").ChildNodes;

    }

    private void GetSaveData(int dataChooseNum)
    {
        XmlElement element = (XmlElement)dataNodeList[dataChooseNum];
        state = element.GetAttribute("state");
        time = element.GetAttribute("time");
        deathCount = int.Parse(element.GetAttribute("death"));
        time = element.GetAttribute("time");

    }
    
}
