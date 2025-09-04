using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField]
    private Image cooldownCover;
    [SerializeField]
    private TextMeshProUGUI cooldownText;
    [SerializeField]
    private float cooldownTotal;
    private Player player;

    private float currentCooldownTime;
    void Awake()
    {
        SetCooldownState(false);
    }
    public void OnUseSkill()
    {
        StartCoroutine(BeginCooldown(cooldownTotal));
    }
    IEnumerator BeginCooldown(float duration)
    {
        currentCooldownTime = duration;
        SetCooldownState(true);

        while (currentCooldownTime > 0)
        {
            currentCooldownTime -= Time.deltaTime;
            cooldownCover.fillAmount = currentCooldownTime / duration;
            cooldownText.text = currentCooldownTime.ToString("00");

            yield return null;
        }
        SetCooldownState(false);
    }
    private void SetCooldownState(bool boolean)
    {
        cooldownCover.gameObject.SetActive(boolean);
    }
}


//public class SkillUI : MonoBehaviour
//{
//    [Header("Wiring")]
//    [SerializeField] private Image icon;
//    [SerializeField] private Image cooldownMask;
//    [SerializeField] private TextMeshProUGUI keyText;
//    [SerializeField] private TextMeshProUGUI timeText;

//    private float cooldownTotal;
//    private float cooldownEnd;   // 쿨다운이 끝나는 시각(Time.time 기준)

//    public void SetIcon(Sprite sprite) => icon.sprite = sprite;
//    public void SetKey(string key) => keyText.text = key;

//    /// <summary>쿨다운 시작. duration(초) 동안 마스크가 채워지고 숫자가 표시됨.</summary>
//    public void BeginCooldown(float duration)
//    {
//        cooldownTotal = Mathf.Max(0f, duration);
//        cooldownEnd = Time.time + cooldownTotal;
//        cooldownMask.fillAmount = 1f;  // 가득 가린 상태에서 시작
//        timeText.enabled = cooldownTotal > 0.05f;
//        UpdateVisual();
//    }

//    /// <summary>즉시 사용 가능 상태로 만듦.</summary>
//    public void ResetCooldown()
//    {
//        cooldownEnd = 0f;
//        cooldownMask.fillAmount = 0f;
//        timeText.enabled = false;
//        timeText.text = "";
//    }

//    private void Update()
//    {
//        UpdateVisual();
//    }

//    private void UpdateVisual()
//    {
//        if (cooldownEnd <= Time.time || cooldownTotal <= 0f)
//        {
//            // 쿨다운 없음
//            cooldownMask.fillAmount = 0f;
//            timeText.enabled = false;
//            return;
//        }

//        float remain = Mathf.Max(0f, cooldownEnd - Time.time);
//        float t = Mathf.InverseLerp(cooldownTotal, 0f, remain); // 0→1
//        cooldownMask.fillAmount = Mathf.Clamp01(remain / cooldownTotal); // 마스크는 남은 비율

//        // 10초 미만은 소수 1자리, 그 외 정수로 깔끔 표시
//        timeText.enabled = true;
//        timeText.text = remain < 10f ? remain.ToString("0.0") : Mathf.CeilToInt(remain).ToString();
//    }
//}