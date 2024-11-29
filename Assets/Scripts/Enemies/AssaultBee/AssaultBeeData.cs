using UnityEngine;

namespace Enemies.AssaultBee
{
    public class AssaultBeeData : MonoBehaviour
    {
        #region Movement Fields
            public bool IsFacingLeft = true;
        #endregion

        #region Default Movement Fields
            public Vector2 LeftPoint, RightPoint;
            public float Speed = 2f;
        #endregion

        #region Attack Fields
            public float AttackCD = 2f;
            public float PrecastSpeedMultiplier = 0.3f;
            public float AttackSpeedMultiplier = 2.5f;
        #endregion

        #region Detection Fields
            public float DetectionDistance = 7f;
        #endregion
    }
}