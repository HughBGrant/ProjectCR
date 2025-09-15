using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private Image maskImage;
    [SerializeField]
    private TextMeshProUGUI timeText;

    private SkillInstance skill;
    public SkillInstance Skill
    {
        get { return skill; }
        set
        {
            skill = value;
            RefreshIcon();
        }
    }
    private Coroutine cooldownCo;
    private void OnEnable()
    {
        RefreshIcon();
        SwitchCooldown(false);
    }
    private void RefreshIcon()
    {
        iconImage.enabled = true;
        iconImage.sprite = skill.data.icon;
    }
    public void PlayCooldownUI(float cooldownTime)
    {
        if (cooldownCo != null)
        {
            StopCoroutine(cooldownCo);
        }
        cooldownCo = StartCoroutine(CooldownRoutine(cooldownTime));
    }
    private IEnumerator CooldownRoutine(float cooldownTime)
    {
        SwitchCooldown(true);

        float endTime = Time.time + cooldownTime;

        while (Time.time < endTime)
        {
            float remainingTime = endTime - Time.time;

            maskImage.fillAmount = remainingTime / cooldownTime;

            timeText.text = remainingTime >= 10f
                ? Mathf.CeilToInt(remainingTime).ToString()
                : remainingTime.ToString("0.0");

            yield return null;
        }
        SwitchCooldown(false);

        cooldownCo = null;
    }
    private void SwitchCooldown(bool state)
    {
        maskImage.fillAmount = state ? 1f : 0f;
        timeText.enabled = state;
    }
}
