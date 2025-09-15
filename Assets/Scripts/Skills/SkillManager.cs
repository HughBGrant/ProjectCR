using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [System.Serializable]
    struct SlotBinding
    {
        public string key;
        public SkillSlot slot;
        public SkillData skillData;
    }
    [SerializeField]
    private List<SlotBinding> bindings;
    private Dictionary<string, SkillSlot> keyToSlot = new Dictionary<string, SkillSlot>();

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        foreach (SlotBinding binding in bindings)
        {
            SkillRuntime runtime = new SkillRuntime(binding.skillData);
            binding.slot.Runtime = runtime;
            keyToSlot[binding.key.Trim().ToLowerInvariant()] = binding.slot;
        }
    }
    public bool TryUseSkill(string key)
    {
        if (!keyToSlot.TryGetValue(key.ToLowerInvariant(), out SkillSlot slot)) { return false; }

        SkillRuntime runtime = slot.Runtime;

        //스킬 실행
        if (!runtime.StartCooldown()) {  return false; }
        
        //runtime.data.Execute();
        //애니메이션 실행
        animator.SetTrigger(runtime.data.useSkillHash);
        //스킬 UI 실행
        slot.PlayCooldownUI(runtime.data.cooldown);

        return true;
    }
}