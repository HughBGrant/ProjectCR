using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;

    [SerializeField]
    private UI_DamageText damageTextPrefab;
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private int poolSize = 20;
    //1
    private Queue<UI_DamageText> damageTextPool = new Queue<UI_DamageText>();
    //2
    //private IObjectPool<UI_DamageText> pool;
    private Camera mainCamera;

    private void Awake()
    {
        Instance = this;
        //pool = new ObjectPool<UI_DamageText>(CreateEnemy, OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy, maxSize: 50);
        mainCamera = Camera.main;

        for (int i = 0; i < poolSize; i++)
        {
            UI_DamageText text = Instantiate(damageTextPrefab, parentTransform);
            text.Setup(mainCamera, RecycleText);
            text.gameObject.SetActive(false);
            damageTextPool.Enqueue(text);
        }
    }
    public void DisplayDamage(float damage, Vector3 worldPosition, float duration = 2f, Color? color = null)
    {
        UI_DamageText text = damageTextPool.Count > 0 ? damageTextPool.Dequeue() : Instantiate(damageTextPrefab, parentTransform);
        text.Play(damage, worldPosition, duration, color);
    }
    private void RecycleText(UI_DamageText dt)
    {
        damageTextPool.Enqueue(dt);
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