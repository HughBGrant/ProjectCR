using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct SkillContext
{
    public Animator animator;
    public float nextUsableTime;
}
public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private SkillBase skill;
    [SerializeField]
    private SkillUI ui;

    private Animator animator;

    private Dictionary<string, int> keyToIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        keyToIndex = new Dictionary<string, int>();

        //for (int i = 0; i < slots.Count; i++)
        //{
        //    keyToIndex[slots[i].key] = i;
        //}
    }
    private void Start()
    {
        if (ui) ui.ResetCooldown();
    }
    public void TryCast(string key)
    {
        int index = keyToIndex[key];
        //slots[index].caster.Execute();
    }
}