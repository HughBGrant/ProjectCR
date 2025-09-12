using UnityEngine;

[CreateAssetMenu(menuName = "Skills/MeleeSkill")]
public class MeleeSkill : SkillBase
{
    public override void Execute(SkillContext ctx)
    {
        if (ctx.animator && !string.IsNullOrEmpty(animationName))
        {
            ctx.animator.SetTrigger(animationName);
        }
    }
}
