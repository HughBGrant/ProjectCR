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

    private SkillRuntime runtime;
    public SkillRuntime Runtime
    {
        get { return runtime; }
    }
    private Coroutine cooldownCo;
    private void OnEnable()
    {
        RefreshIcon();
        maskImage.fillAmount = 0f;
        timeText.enabled = false;
    }
    public void SetSkillRuntime(SkillRuntime runtime)
    {
        this.runtime = runtime;
        RefreshIcon();
    }
    private void RefreshIcon()
    {
        if (iconImage == null) { return; }

        if (runtime != null && runtime.data.icon != null)
        {
            iconImage.enabled = true;
            iconImage.sprite = runtime.data.icon;
        }
        else
        {
            iconImage.enabled = false;
            iconImage.sprite = null;
        }
    }
    public void PlayCooldownUI(float duration)
    {
        if (cooldownCo != null)
        {
            StopCoroutine(cooldownCo);
        }
        cooldownCo = StartCoroutine(CooldownRoutine(duration));
    }
    private IEnumerator CooldownRoutine(float duration)
    {
        duration = Mathf.Max(0f, duration);

        maskImage.fillAmount = 1f;
        timeText.enabled = true;

        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            float remain = endTime - Time.time;

            maskImage.fillAmount = remain / duration;

            // 10초 미만은 소수 1자리, 그 외 정수
            timeText.text = remain > 10f
                ? Mathf.CeilToInt(remain).ToString()
                : remain.ToString("0.0");

            yield return null;
        }
        maskImage.fillAmount = 0f;
        timeText.enabled = false;

        cooldownCo = null;
    }
}
