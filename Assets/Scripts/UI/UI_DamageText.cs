using System;
using TMPro;
using UnityEngine;

public class UI_DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText;
    private float remainingLifetime;
    private Vector3 spawnWorldPosition;
    private Camera mainCamera;
    private Action<UI_DamageText> returnToPoolCallback;
    //private IObjectPool<Enemy> enemyPool;

    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }
    public void Setup(Camera cam, Action<UI_DamageText> returnCallback)
    {
        this.mainCamera = cam;
        returnToPoolCallback += returnCallback;
    }
    public void Play(float damage, Vector3 position, float duration = 2f, Color? color = null)
    {
        damageText.text = damage.ToString();
        spawnWorldPosition = position + Vector3.up * 2f;
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
            gameObject.SetActive(false);
            returnToPoolCallback?.Invoke(this);

            return;
        }
        damageText.alpha = Mathf.Clamp01(remainingLifetime);
        Vector3 screenPos = mainCamera.WorldToScreenPoint(spawnWorldPosition);
        transform.position = screenPos + new Vector3(0, -remainingLifetime * 100f, 0);
    }
    //public void SetEnemyPool(IObjectPool<Enemy> pool)
    //{
    //    enemyPool = pool;
    //}
}
