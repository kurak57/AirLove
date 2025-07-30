using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RopeShooter : MonoBehaviour
{
    [Header("Referensi Komponen")]
    public LineRenderer lineRenderer;
    public Transform hookSpriteTransform;

    [Header("Input Action")]
    private InputActionReference activeFireAction;

    [Header("Pengaturan Tali")]
    public float ropeLength = 10f;
    public float extendSpeed = 20f;
    public float pullSpeed = 5f;

    [Header("Pengaturan Fisika")]
    public LayerMask grabbableLayer;
    [Tooltip("Geser titik kaitan (hook) relatif terhadap pusat objek yang ditarik.")]
    public Vector2 hookOffset;

    private Coroutine activeCoroutine;
    private float directionMultiplier = 1f;
    
    private GameObject hookedObject = null;
    private bool isHooked = false;

    public void SetFireAction(InputActionReference fireAction)
    {
        this.activeFireAction = fireAction;
    }
    private void OnEnable()
    {
        activeFireAction.action.Enable();
        activeFireAction.action.performed += OnFirePressed;
        activeFireAction.action.canceled += OnFireReleased;
    }

    private void OnDisable()
    {
        activeFireAction.action.performed -= OnFirePressed;
        activeFireAction.action.canceled -= OnFireReleased;
        activeFireAction.action.Disable();
    }

    void Start()
    {
        if (transform.parent != null && transform.parent.name == "Top")
        {
            directionMultiplier = -1f;
        }

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
            lineRenderer.enabled = false;
        }

        // Baris yang menonaktifkan hook di awal telah dihapus.
        // Pastikan hookSpriteTransform aktif di scene sebelum menekan Play.
    }

    void Update()
    {
        if (isHooked && hookedObject != null)
        {
            hookedObject.transform.position = Vector2.MoveTowards(hookedObject.transform.position, transform.position, pullSpeed * Time.deltaTime);

            Transform hookedTransform = hookedObject.transform;
            Vector2 hookWorldPosition = hookedTransform.TransformPoint(hookOffset);
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(hookWorldPosition));

            if (hookSpriteTransform != null)
            {
                hookSpriteTransform.position = hookWorldPosition;
            }

            if (Vector2.Distance(hookedObject.transform.position, transform.position) < 0.1f)
            {
                Destroy(hookedObject);
                ReleaseHookedObject();
            }
        }
    }

    private void OnFirePressed(InputAction.CallbackContext context)
    {
        if (activeCoroutine != null) StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(ExtendLine());
    }

    private void OnFireReleased(InputAction.CallbackContext context)
    {
        if (isHooked)
        {
            ReleaseHookedObject();
        }
        else
        {
            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(RetractLine());
        }
    }

    private void ReleaseHookedObject()
    {
        isHooked = false;
        hookedObject = null;
        if (activeCoroutine != null) StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(RetractLine());
    }

    private IEnumerator ExtendLine()
    {
        lineRenderer.enabled = true;
        // Baris yang mengaktifkan hook di sini telah dihapus.

        float targetX = ropeLength * directionMultiplier;
        float currentX = lineRenderer.GetPosition(1).x;

        while (currentX != targetX && !isHooked)
        {
            currentX = Mathf.MoveTowards(currentX, targetX, extendSpeed * Time.deltaTime);
            Vector3 endPointLocal = new Vector3(currentX, 0, 0);
            lineRenderer.SetPosition(1, endPointLocal);

            if (hookSpriteTransform != null)
            {
                Vector3 hookWorldPosition = lineRenderer.transform.TransformPoint(endPointLocal);
                hookSpriteTransform.position = hookWorldPosition;
            }

            Vector3 endPointWorld = transform.TransformPoint(endPointLocal);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (endPointWorld - transform.position).normalized, Vector2.Distance(transform.position, endPointWorld), grabbableLayer);

            if (hit.collider != null)
            {
                isHooked = true;
                hookedObject = hit.collider.gameObject;

                Vector3 hitPointWorld = hit.point;
                lineRenderer.SetPosition(1, transform.InverseTransformPoint(hitPointWorld));

                if (hookSpriteTransform != null)
                {
                    hookSpriteTransform.position = hitPointWorld;
                }
                
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator RetractLine()
{
    Vector3 endPoint = lineRenderer.GetPosition(1);

    while (Vector3.Distance(endPoint, Vector3.zero) > 0.01f)
    {
        endPoint = Vector3.MoveTowards(endPoint, Vector3.zero, extendSpeed * Time.deltaTime);
        lineRenderer.SetPosition(1, endPoint);

        if (hookSpriteTransform != null)
        {
            Vector3 hookWorldPosition = lineRenderer.transform.TransformPoint(endPoint);
            hookSpriteTransform.position = hookWorldPosition;
        }
        yield return null;
    }
    
    // Cleanup akhir
    endPoint = Vector3.zero;
    lineRenderer.SetPosition(1, endPoint);
    lineRenderer.enabled = false;
    
    if (hookSpriteTransform != null)
    {
        // --- INI BARIS YANG DIPERBAIKI ---
        // Atur posisi dunia hook agar sama dengan posisi dunia titik awal tali.
        hookSpriteTransform.position = lineRenderer.transform.position;
    }
}
}