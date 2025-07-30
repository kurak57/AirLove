// PlayerInteraction.cs (MODIFIED)
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Referensi ke handler input milik pemain ini
    private PlayerInputHandler playerInputHandler;

    private GunShooter activeGunShooterScript;
    private GunShooterRotation activeGunShooterRotationScript;
    private HouseMover activeHouseMoverScript;
    private RopeShooter activeRopeShooterScript;
    private RopeShooterRotation activeRopeShooterRotation;

    // Dapatkan referensi ke PlayerInputHandler saat mulai
    private void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        if (playerInputHandler == null)
        {
            Debug.LogError("PlayerInteraction membutuhkan PlayerInputHandler pada GameObject yang sama!", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // --- Interaksi dengan Gun Panel ---
        GunPanelController gunPanel = other.GetComponent<GunPanelController>();
        if (gunPanel != null)
        {
            Debug.Log($"Masuk ke panel {other.name}! Menyiapkan shooter untuk pemain ini.");

            activeGunShooterScript = gunPanel.targetGunShooterScript;
            activeGunShooterRotationScript = gunPanel.targetGunRotationScript;

            // Berikan input action dari pemain ini ke skrip senjata SEBELUM mengaktifkannya
            if (activeGunShooterScript != null)
            {
                activeGunShooterScript.SetFireAction(playerInputHandler.fireAction);
                activeGunShooterScript.enabled = true;
            }
            if (activeGunShooterRotationScript != null)
            {
                activeGunShooterRotationScript.SetMoveAction(playerInputHandler.moveAction);
                activeGunShooterRotationScript.enabled = true;
            }
        }
        
        // (Logika untuk HousePanelController dan RopePanelController tetap sama)
        HousePanelController housePanel = other.GetComponent<HousePanelController>();
        if (housePanel != null)
        {
            Debug.Log($"Masuk ke panel rumah {other.name}! Mengaktifkan pergerakan rumah.");
            activeHouseMoverScript = housePanel.targetHouseMoverScript;
            if (activeHouseMoverScript != null)
            {
                activeHouseMoverScript.SetMoveAction(playerInputHandler.moveAction);
                activeHouseMoverScript.enabled = true;
            }
        }

        RopePanelController ropePanel = other.GetComponent<RopePanelController>();
        if (ropePanel != null)
        {
            Debug.Log($"Masuk ke panel rope {other.name}! Mengaktifkan Rope Shooter.");
            activeRopeShooterScript = ropePanel.targetRopeShooterScript;
            activeRopeShooterRotation = ropePanel.targetRopeShooterRotationScript;
            if (activeRopeShooterScript != null)
            {
                activeRopeShooterScript.SetFireAction(playerInputHandler.fireAction);
                activeRopeShooterScript.enabled = true;
            }


            if (activeRopeShooterRotation != null)
            {
                activeRopeShooterRotation.SetRotationAction(playerInputHandler.moveAction);
                activeRopeShooterRotation.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // --- Meninggalkan Gun Panel ---
        GunPanelController gunPanel = other.GetComponent<GunPanelController>();
        if (gunPanel != null && gunPanel.targetGunShooterScript == activeGunShooterScript)
        {
            Debug.Log($"Keluar dari panel {other.name}! Menonaktifkan shooter.");

            if (activeGunShooterScript != null) activeGunShooterScript.enabled = false;
            if (activeGunShooterRotationScript != null) activeGunShooterRotationScript.enabled = false;

            activeGunShooterScript = null;
            activeGunShooterRotationScript = null;
        }
        
        // (Logika untuk HousePanelController dan RopePanelController tetap sama)
        HousePanelController housePanel = other.GetComponent<HousePanelController>();
        if (housePanel != null && housePanel.targetHouseMoverScript == activeHouseMoverScript)
        {
            Debug.Log($"Keluar dari panel rumah {other.name}! Menonaktifkan House Mover.");
            if (activeHouseMoverScript != null) activeHouseMoverScript.enabled = false;
            activeHouseMoverScript = null;
        }

        RopePanelController ropePanel = other.GetComponent<RopePanelController>();
        if (ropePanel != null && ropePanel.targetRopeShooterScript == activeRopeShooterScript)
        {
            Debug.Log($"Keluar dari panel {other.name}! Mononaktifkan rope shooter.");
            if (activeRopeShooterScript != null) activeRopeShooterScript.enabled = false;
            if (activeRopeShooterRotation != null) activeRopeShooterRotation.enabled = false;
            activeRopeShooterScript = null;
            activeRopeShooterRotation = null;
        }
    }
}