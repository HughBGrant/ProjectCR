using UnityEngine;

[System.Serializable]
public class SkillRuntime
{
    public SkillData data;
    public float cooldownEndTime;

    public SkillRuntime(SkillData data)
    {
        this.data = data;
        cooldownEndTime = 0f;
    }
    public bool CanExecute()
    {
        return (Time.time >= cooldownEndTime);
    }
    public void StartCooldown()
    {
        data.Execute();
        cooldownEndTime = Time.time + Mathf.Max(0f, data.cooldown);
    }
}