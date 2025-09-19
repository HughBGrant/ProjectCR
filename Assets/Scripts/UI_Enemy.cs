using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Enemy : MonoBehaviour
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
    IEnumerator DecreaseHealth()
    {
        float duration = 0.5f;
        while (backHealthBar.fillAmount > frontHealthBar.fillAmount)
        {
            backHealthBar.fillAmount = Mathf.MoveTowards(
                backHealthBar.fillAmount,
                frontHealthBar.fillAmount,
                (backHealthBar.fillAmount / duration) * Time.deltaTime
            );
            yield return null;
        }
    }
}
