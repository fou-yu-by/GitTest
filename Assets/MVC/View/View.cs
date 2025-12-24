using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public Button levUpBtn;
    public Text expTxt;
    public Text healthTxt;


    public void UpdateInfo(Model model)
    {
        expTxt.text = "Exp:" + model.exp.ToString();
        healthTxt.text = "Health:" + model.health.ToString();
    }


}
