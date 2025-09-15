using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [System.Serializable]
    struct SkillBinding
    {
        public string key;
        public SkillSlot slot;
        public SkillData skillData;
    }
    [SerializeField]
    private List<SkillBinding> bindings;
    private Dictionary<string, SkillSlot> keyToSlot = new Dictionary<string, SkillSlot>();

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        foreach (SkillBinding binding in bindings)
        {
            SkillInstance skill = new SkillInstance(binding.skillData);
            binding.slot.Skill = skill;
            keyToSlot[binding.key.Trim().ToLowerInvariant()] = binding.slot;
        }
    }
    public bool TryUseSkill(string key)
    {
        if (!keyToSlot.TryGetValue(key.ToLowerInvariant(), out SkillSlot slot)) { return false; }

        SkillInstance skill = slot.Skill;

        if (!skill.CanExecute()) {  return false; }
        
        skill.data.Execute();
        skill.StartCooldown();
        
        //애니메이션 실행
        animator.SetTrigger(skill.data.useSkillHash);
        //스킬 UI 실행
        slot.PlayCooldownUI(skill.data.cooldown);

        return true;
    }
}