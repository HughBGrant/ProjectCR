using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
    private SkillData equippedSkill;

    private Coroutine cooldownCo;
    private float nextUsableTime;

    private void Start()
    {
        if (equippedSkill != null)
        {
            iconImage.sprite = equippedSkill.icon;
        }
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
        //timeText.text = "";
        timeText.enabled = false;
    }
    public bool TryUseSkill(Animator animator)
    {
        if (equippedSkill == null)
        {
            return false;
        }
        SkillContext ctx = new SkillContext(){
            animator = animator,
            nextUsableTime = nextUsableTime
        };
        if (!equippedSkill.CanExecute(ctx))
        {
            return false;
        }
        equippedSkill.Execute(ctx);
        nextUsableTime = Time.time + equippedSkill.cooldown;

        if (equippedSkill.cooldown > 0f)
        {
            BeginCooldown(equippedSkill.cooldown);
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
        timeText.enabled = true;

        while (Time.time < endTime)
        {
            float remain = Mathf.Max(0f, endTime - Time.time);
            maskImage.fillAmount = remain / duration;

            // 10초 미만은 소수 1자리, 그 외 정수
            timeText.text = remain > 10f
                ? Mathf.CeilToInt(remain).ToString()
                : remain.ToString("0.0");

            yield return null; // 다음 프레임까지 대기
        }
        maskImage.fillAmount = 0f;
        //timeText.text = "";
        timeText.enabled = false;
        cooldownCo = null;
    }
    void Refresh()
    {

    }
}
