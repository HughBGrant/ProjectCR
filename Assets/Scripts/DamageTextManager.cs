using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField]
    private UI_DamageText textPrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private int poolSize = 20;
    //1
    private Queue<UI_DamageText> pool = new Queue<UI_DamageText>();
    //2
    //private IObjectPool<UI_DamageText> pool;
    private Camera cam;

    private void Awake()
    {
        Instance = this;
        //pool = new ObjectPool<UI_DamageText>(CreateEnemy, OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy, maxSize: 50);
        cam = Camera.main;

        for (int i = 0; i < poolSize; i++)
        {
            UI_DamageText text = Instantiate(textPrefab, spawnPoint);
            text.Initialize(cam, ReturnToPool);
            text.gameObject.SetActive(false);
            pool.Enqueue(text);
        }
    }
    public void SpawnText(float damage, Vector3 worldPosition, float duration = 2f, Color? color = null)
    {
        UI_DamageText text = pool.Count > 0 ? pool.Dequeue() : Instantiate(textPrefab, spawnPoint);
        text.Show(damage, worldPosition, duration, color);
    }
    private void ReturnToPool(UI_DamageText dt)
    {
        pool.Enqueue(dt);
    }

    //void Spawn()
    //{
    //    Enemy enemy = enemyPool.Get();
    //    Vector3 spawnPos = spawnPoss[Random.Range(0, spawnPoss.Length)];
    //    enemy.transform.position = transform.position + spawnPos;

    //    enemy.Init(enemyDatas[level]);
    //}
    //private UI_DamageText CreateEnemy()
    //{
    //    UI_DamageText text = Instantiate(textPrefab, GameManager.Instance.EnemySpace.transform).GetComponent<Enemy>();
    //    text.SetTextPool(textPool);
    //    return text;
    //Bullet bullet = Instantiate(bulletPrefab, GameManager.Instance.BulletSpace.transform).GetComponent<Bullet>();
    //bullet.SetBulletPool(bulletPool);
    //    return bullet;
    //}
    //private void OnGetEnemy(UI_DamageText text)
    //{
    //    text.gameObject.SetActive(true);
    //}
    //private void OnReleaseEnemy(UI_DamageText text)
    //{
    //    text.gameObject.SetActive(false);
    //}
    //private void OnDestroyEnemy(UI_DamageText text)
    //{
    //    Destroy(text.gameObject);
    //}
}