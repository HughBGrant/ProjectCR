using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private float lifetime;
    private Vector3 worldPosition;
    private Camera mainCam;
    private System.Action<DamageText> onReturnToPool;

    public void Initialize(Camera cam, System.Action<DamageText> returnCallback)
    {
        mainCam = cam;
        onReturnToPool = returnCallback;
    }

    public void Show(string value, Vector3 position, Color color, float duration = 1f)
    {
        text.text = value;
        text.color = color;
        worldPosition = position;
        lifetime = duration;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            gameObject.SetActive(false);
            onReturnToPool?.Invoke(this);
            return;
        }

        // 화면 좌표 변환
        Vector3 screenPos = mainCam.WorldToScreenPoint(worldPosition);
        transform.position = screenPos + new Vector3(0, lifetime * 30f, 0); // 위로 떠오르는 효과
        text.alpha = Mathf.Clamp01(lifetime); // 점점 사라지게
    }
}