public interface IAttackState
{
    public bool LockAnimation { get; }
    public void Enter();
    public void Update();
    public void Knockbacked();
}