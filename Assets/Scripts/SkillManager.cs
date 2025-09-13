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

    private Dictionary<string, SkillSlot> keySlotMap;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        keySlotMap = new Dictionary<string, SkillSlot>();

        foreach (SlotBinding binding in bindings)
        {
            string key = (binding.key ?? "").Trim().ToLowerInvariant();

            if (string.IsNullOrEmpty(key) || binding.slot == null)
            {
                Debug.LogWarning($"[SkillManager] 빈 키/빈 슬롯 바인딩 무시: '{binding.key}'");
                continue;
            }
            keySlotMap[key] = binding.slot;
        }
    }
    public bool TryActivateSlot(string key)
    {
        if (string.IsNullOrEmpty(key) || keySlotMap == null)
        {
            return false;
        }
        if (!keySlotMap.TryGetValue(key.ToLowerInvariant(), out SkillSlot slot))
        {
            return false;
        }
        return slot.TryUseSkill(animator);
    }
}