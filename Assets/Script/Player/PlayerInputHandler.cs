// PlayerInputHandler.cs
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Weapon Input Actions")]
    [Tooltip("Aksi tembak untuk pemain ini.")]
    public InputActionReference fireAction;

    [Tooltip("Aksi gerak (rotasi) untuk pemain ini.")]
    public InputActionReference moveAction;
}