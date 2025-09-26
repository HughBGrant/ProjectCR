using TMPro;
using UnityEngine;

public class UI_DamageText : MonoBehaviour
{
    private TextMeshPro text;
    private float lifetime;

    private Vector3 worldPosition;
    private Camera cam;
    private System.Action<UI_DamageText> onReturnToPool;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    public void Initialize(Camera cam, System.Action<UI_DamageText> returnCallback)
    {
        this.cam = cam;
        onReturnToPool = returnCallback;
    }
    public void Show(float damage, Vector3 position, float duration = 2f, Color? color = null)
    {
        text.text = damage.ToString();
        worldPosition = position + Vector3.up * 2f;
        lifetime = duration;
        text.color = color ?? Color.red;
        //gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!gameObject.activeSelf) { return; }

        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            //1
            //gameObject.SetActive(false);
            //onReturnToPool?.Invoke(this);
            //2
            Destroy(gameObject);

            return;
        }
        text.alpha = Mathf.Clamp01(lifetime);
        //1
        //Vector3 screenPos = mainCam.WorldToScreenPoint(worldPosition);
        //transform.position = screenPos + new Vector3(0, lifetime * 30f, 0);
        //2
        transform.position += Vector3.up * 3f * Time.deltaTime;
    }
}
