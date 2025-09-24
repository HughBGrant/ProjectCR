using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Damage : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    public void SetDamage(float damage)
    {
        text.text = damage.ToString();
        StartCoroutine(PopupDamage());
    }
    IEnumerator PopupDamage()
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}
