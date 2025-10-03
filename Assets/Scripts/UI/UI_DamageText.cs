using System;
using TMPro;
using UnityEngine;

public class UI_DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText;
    private float startLifetime;

    public Action<UI_DamageText> OnExpired;
    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }
    public void Setup(float damage, float duration = 2f, Color? color = null)
    {
        damageText.text = damage.ToString();
        startLifetime = duration;
        damageText.color = color ?? Color.red;
        //gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!gameObject.activeSelf) { return; }//////////

        startLifetime -= Time.deltaTime;

        if (startLifetime <= 0f)
        {
            OnExpired?.Invoke(this);
            return;
        }
        damageText.alpha = Mathf.Clamp01(startLifetime);

        transform.position += new Vector3(0, Time.deltaTime * 100f, 0);
    }
}
