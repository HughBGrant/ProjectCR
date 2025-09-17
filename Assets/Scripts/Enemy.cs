using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float currentHealth;
    //public float HP => hp;
    private Material material;
    private Coroutine hitCo;
    private float deathDestroyDelay = 2f;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"체력 {damage} 감소. 현재 체력 {currentHealth}");

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

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            material.color = Color.white;
        }
    }
}
