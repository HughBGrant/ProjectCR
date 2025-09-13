using UnityEngine;

[CreateAssetMenu(menuName = "Skills/MeleeSkill")]
public class MeleeSkillData : SkillData
{
    public override void Execute(SkillContext ctx)
    {
        if (ctx.animator && !string.IsNullOrEmpty(animTrigger))
        {
            ctx.animator.SetTrigger(animTrigger);
        }
    }
}
