using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    float[] stats = new float[(int)StatType.Count];
    public void SetStat(StatType e, float value)
    {
        stats[(int)e] = value;
    }
    public float GetStat(StatType e)
    {
        return stats[(int)e];
    }

    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = Mathf.Max(0, value); } }
    private Material material;
    private Coroutine hitCo;
    private float deathDestroyDelay = 2f;
    [SerializeField]
    private UI_HealthBar healthBar;
    [SerializeField]
    private GameObject DamageTextPrefab;
    [SerializeField]
    private Transform spawnPoint;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }
    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        CurrentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        healthBar.SetHealth(CurrentHealth / maxHealth);

        DamageTextManager.Instance.DisplayDamage(damage, 1f, transform.position);
        Debug.Log($"체력 {damage} 감소. 현재 체력 {CurrentHealth}");

        if (hitCo != null)
        {
            StopCoroutine(hitCo);
        }
        hitCo = StartCoroutine(ReactToHit());

    }
    private void Die()
    {
        material.color = Color.gray;


        Destroy(gameObject, deathDestroyDelay);
    }
    private IEnumerator ReactToHit()
    {
        material.color = Color.red;
        yield return new WaitForSeconds(0.5f);

        if (CurrentHealth <= 0)
        {
            Die();
        }
        else
        {
            material.color = Color.white;
        }
    }
}
