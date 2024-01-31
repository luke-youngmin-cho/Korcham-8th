using UnityEngine;

[CreateAssetMenu(fileName = "New SkillInfo", menuName = "ScriptableObjects/SkillInfo", order = 2)]
public class SkillInfo : ScriptableObject
{
    public Vector3 castingPoint1;
    public Vector3 castingPoint2;
    public Vector3 castingDirection;
    public float castingRadius;
    public float castingMaxDistance;
    public float castingArcAngle;
    public LayerMask castingMask;
    public int maxTargets;
    public float damageGain;
}
