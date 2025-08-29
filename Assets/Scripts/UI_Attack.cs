using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UI_Attack : MonoBehaviour
{
    [SerializeField]
    private float maxCooldownTime;
    private float currentCooldownTime;
    private bool isCooldown;
    [SerializeField]
    private TextMeshProUGUI textCooldownTime;
    [SerializeField]
    private Image imageCooldownTime;

    //public void OnClickSkill(int index)
    //{
    //    switch ((Skill)index)
    //    {
    //        case Skill.Attack:
    //            break;
    //        case Skill.Skill1:
    //            break;
    //    }
    //}
    public void OnUseSkill()
    {
        StartCoroutine(nameof(OnCooldownTime), maxCooldownTime);
    }
    void OnCooldownTime()
    {

    }
}
