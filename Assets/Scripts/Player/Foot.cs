using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField] private LayerMask checkGroundLayer;
    [SerializeField] private bool isGround;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }


    /// <summary>
    /// 检测是否站在地面上
    /// </summary>
    /// <returns></returns>
    public bool CheckOnGround()
    {
        return isGround; 
    }

}
