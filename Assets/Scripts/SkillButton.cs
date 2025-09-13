using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private Image maskImage;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private SkillData skillData;

    private Coroutine cooldownCo;
    private float nextUsableTime;

    private void Start()
    {
        iconImage.sprite = skillData.icon;
        ResetCooldown();
    }
    public void ResetCooldown()
    {
        if (cooldownCo != null)
        {
            StopCoroutine(cooldownCo);
        }
        cooldownCo = null;

        maskImage.fillAmount = 0f;
        timeText.enabled = false;
        timeText.text = "";
    }
    public bool TryCast(Animator animator)
    {
        if (skillData == null)
        {
            return false;
        }
        SkillContext ctx = new SkillContext(){
            animator = animator,
            nextUsableTime = nextUsableTime
        };
        if (!skillData.CanExecute(ctx))
        {
            return false;
        }
        skillData.Execute(ctx);
        nextUsableTime = Time.time + skillData.cooldownTime;

        if (skillData.cooldownTime > 0f)
        {
            BeginCooldown(skillData.cooldownTime);
        }
        return true;
    }
    public void BeginCooldown(float duration)
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
        float endTime = Time.time + duration;

        maskImage.fillAmount = 1f;
        timeText.enabled = duration > 0.05f;

        while (Time.time < endTime)
        {
            float remain = Mathf.Max(0f, endTime - Time.time);
            maskImage.fillAmount = remain / duration;

            // 10초 미만은 소수 1자리, 그 외 정수
            timeText.text = remain < 10f
                ? remain.ToString("0.0")
                : Mathf.CeilToInt(remain).ToString();

            yield return null; // 다음 프레임까지 대기
        }
        maskImage.fillAmount = 0f;
        timeText.enabled = false;
        cooldownCo = null;
    }

}
