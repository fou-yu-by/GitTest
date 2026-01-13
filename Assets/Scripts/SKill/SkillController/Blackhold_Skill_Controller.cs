using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhold_Skill_Controller : MonoBehaviour
{
    public float maxSize;
    public float growSpeed;
    public bool canGrow;
    public List<Transform> targets;

    private void Update()
    {
        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Add(collision.transform);
        }
    }
}
