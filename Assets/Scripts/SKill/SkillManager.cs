using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingleMonoBase<SkillManager>
{
    public Dash_Skill dash;

    public Sword_Skill sword_Skill;


    protected override void Awake()
    {
        base.Awake();
    }


    private void Start()
    {
        dash = GetComponent<Dash_Skill>(); 
        sword_Skill = GetComponent<Sword_Skill>();
    }

}
