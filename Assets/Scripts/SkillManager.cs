using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    //[SerializeField]
    //private List<string> keys;
    //[SerializeField]
    //private List<SkillButton> slots;
    [System.Serializable]
    struct SlotBinding {
        public string key;
        public SkillButton slot;
    }
    [SerializeField]
    private List<SlotBinding> bindings;

    private Dictionary<string, int> keyToIndex;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        keyToIndex = new Dictionary<string, int>();

        for (int i = 0; i < bindings.Count; i++)
        {
            string k = (bindings[i].key ?? "").Trim().ToLowerInvariant();
            if (!string.IsNullOrEmpty(k))
            {
                keyToIndex[k] = i;
            }
        }
    }
    public bool TryActivateSlot(string key)
    {
        key = (key ?? "").ToLowerInvariant();
        if (keyToIndex != null && keyToIndex.TryGetValue(key, out int idx))
        {
            if (idx >= 0 && idx < bindings.Count && bindings[idx].slot != null)
            {
                return bindings[idx].slot.TryUseSkill(animator);
            }
        }
        return false;
    }
}