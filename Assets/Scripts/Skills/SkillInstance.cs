using UnityEngine;

[System.Serializable]
public class SkillInstance
{
    public SkillData data;
    public float cooldownEndTime;

    public SkillInstance(SkillData data)
    {
        this.data = data;
        cooldownEndTime = 0f;
    }
    public void StartCooldown()
    { 
        cooldownEndTime = Time.time + Mathf.Max(0f, data.cooldown);
    }
    public bool CanExecute()
    {
        return (Time.time >= cooldownEndTime);
    }
}