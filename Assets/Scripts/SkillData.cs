using UnityEngine;
using UnityEngine.Serialization;

public abstract class SkillData : ScriptableObject
{
    public string animTrigger;
    [HideInInspector]
    public int doSkillHash; // 해시값

    public float cooldown;
    public Sprite icon;
    private void OnEnable()
    {
        doSkillHash = Animator.StringToHash(animTrigger);
    }

    public abstract void Execute(SkillContext ctx);
    public virtual bool CanExecute(SkillContext ctx)
    {
        return (Time.time >= ctx.cooldownEndTime);
    }
}