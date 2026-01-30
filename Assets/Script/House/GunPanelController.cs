using UnityEngine;

public class GunPanelController : MonoBehaviour
{
    [Header("Shooter Target")]
    [Tooltip("Drag PlayerShooting script from specific shooter.")]
    public GunShooter targetGunShooterScript;

    [Tooltip("Drag ShooterRotation script from specific shooter.")]
    public GunShooterRotation targetGunRotationScript;
}