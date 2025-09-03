using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillControl : MonoBehaviour
{
    [SerializeField]
    private Image[] cooldownCovers;
    [SerializeField]
    private TextMeshProUGUI[] cooldownTexts;
    [SerializeField]
    private float[] maxCooldownTimes;
    private Player player;

    private float[] currentCooldownTimes = new float[3];
    void Awake()
    {
        SetCooldownState(0, false);
        SetCooldownState(1, false);
        SetCooldownState(2, false);
    }
    public void OnUseSkill(int skillNum)
    {
        StartCoroutine(Cooldown(skillNum));
    }
    IEnumerator Cooldown(int skillNum)
    {
        currentCooldownTimes[skillNum] = maxCooldownTimes[skillNum];
        SetCooldownState(skillNum, true);

        while (currentCooldownTimes[skillNum] > 0)
        {
            currentCooldownTimes[skillNum] -= Time.deltaTime;
            cooldownCovers[skillNum].fillAmount = currentCooldownTimes[skillNum] / maxCooldownTimes[skillNum];
            cooldownTexts[skillNum].text = currentCooldownTimes[skillNum].ToString("00");

            yield return null;
        }
        SetCooldownState(skillNum, false);
    }
    private void SetCooldownState(int skillNum, bool boolean)
    {
        cooldownCovers[skillNum].gameObject.SetActive(boolean);
    }
}
