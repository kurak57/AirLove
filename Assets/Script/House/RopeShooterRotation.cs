using UnityEngine;
using UnityEngine.InputSystem;

public class RopeShooterRotation : MonoBehaviour
{
    public InputActionReference activeRotationAction;

    [Header("Rotation Setting")]
    public float rotationSpeed = 90f;
    [SerializeField]private float minRotationAngle = -30f;
    [SerializeField] private float maxRotationAngle = 45f;

    private float rotationInput = 0f;
    private float rotationMultiplier = 1f;
    private float currentZRotation = 0f; 

    void Awake()
    {
        if (transform.parent != null)
        {

            if (transform.parent.name == "Top")
            {
                rotationMultiplier = -1f;
            }
        }
    }

    public void SetRotationAction(InputActionReference rotationAction)
    {
        this.activeRotationAction = rotationAction;
    }
    private void OnEnable()
    {
        activeRotationAction.action.Enable();
        activeRotationAction.action.performed += OnMove;
        activeRotationAction.action.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        activeRotationAction.action.Disable();
        activeRotationAction.action.performed -= OnMove;
        activeRotationAction.action.canceled -= OnMoveCanceled;
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