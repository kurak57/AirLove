using UnityEngine;

public class RopePanelController : MonoBehaviour
{
    [Header("Target yang Dikontrol")]
    [Tooltip("Seret ke sini script PlayerShooting dari Shooter yang sesuai.")]
    public RopeShooter targetRopeShooterScript;

    [Tooltip("Seret ke sini script ShooterRotation dari Shooter yang sesuai.")]
    public RopeShooterRotation targetRopeShooterRotationScript;
}