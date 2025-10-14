using UnityEngine;

public enum AiState
{
    Create,
    Search,
    Move,
    Reset,
}
public class TestMonsterAI : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private TestMonster monster;
    [SerializeField]
    private Player player;
    bool isChasing;
    protected AiState aiState = AiState.Create;
    int index = 0;

    public void Init(TestMonster monster)
    {
        this.monster = monster;
    }
    public void StateUpdate()
    {
        switch (aiState)
        {
            case AiState.Create:
                Create();
                break;

            case AiState.Search:

                Search();
                break;

            case AiState.Move:

                Move();
                break;

            case AiState.Reset:

                Reset();
                break;
        }
    }

    protected void Create()
    {
        aiState = AiState.Search;
    }
    protected void Search()
    {
        float dis = Vector3.Distance(monster.transform.position, player.transform.position);

        isChasing = dis < 1f ? true : false;

        if (!isChasing)
        {
            dis = Vector3.Distance(monster.transform.position, wayPoints[index].position);

            if (dis < 0.1f)
            {
                if (index < wayPoints.Length - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
        }
        aiState = AiState.Move;
    }
    protected void Move()
    {
        if (!isChasing)
        {
            monster.Move(wayPoints[index].position);
        }
        else
        {
            float dis = Vector3.Distance(monster.transform.position, player.transform.position);

            if (dis < 3f)
            {
                monster.Move(player.transform.position);
            }
            else
            {
                Attack();
            }
        }
        aiState = AiState.Search;
    }
    protected void Attack()
    {
        //monster.SetAnimation("Attack");
    }
    protected void Reset()
    {
        aiState = AiState.Create;
    }
}
