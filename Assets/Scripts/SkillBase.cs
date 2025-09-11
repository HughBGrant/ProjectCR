using UnityEngine;

public abstract class SkillBase : ScriptableObject
{
    protected string doSkillHash;
    protected float cooldown;

    public abstract void Execute(SkillContext ctx);
    public virtual bool CanCast(SkillContext ctx)
    {
        return Time.time >= ctx.nextUsableTime;
    }
}