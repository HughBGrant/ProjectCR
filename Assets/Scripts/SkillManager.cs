using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private List<string> keys;
    [SerializeField]
    private List<SkillButton> slots;


    private Dictionary<string, int> keyToIndex;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        keyToIndex = new Dictionary<string, int>();

        for (int i = 0; i < Mathf.Min(keys.Count, slots.Count); i++)
        {
            string k = (keys[i] ?? "").Trim().ToLowerInvariant();
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
            if (idx >= 0 && idx < slots.Count && slots[idx] != null)
            {
                return slots[idx].TryUseSkill(animator);
            }
        }
        return false;
    }
}