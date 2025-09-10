using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private string doSkillHash;

    [SerializeField]
    private SkillUI ui;

    [SerializeField]
    private Player player;

    private float nextUsableTime;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    private void Start()
    {
        if (ui != null)
        {
            ui.ResetCooldown();
        }
    }
    public bool TryCast()
    {
        if (Time.time < nextUsableTime)
        {
            return false; // 쿨다운 중
        }
        Debug.Log($"Skill Cast! ({doSkillHash})");

        if (player.animator != null)
        {
            player.animator.SetTrigger(doSkillHash);
        }
        nextUsableTime = Time.time + cooldown;

        if (ui != null)
        {
            ui.BeginCooldown(cooldown);
        }
        return true;
    }
}
