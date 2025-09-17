using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float currentHealth;
    //public float HP => hp;
    private Coroutine hitCo;
    private Material material;
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (hitCo != null)
        {
            StopCoroutine(hitCo);
        }
        hitCo = StartCoroutine(ReactHit());

    }
    private IEnumerator ReactHit()
    {
        material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        material.color = Color.white;
        
    }
}
