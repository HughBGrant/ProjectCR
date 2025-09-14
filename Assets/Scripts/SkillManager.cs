using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [System.Serializable]
    struct SlotBinding
    {
        public string key;
        public SkillSlot slot;
    }
    [SerializeField]
    private List<SlotBinding> bindings;
    private Dictionary<string, float> keyToEndTimes = new Dictionary<string, float>();
    private Dictionary<string, SkillSlot> keyToSlot = new Dictionary<string, SkillSlot>();

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        keyToSlot = new Dictionary<string, SkillSlot>();

        foreach (SlotBinding binding in bindings)
        {
            if (string.IsNullOrEmpty(binding.key) || binding.slot == null)
            {
                continue;
            }
            string key = binding.key.Trim().ToLowerInvariant();

            keyToSlot[key] = binding.slot;
            keyToEndTimes[key] = 0f;

        }
    }
    public bool TryUseSkill(string key)
    {
        if (string.IsNullOrEmpty(key)) { return false; }

        key = key.ToLowerInvariant();

        if (!keyToSlot.TryGetValue(key, out SkillSlot slot)) { return false; }

        SkillData skill = slot.EquippedSkill;

        if (skill == null) { return false; }
        
        float endTime = keyToEndTimes.TryGetValue(key, out float time) ? time : 0f;

        SkillContext ctx = new SkillContext()
        {
            animator = animator,
            cooldownEndTime = endTime
        };

        if (!skill.CanExecute(ctx)) { return false; }

        ctx.animator.SetTrigger(skill.doSkillHash);
        
        skill.Execute(ctx);

        float addedCooldown = Mathf.Max(0f, skill.cooldown);
        slot.PlayCooldownUI(addedCooldown);
        keyToEndTimes[key] = Time.time + addedCooldown;

        return true;
    }

}