using UnityEngine;

public class TestMonster : MonoBehaviour
{
    public TestMonsterAI monsterAI;
    private void FixedUpdate()
    {
        monsterAI.StateUpdate();
    }
    public void Move(Vector3 move)
    {
        transform.position = Vector3.MoveTowards(transform.position, move, 0.1f);
    }
}
