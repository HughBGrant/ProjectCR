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
    private Dictionary<string, SkillInstance> keyToSkill = new Dictionary<string, SkillInstance>();

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        foreach (SkillBinding binding in bindings)
        {
            string key = binding.key.Trim().ToLowerInvariant();

            keyToSlot[key] = binding.slot;
            keyToSkill[key] = new SkillInstance(binding.skillData);
            binding.slot.RefreshIcon(binding.skillData.icon);
        }
    }
    public bool TryUseSkill(string key)
    {
        key = key.ToLowerInvariant();
        if (!keyToSlot.TryGetValue(key, out SkillSlot slot)) { return false; }

        if (!keyToSkill.TryGetValue(key, out SkillInstance skill)) { return false; }

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