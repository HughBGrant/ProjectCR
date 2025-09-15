using UnityEngine;
using System;

public abstract class SkillData : ScriptableObject
{
    public string animTrigger;

    public float cooldown;
    public Sprite icon;
    [NonSerialized]
    public int useSkillHash;

    private void OnEnable()
    {
        useSkillHash = Animator.StringToHash(animTrigger);
    }
    public abstract void Execute();
}