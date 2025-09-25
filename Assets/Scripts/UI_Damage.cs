using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Damage : MonoBehaviour
{
    private TextMeshPro text;
    private const float speed = 5f;
    private const float popupDuration = 2f;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    public void Init(float damage)
    {
        text.text = damage.ToString();
        StartCoroutine(PopupDamage());
    }
    IEnumerator PopupDamage()
    {
        float time = 0f;
        while (time < popupDuration)
        {
            time += Time.deltaTime;
            text.alpha -= Time.deltaTime;
            transform.position += Vector3.up * speed * Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
