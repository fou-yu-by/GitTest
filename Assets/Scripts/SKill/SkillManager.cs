using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingleMonoBase<SkillManager>
{
    public Dash_Skill dash;

    public Sword_Skill sword_Skill;

    public Crystal_Skill crystal;
    protected override void Awake()
    {
        base.Awake();
    }


    private void Start()
    {
        dash = GetComponent<Dash_Skill>(); 
        sword_Skill = GetComponent<Sword_Skill>();
        crystal = GetComponent<Crystal_Skill>();
    }

}
