using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using Newtonsoft.Json;


public class Model
{
    //µ¥ÀýÄ£Ê½
    private static Model instance;
    public static Model Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Model();
                instance.InitData();
            }

            return instance;
        }
    }

    public float exp {  get; private set; }

    public float health {  get; private set; }

    private ModelData modelData;

    public void InitData()
    {
        modelData = LocalData();
        exp = modelData.exp;
        health = modelData.health;
    }


    public void GetData()
    {
        exp = modelData.exp;
        health = modelData.health;
    }


    public void LevUp()
    {
        if(exp - 200 >= 0)
        {
            exp -= 200;
            health += 10;
        }

        this.TriggerEvent("UpdateView", new LevUpArgs { model = this });
        SaveData(modelData);
    }


    public static void SaveData(ModelData data)
    {
        if(!File.Exists(Application.persistentDataPath + "/users"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/users");
        }
        data.GetValue();
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.persistentDataPath + string.Format("/users/ModelData.json"), jsonData);

    }

    public static ModelData LocalData()
    {
        string path = Application.persistentDataPath + string.Format("/users/ModelData.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ModelData>(jsonData);
        }
        else
        {
            return null;
        }
    }


   
}



public class ModelData
{
    public float exp;
    public float health;

    public void GetValue()
    {
        exp = Model.Instance.exp;
        health = Model.Instance.health;
    }



}
