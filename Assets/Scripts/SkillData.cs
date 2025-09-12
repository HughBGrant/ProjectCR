using UnityEngine;
using UnityEngine.Serialization;

public abstract class SkillData : ScriptableObject
{
    public string animationName;
    public float cooldown;

    public abstract void Execute(SkillContext ctx);
    public virtual bool CanExecute(SkillContext ctx)
    {
        return (Time.time >= ctx.nextUsableTime);
    }
}