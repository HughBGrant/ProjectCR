public abstract class StateBase
{
    public StateBase(Enemy enemy)
    {
        this.enemy = enemy;
    }
    protected Enemy enemy;

    public abstract void OnEnterState();
    public abstract void OnUpdate();
    public abstract void OnExitState();
}