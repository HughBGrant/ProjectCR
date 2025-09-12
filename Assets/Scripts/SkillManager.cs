using System.Collections.Generic;
using UnityEngine;

public struct SkillContext
{
    public Animator animator;
    public float nextUsableTime;
}
public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private SkillData skill;
    [SerializeField]
    private SkillButton ui;
    private float nextUsableTime;

    private Animator animator;

    [SerializeField]
    private List<string> keys = new List<string>() { "1" };
    [SerializeField]
    private List<SkillData> skills = new List<SkillData>();
    private Dictionary<string, int> keyToIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        keyToIndex = new Dictionary<string, int>();

        for (int i = 0; i < Mathf.Min(keys.Count, skills.Count); i++)
        {
            keyToIndex[(keys[i] ?? "").Trim().ToLowerInvariant()] = i;
        }
    }
    private void Start()
    {
        if (ui)
        {
            ui.ResetCooldown();
        }
    }
    public bool TryCast(string key)
    {
        key = (key ?? "").ToLowerInvariant();
        int idx = keyToIndex != null && keyToIndex.TryGetValue(key, out var i) ? i : 0;

        if (skill == null)
        {
            return false;
        }
        var ctx = new SkillContext{ animator = animator, nextUsableTime = nextUsableTime };

        if (!skill.CanExecute(ctx))
        {
            return false;
        }

        skill.Execute(ctx);
        nextUsableTime = Time.time + skill.cooldown;

        if (ui != null)
        {
            ui.BeginCooldown(skill.cooldown);
        }
        return true;
    }
}