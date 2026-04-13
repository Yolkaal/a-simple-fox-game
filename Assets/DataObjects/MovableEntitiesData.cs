using UnityEngine;

[CreateAssetMenu(fileName = "MovableEntitiesData", menuName = "Scriptable Objects/MovableEntitiesData")]
public class MovableEntitiesData : ScriptableObject
{
    public float speed;
    public float jumpForce;
    public bool canDoubleJump;
    public bool canFly;
    public float flySpeed;
}
