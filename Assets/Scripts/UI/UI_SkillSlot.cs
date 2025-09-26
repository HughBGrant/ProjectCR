using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSlot : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private Image maskImage;
    [SerializeField]
    private TextMeshProUGUI timeText;

    private Coroutine cooldownCo;
    private void OnEnable()
    {
        SwitchCooldown(false);
    }
    public void RefreshIcon(Sprite icon)
    {
        iconImage.enabled = true;
        iconImage.sprite = icon;
    }
    public void PlayCooldownUI(float cooldownTime)
    {
        if (cooldownCo != null)
        {
            StopCoroutine(cooldownCo);
        }
        cooldownCo = StartCoroutine(AnimateCooldownUI(cooldownTime));
    }
    private IEnumerator AnimateCooldownUI(float cooldownTime)
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
