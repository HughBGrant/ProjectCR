using UnityEngine;
using UnityEngine.InputSystem;

public class SkillCaster : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private string hotkeyLabel;

    [Header("Refs")]
    [SerializeField]
    private SkillUI ui;

    [SerializeField]
    private Player player;

    private float nextUsableTime;

    private static readonly int DoSkill1Hash = Animator.StringToHash("doSkill1");
    private static readonly int DoSkill2Hash = Animator.StringToHash("doSkill2");
    private static readonly int DoSkill3Hash = Animator.StringToHash("doSkill3");

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


//using UnityEngine;
//using UnityEngine.InputSystem;

//public class SkillCaster : MonoBehaviour
//{
//    [Header("Config")]
//    [SerializeField] private float cooldown = 5f;

//    [Tooltip("이 스킬이 재생할 Animator Trigger 이름 (예: doSkill1, doSkill2...)")]
//    [SerializeField] private string animatorTrigger = "doSkill1";

//    [Header("Refs")]
//    [SerializeField] private SkillUI ui;

//    // Player에서 Animator를 가져오던 기존 방식 유지 (없다면 직접 Animator 참조로 바꿔도 됨)
//    private Player player;

//    private float nextUsableTime;

//    private void Awake()
//    {
//        player = GetComponent<Player>();
//    }

//    private void Start()
//    {
//        if (ui != null)
//        {
//            ui.ResetCooldown();
//        }
//    }

//    // PlayerInput의 Send Messages를 이 컴포넌트에 연결해두었다면 그대로 동작함.
//    // 하지만 이제는 SkillManager가 대신 받아 호출하는 게 주 경로.
//    public void OnSkill(InputAction.CallbackContext ctx)
//    {
//        if (!ctx.performed) return;
//        TryCast();
//    }

//    /// <summary>외부에서 호출: 쿨다운 체크 → 애니메이션 → UI 쿨다운 시작</summary>
//    public bool TryCast()
//    {
//        if (Time.time < nextUsableTime) return false; // 쿨다운 중

//        // 실제 스킬 발동 로직(데미지/이펙트 등)을 여기 또는 별도 함수로
//        Debug.Log($"Skill Cast! ({animatorTrigger})");

//        if (player != null && player.animator != null && !string.IsNullOrEmpty(animatorTrigger))
//        {
//            player.animator.SetTrigger(animatorTrigger);
//        }

//        nextUsableTime = Time.time + cooldown;
//        if (ui != null)
//        {
//            ui.BeginCooldown(cooldown);
//        }
//        return true;
//    }
//}