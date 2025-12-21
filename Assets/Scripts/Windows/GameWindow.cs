using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : WindowRoot
{

    public GameObject player;
    public GameObject gameOverTip;

    public Transform startPoint;

    protected override void InitWindow()
    {
        base.InitWindow();
        GameStart();
    }

    private void GameStart()
    {
        player.SetActive(true);
        gameOverTip.SetActive(false);
    }


    public void GameOver()
    {
        player.SetActive(false);
        gameOverTip.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    private void Restart()
    {
        GameStart();
        player.transform.position = startPoint.position;
    }
    
}
