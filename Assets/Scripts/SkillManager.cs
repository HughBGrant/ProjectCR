using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillManager : MonoBehaviour
{
    [Serializable]
    public class Slot
    {
        [Tooltip("눌렀을 때 매핑될 키 문자열 (예: q, w, e, r, 1, 2, 3) — 소문자 권장")]
        public string key = "q";

        [Tooltip("이 슬롯을 실제로 실행할 캐스터")]
        public SkillCaster caster;

        [Tooltip("UI 버튼 클릭으로 실행하고 싶다면, 버튼에서 이 슬롯 인덱스로 OnClickSlot 호출")]
        public bool enabled = true;
    }

    [Header("Slots (키 → 캐스터 매핑)")]
    [SerializeField] private List<Slot> slots = new();

    private Dictionary<string, int> keyToIndex;

    private void Awake()
    {
        keyToIndex = new Dictionary<string, int>();
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == null || slots[i].caster == null) continue;
            var k = (slots[i].key ?? "").Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(k)) continue;

            // 중복 키는 마지막 설정 우선
            keyToIndex[k] = i;
        }
    }

    // PlayerInput: Behavior = Send Messages 일 때 자동 호출
    private void OnSkill(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        // 어떤 키/컨트롤로 발동됐는지 확인
        string k = (ctx.control?.name ?? "").ToLowerInvariant();
        if (string.IsNullOrEmpty(k)) return;

        if (keyToIndex.TryGetValue(k, out int idx))
        {
            TryCastSlot(idx);
        }
        else
        {
            // 숫자 줄(1~9)처럼 키 이름이 "1","2" 형태로 오는 경우도 여기서 처리됨
            // 매핑이 없으면 무시
        }
    }

    /// <summary>UI 버튼용. 인스펙터에서 Button.onClick → SkillManager.OnClickSlot(index)</summary>
    public void OnClickSlot(int index)
    {
        TryCastSlot(index);
    }

    private void TryCastSlot(int index)
    {
        if (index < 0 || index >= slots.Count) return;
        var s = slots[index];
        if (s == null || !s.enabled || s.caster == null) return;

        //s.caster.TryCast(); // 내부에서 쿨다운/애니메이션/UI 처리
    }
}