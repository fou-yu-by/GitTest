using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Skill : Skill
{
    public float dashDuration;
    public float dashForce;
    [SerializeField] private float dashTimer;
    public int shadowCount;

    public override void UseSkill()
    {
        base.UseSkill();

    }

    protected override void Update()
    {
        base.Update();
        dashTimer = coolDownTimer;
        if (dashTimer < 0)
        {
            dashTimer = 0;
        }
    }
}
