using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimationTrigger : MonoBehaviour
{
   public void ThrowSword()
    {
        SkillManager.Instance.sword_Skill.CreatSword();
    } 
}
