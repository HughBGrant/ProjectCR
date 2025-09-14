using System.Collections.Generic;
using UnityEngine;
public struct SkillContext
{
    public Animator animator;
    public float cooldownEndTime;
}
public class SkillManager : MonoBehaviour
{
    [System.Serializable]
    struct SlotBinding
    {
        public string key;
        public SkillSlot slot;
        public SkillData skillData; // 원본 스펙 에셋
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
            if (string.IsNullOrEmpty(binding.key) || binding.slot == null)
            {
                continue;
            }
            SkillRuntime runtime = new SkillRuntime(binding.skillData);
            binding.slot.SetSkillRuntime(runtime);
            keyToSlot[binding.key.Trim().ToLowerInvariant()] = binding.slot;

        }
    }
    public bool TryUseSkill(string key)
    {
        if (!keyToSlot.TryGetValue(key.ToLowerInvariant(), out SkillSlot slot)) { return false; }

        SkillRuntime runtime = slot.Runtime;

        if (runtime == null) { return false; }

        if (!runtime.CanExecute()) { return false; }

        animator.SetTrigger(runtime.data.useSkillHash);
        
        runtime.data.Execute();
        runtime.StartCooldown();

        slot.PlayCooldownUI(runtime.data.cooldown);

        return true;
    }

}