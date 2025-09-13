using System.Collections.Generic;
using UnityEngine;

public struct SkillContext
{
    public Animator animator;
    public float nextUsableTime;
}
public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private List<string> keys = new List<string>() { "1" };
    [SerializeField]
    private List<SkillButton> slots = new List<SkillButton>();


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
    public bool TryCast(string key)
    {
        key = (key ?? "").ToLowerInvariant();
        if (keyToIndex != null && keyToIndex.TryGetValue(key, out int idx))
        {
            if (idx >= 0 && idx < slots.Count && slots[idx] != null)
            {
                return slots[idx].TryCast(animator);
            }
        }
        return false;
    }
}