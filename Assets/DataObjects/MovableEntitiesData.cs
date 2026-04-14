using UnityEngine;

namespace DataObjects
{
    [CreateAssetMenu(fileName = "MovableEntitiesData", menuName = "Scriptable Objects/MovableEntitiesData")]
    public class MoveableEntitiesData : ScriptableObject
    {
        public float speed;
        public float jumpForce;
        public bool isDoubleJumpUnlocked;
        public bool isFlyUnlocked;
        public float flySpeed;
        public float crouchSpeedModifier;
        public float crouchJumpForceModifier;
        public float airborneJumpForceModifier;
        public float airborneSpeedModifier;
        public float airborneDirectionThreshold;
    }
}
