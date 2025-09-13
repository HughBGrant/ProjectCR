using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    //[SerializeField]
    //private List<string> keys;
    //[SerializeField]
    //private List<SkillButton> slots;
    [System.Serializable]
    struct SlotBinding
    {
        public string key;
        public SkillButton slot;
    }
    [SerializeField]
    private List<SlotBinding> bindings;

    //private Dictionary<string, int> keyToIndex;
    private Dictionary<string, SkillButton> keySlotMap;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //keyToIndex = new Dictionary<string, int>();
        keySlotMap = new Dictionary<string, SkillButton>();

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
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }
        if (!keySlotMap.TryGetValue(key.ToLowerInvariant(), out SkillButton slot))
        {
            return false;
        }
        return slot.TryUseSkill(animator);
    }
}