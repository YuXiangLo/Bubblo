using UnityEngine;

public class JumpSpiderData : MonoBehaviour
{
    #region Movement Fields
        public bool IsGrounded = true;
        public bool IsFacingLeft = true;
    #endregion

    #region Default Movement Fields
        public Vector2 LeftPoint, RightPoint;
        public float Speed = 2f;
    #endregion

    #region Jump Movement Fields
        public float JumpHeight = 1f;
        public float Gravity = 1f;
        public float FallingMultiplier = 2f;
    #endregion

    #region Detection Fields
        public float ToApproachDistance = 3f;
        public float ToJumpDistance = 2f;
        public float YDetectRange = 1f;
    #endregion
}