public interface IMovementState
{
    public bool AttackEnabled { get; }
    public void Enter();
    public void Update();
}