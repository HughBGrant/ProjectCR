using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class UI_DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText;
    private float remainingLifetime;
    private IObjectPool<UI_DamageText> textPool;

    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }
    public void Setup(float damage, float duration = 2f, Color? color = null)
    {
        damageText.text = damage.ToString();

        remainingLifetime = duration;
        damageText.color = color ?? Color.red;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!gameObject.activeSelf) { return; }

        remainingLifetime -= Time.deltaTime;

        if (remainingLifetime <= 0f)
        {
            textPool.Release(this);

            return;
        }
        damageText.alpha = Mathf.Clamp01(remainingLifetime);

        transform.position += new Vector3(0, Time.deltaTime * 100f, 0);
    }
    public void SetTextPool(IObjectPool<UI_DamageText> pool)
    {
        textPool = pool;
    }
}
