using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public  static GameRoot Instance;

    public StartWindow startWindow;

    private void Awake()
    {
        Instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
        ClearWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ClearWindow()
    {
        Transform canvas = transform.Find("Canvas");
        for(int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        startWindow.SetWindowState(true);

    }

    private void InitGame()
    {
        ResourceSvc resourceSvc = GetComponent<ResourceSvc>();
        resourceSvc.InitSvc();


    }



}
