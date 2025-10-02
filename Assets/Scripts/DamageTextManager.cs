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
    //1
    //private Queue<UI_DamageText> damageTextPool = new Queue<UI_DamageText>();
    //2
    private IObjectPool<UI_DamageText> textPool;

    private void Awake()
    {
        Instance = this;
        textPool = new ObjectPool<UI_DamageText>(CreateText, OnGetText, OnReleaseText, OnDestroyText, maxSize: poolSize);
        mainCamera = Camera.main;

        //Prewarm(poolSize);
    }
    public void DisplayDamage(float damage, Vector3 worldPosition, float duration = 2f, Color? color = null)
    {
        UI_DamageText text = textPool.Get();
        text.transform.position = mainCamera.WorldToScreenPoint(worldPosition + (Vector3.up * 2.5f));

        text.Setup(damage, duration, color);
    }
    private void Prewarm(int count)
    {
        List<UI_DamageText> tmp = new List<UI_DamageText>();
        for (int i = 0; i < count; i++)
        {
            UI_DamageText text = textPool.Get();
            tmp.Add(text);
            textPool.Release(text);
        }
    }
    private UI_DamageText CreateText()
    {
        UI_DamageText text = Instantiate(damageTextPrefab, parentTransform).GetComponent<UI_DamageText>();
        text.SetTextPool(textPool);
        return text;
    }
    private void OnGetText(UI_DamageText text)
    {
        text.gameObject.SetActive(true);
    }
    private void OnReleaseText(UI_DamageText text)
    {
        text.gameObject.SetActive(false);
    }
    private void OnDestroyText(UI_DamageText text)
    {
        Destroy(text.gameObject);
    }

}