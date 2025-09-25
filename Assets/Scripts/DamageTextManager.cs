using UnityEngine;
using System.Collections.Generic;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField] private DamageText prefab;
    [SerializeField] private int poolSize = 20;
    [SerializeField] private Canvas canvas;

    private Queue<DamageText> pool = new Queue<DamageText>();
    private Camera mainCam;

    private void Awake()
    {
        Instance = this;
        mainCam = Camera.main;

        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab, canvas.transform);
            obj.Initialize(mainCam, ReturnToPool);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public void SpawnText(string value, Vector3 worldPosition, Color color, float duration = 1f)
    {
        DamageText dt = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab, canvas.transform);
        dt.Initialize(mainCam, ReturnToPool);
        dt.Show(value, worldPosition, color, duration);
    }

    private void ReturnToPool(DamageText dt)
    {
        pool.Enqueue(dt);
    }
}