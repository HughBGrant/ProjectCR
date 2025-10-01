using System;
using TMPro;
using UnityEngine;

public class UI_DamageText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float lifetime;

    private Vector3 worldPosition;
    private Camera cam;
    private Action<UI_DamageText> onReturnToPool;
    //private IObjectPool<Enemy> enemyPool;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public void Initialize(Camera cam, Action<UI_DamageText> returnCallback)
    {
        this.cam = cam;
        onReturnToPool = returnCallback;
    }
    public void Show(float damage, Vector3 position, float duration = 2f, Color? color = null)
    {
        text.text = damage.ToString();
        worldPosition = position + Vector3.up * 2f;
        lifetime = duration;
        text.color = color ?? Color.red;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!gameObject.activeSelf) { return; }

        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            gameObject.SetActive(false);
            onReturnToPool?.Invoke(this);

            return;
        }
        text.alpha = Mathf.Clamp01(lifetime);
        Vector3 screenPos = cam.WorldToScreenPoint(worldPosition);
        transform.position = screenPos + new Vector3(0, -lifetime * 100f, 0);
    }
    //public void SetEnemyPool(IObjectPool<Enemy> pool)
    //{
    //    enemyPool = pool;
    //}
}
