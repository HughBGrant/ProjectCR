using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField]
    private UI_DamageText damageTextPrefab;
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private int poolSize = 20;
    private Camera mainCamera;
    private IObjectPool<UI_DamageText> textPool;

    private void Awake()
    {
        Instance = this;

        textPool = new ObjectPool<UI_DamageText>(CreateText, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, maxSize: 100);
        mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        Prewarm(poolSize);
    }
    public void DisplayDamage(float damage = 0, float duration = 2f, Vector3? worldPosition = null, Color? color = null)
    {
        UI_DamageText text = textPool.Get();
        text.transform.position = mainCamera.WorldToScreenPoint((Vector3)worldPosition + (Vector3.up * 2.5f));

        text.Setup(damage, duration, color);
    }
    private void Prewarm(int count)
    {
        List<UI_DamageText> tmp = new List<UI_DamageText>();
        for (int i = 0; i < count; i++)
        {
            tmp.Add(textPool.Get());
        }
        foreach (UI_DamageText t in tmp)
        {
            textPool.Release(t);
        }
    }
    private UI_DamageText CreateText()
    {
        UI_DamageText text = Instantiate(damageTextPrefab, parentTransform).GetComponent<UI_DamageText>();
        //text.SetTextPool(textPool);
        text.OnExpired += (expiredText) =>
        {
            textPool.Release(expiredText);
        };
        return text;
    }
    private void OnGetFromPool(UI_DamageText text)
    {
        text.gameObject.SetActive(true);
    }
    private void OnReleaseToPool(UI_DamageText text)
    {
        text.gameObject.SetActive(false);
    }
    private void OnDestroyPooledObject(UI_DamageText text)
    {
        Destroy(text.gameObject);
    }

}