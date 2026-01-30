using UnityEngine;

public class RopePanelController : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Drag PlayerShooting script from specific shooter.")]
    public RopeShooter targetRopeShooterScript;

    [Tooltip("Drag Shooter Rotation script from specific shooter.")]
    public RopeShooterRotation targetRopeShooterRotationScript;
}