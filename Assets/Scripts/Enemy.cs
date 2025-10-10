using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private float[] stats = new float[(int)StatType.Count];
    public float this[StatType e]
    {
        get => stats[(int)e];
        set => stats[(int)e] = value;
    }
    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = Mathf.Max(0, value); } }

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private UI_HealthBar healthBar;
    [SerializeField]
    private GameObject DamageTextPrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Transform targetPoint;

    private bool isChasing;
    private Material material;
    private Coroutine hitCo;
    private NavMeshAgent navAgent;

    private const float deathDestroyDelay = 2f;

    private void Awake()
    {
        //material = GetComponent<MeshRenderer>().material;
        material = meshRenderer.material;
        navAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        CurrentHealth = maxHealth;

        StartCoroutine(StartChase());
    }
    private void Update()
    {
        //Target();

        if (navAgent.enabled)
        {
            navAgent.SetDestination(targetPoint.position);
            navAgent.isStopped = !isChasing;
        }
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
    private void Target()
    {
        float radius = 5f;
        float range = 5f;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, range, LayerMask.GetMask("Player"));
    }
    private void Die()
    {
        material.color = Color.gray;


        Destroy(gameObject, deathDestroyDelay);
    }
    private IEnumerator StartChase()
    {
        yield return new WaitForSeconds(2.0f);

        isChasing = true;
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

    //public IEnumerator Attack(Enemy enemy)
    //{
    //    yield return YieldCache.WaitForSeconds(0.1f);
    //    enemy.Rigidbody.AddForce(enemy.transform.forward * 20, ForceMode.Impulse);
    //    enemy.HitBox.enabled = true;
    //    yield return YieldCache.WaitForSeconds(0.5f);
    //    enemy.Rigidbody.velocity = Vector3.zero;
    //    enemy.HitBox.enabled = false;
    //    yield return YieldCache.WaitForSeconds(2.0f);
    //}

}
