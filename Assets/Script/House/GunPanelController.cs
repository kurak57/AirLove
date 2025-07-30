using UnityEngine;

public class GunPanelController : MonoBehaviour
{
    [Header("Target yang Dikontrol")]
    [Tooltip("Seret ke sini script PlayerShooting dari Shooter yang sesuai.")]
    public GunShooter targetGunShooterScript;

    [Tooltip("Seret ke sini script ShooterRotation dari Shooter yang sesuai.")]
    public GunShooterRotation targetGunRotationScript;
}