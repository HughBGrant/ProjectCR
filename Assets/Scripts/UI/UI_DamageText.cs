using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class UI_DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText;
    private float startLifetime = 1f;
    private IObjectPool<UI_DamageText> textPool;

    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }
    public void Setup(float damage, float duration = 2f, Color? color = null)
    {
        damageText.text = damage.ToString();

        startLifetime = duration;
        damageText.color = color ?? Color.red;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!gameObject.activeSelf) { return; }

        Debug.Log(startLifetime);
        startLifetime -= Time.deltaTime;

        if (startLifetime <= 0f)
        {
            Debug.Log("asdfsdfdsf");
            textPool.Release(this);

            return;
        }
        damageText.alpha = Mathf.Clamp01(startLifetime);

        transform.position += new Vector3(0, Time.deltaTime * 100f, 0);
    }
    private void OnDisable()
    {
        if (!Application.isPlaying) textPool = null;
    }
    public void SetTextPool(IObjectPool<UI_DamageText> pool)
    {
        textPool = pool;
    }
}
