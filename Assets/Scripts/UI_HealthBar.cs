using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image backHealthBar;
    [SerializeField]
    private Image frontHealthBar;

    public void SetMaxHealth(float maxValue)
    {
        backHealthBar.fillAmount = 1f;
        frontHealthBar.fillAmount = 1f;

    }
    public void SetHealth(float value)
    {
        frontHealthBar.fillAmount = value;

        StartCoroutine(DecreaseHealth());
    }
    private IEnumerator DecreaseHealth()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        float startValue = backHealthBar.fillAmount;
        float targetValue = frontHealthBar.fillAmount;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            backHealthBar.fillAmount = Mathf.Lerp(startValue, targetValue, t);

            yield return null;
        }
        backHealthBar.fillAmount = targetValue;
    }
}
