using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShadow : MonoBehaviour
{
    private SpriteRenderer shadowSP;
    private SpriteRenderer playerSP;
    [SerializeField] private float activeTime;
    [SerializeField] private float activeStartTime;


    [Header("²»Í¸Ã÷¶È")]
    [SerializeField] private float alpha;
    [SerializeField] private float originAlpha;
    [SerializeField] private float alphaMult;

    private void OnEnable()
    {
        shadowSP = GetComponent<SpriteRenderer>();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSP = player.GetComponent<SpriteRenderer>();

        alpha = originAlpha;
        shadowSP.sprite = playerSP.sprite;

        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.root.localScale;

        activeStartTime = Time.time;

    }

    private void Update()
    {
        alpha *= alphaMult;
        Color color = new Color(0.5f, 0.5f, 1f, alpha);
        shadowSP.color = color;

        if (Time.time >= activeStartTime + activeTime)
        {
            ObjectPool.Instance.ReturnPool(this.gameObject);

        }
    }

}
