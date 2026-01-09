using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField] private float checkWallDistance;
    [SerializeField] private LayerMask checkWallLayer;
    [SerializeField] private bool isOnWall;

    private void Update()
    {
        checkWall();
    }

    private void checkWall()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.right * Player.Instance.faceDir, checkWallDistance, checkWallLayer);
        if(hit)
        {
            isOnWall = true;
        }
        else
        {
            isOnWall= false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + checkWallDistance, transform.position.y));
    }


    public bool CheckOnWall()
    {
        return isOnWall; 
    }


}
