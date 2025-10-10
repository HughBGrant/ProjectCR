public class IdleState : StateBase
{
    public IdleState(Enemy enemy) : base(enemy) { }

    public override void OnEnterState()
    {
    }
    public override void OnUpdate()
    {
        if ((enemy.transform.position - enemy.targetPoint.position).magnitude < 10f)
        {

        }
    }
    public override void OnExitState()
    {
    }
}