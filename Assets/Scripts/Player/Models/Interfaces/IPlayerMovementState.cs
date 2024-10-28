public interface IPlayerMovementState
{
    void FloatingMovementDetect();
    void HorizontalMovementDetect();
    void JumpMovementDetect();
    void FaceSideDetect();
    void ApplyGravity();
}
