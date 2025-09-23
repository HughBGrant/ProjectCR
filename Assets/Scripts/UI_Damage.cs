using TMPro;
using UnityEngine;

public class UI_Damage : MonoBehaviour
{
    TextMeshPro text;

    private void OnEnable()
    {

    }
    public void SetDamage(float damage)
    {
        text.text = damage.ToString();
    }
}
