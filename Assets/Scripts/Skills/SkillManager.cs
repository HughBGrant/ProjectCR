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

        if (!runtime.CanExecute()) { return false; }

        animator.SetTrigger(runtime.data.useSkillHash);
        
        runtime.StartCooldown();
        //runtime.data.Execute();

        slot.PlayCooldownUI(runtime.data.cooldown);

        return true;
    }
}