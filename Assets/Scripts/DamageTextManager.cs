using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField]
    private UI_DamageText prefab;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private int poolSize = 20;

    private Queue<UI_DamageText> pool = new Queue<UI_DamageText>();
    private Camera cam;

    private void Awake()
    {
        Instance = this;

        cam = Camera.main;

        for (int i = 0; i < poolSize; i++)
        {
            UI_DamageText obj = Instantiate(prefab, canvas.transform);
            obj.Initialize(cam, ReturnToPool);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public void SpawnText(float damage, Vector3 worldPosition, float duration = 2f, Color? color = null)
    {
        UI_DamageText dt = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab, canvas.transform);
        dt.Initialize(cam, ReturnToPool);
        dt.Show(damage, worldPosition, duration, color);
    }
    private void ReturnToPool(UI_DamageText dt)
    {
        pool.Enqueue(dt);
    }
}