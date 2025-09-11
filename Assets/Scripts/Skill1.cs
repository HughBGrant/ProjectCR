using UnityEngine;

public class Skill1 : SkillBase
{
    private float nextUsableTime;

    public override void Execute(SkillContext ctx)
    {
        if (ctx.animator) ctx.animator.SetTrigger(doSkillHash);
    }
}
