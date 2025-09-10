using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillManager : MonoBehaviour
{
    public void UseSkill(string key)
    {

    }
    //    [Serializable]
    //    public class Slot
    //    {
    //        public string key = "1";

    //        public SkillCaster caster;

    //        public bool enabled = true;
    //    }
    //    [SerializeField]
    //    private List<Slot> slots = new List<Slot>();

    //    private Dictionary<string, int> keyToIndex;

    //    private void Awake()
    //    {
    //        keyToIndex = new Dictionary<string, int>();
    //        for (int i = 0; i < slots.Count; i++)
    //        {
    //            if (slots[i] == null || slots[i].caster == null) { continue; }

    //            string k = (slots[i].key ?? "").Trim().ToLowerInvariant();

    //            if (string.IsNullOrEmpty(k)) { continue; }

    //            keyToIndex[k] = i;
    //        }
    //    }
    //    public void OnSkill(InputAction.CallbackContext ctx)
    //    {
    //        if (!ctx.performed) { return; }

    //        string k = (ctx.control?.name ?? "").ToLowerInvariant();
    //        if (string.IsNullOrEmpty(k)) { return; }

    //        if (keyToIndex.TryGetValue(k, out int idx))
    //        {
    //            TryCastSlot(idx);
    //        }
    //        else
    //        {
    //        }
    //    }
    //    private void TryCastSlot(int index)
    //    {
    //        if (index < 0 || index >= slots.Count) { return; }

    //        Slot slot = slots[index];

    //        if (slot == null || !slot.enabled || slot.caster == null) { return; }

    //        slot.caster.TryCast();
    //    }
    //    //public void OnClickSlot(int index)
    //    //{
    //    //    TryCastSlot(index);
    //    //}
}