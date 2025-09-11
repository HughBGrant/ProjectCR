using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField]
    private Image maskImage;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private SkillBase currentSkill;

    private Coroutine cooldownRoutine;

    public void BeginCooldown(float duration)
    {
        if (cooldownRoutine != null)
        {
            StopCoroutine(cooldownRoutine);
        }
        cooldownRoutine = StartCoroutine(CooldownRoutine(duration));
    }
    public void ResetCooldown()
    {
        if (cooldownRoutine != null)
        {
            StopCoroutine(cooldownRoutine);
        }
        cooldownRoutine = null;

        maskImage.fillAmount = 0f;
        timeText.enabled = false;
        timeText.text = "";
    }
    private IEnumerator CooldownRoutine(float duration)
    {
        duration = Mathf.Max(0f, duration);
        float endTime = Time.time + duration;

        maskImage.fillAmount = 1f;
        timeText.enabled = duration > 0.05f;

        while (Time.time < endTime)
        {
            float remain = Mathf.Max(0f, endTime - Time.time);
            maskImage.fillAmount = remain / duration;

            // 10초 미만은 소수 1자리, 그 외 정수
            timeText.text = remain < 10f ? remain.ToString("0.0") : Mathf.CeilToInt(remain).ToString();

            yield return null; // 다음 프레임까지 대기
        }
        maskImage.fillAmount = 0f;
        timeText.enabled = false;
        cooldownRoutine = null;
    }
}
