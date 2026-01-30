// GunShooterRotation.cs (MODIFIED)
using UnityEngine;
using UnityEngine.InputSystem;

public class GunShooterRotation : MonoBehaviour
{
    [Header("Rotation Setting")]
    public float rotationSpeed = 90f;
    [SerializeField] private float minRotationAngle = -60f;
    [SerializeField] private float maxRotationAngle = 60f;

    private InputActionReference activeMoveAction;
    private float rotationInput = 0f;
    private float rotationMultiplier = 1f;
    private float currentZRotation = 0f;

    void Awake()
    {
        if (transform.parent != null && transform.parent.name == "BottomShooter")
        {
            rotationMultiplier = -1f;
        }
    }

    public void SetMoveAction(InputActionReference moveAction)
    {
        this.activeMoveAction = moveAction;
    }

    private void OnEnable()
    {
        if (activeMoveAction != null)
        {
            activeMoveAction.action.Enable();
            activeMoveAction.action.performed += OnMove;
            activeMoveAction.action.canceled += OnMoveCanceled;
        }
    }

    private void OnDisable()
    {
        if (activeMoveAction != null)
        {
            activeMoveAction.action.Disable();
            activeMoveAction.action.performed -= OnMove;
            activeMoveAction.action.canceled -= OnMoveCanceled;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>().y;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        rotationInput = 0f;
    }

    void Update()
    {
        float rotationAmount = rotationInput * rotationSpeed * rotationMultiplier * Time.deltaTime;
        currentZRotation += rotationAmount;
        currentZRotation = Mathf.Clamp(currentZRotation, minRotationAngle, maxRotationAngle);
        transform.localRotation = Quaternion.Euler(0f, 0f, currentZRotation);
    }
}