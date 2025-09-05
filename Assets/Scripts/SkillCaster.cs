using UnityEngine;
using UnityEngine.InputSystem;

public class SkillCaster : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private float cooldown = 5f;
    [SerializeField]
    private string hotkeyLabel = "1";

    [Header("Refs")]
    [SerializeField]
    private SkillUI ui;
    private Player player;

    private float nextUsableTime;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Start()
    {
        if (ui != null)
        {
            ui.ResetCooldown();
        }
    }

    public void OnSkill(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (Time.time < nextUsableTime) return; // 아직 쿨다운 중

        // 실제 스킬 발동 로직이 들어갈 자리 (이펙트/데미지 등)
        Debug.Log("Skill Cast!");
        player.animator.SetTrigger("doSkill1");


        // 쿨다운 시작
        nextUsableTime = Time.time + cooldown;
        ui.BeginCooldown(cooldown);
    }
}
