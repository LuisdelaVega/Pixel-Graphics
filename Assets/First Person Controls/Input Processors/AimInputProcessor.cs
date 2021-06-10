using UnityEngine;
using UnityEngine.InputSystem;

// TODO-LUIS Rename this to AimInputProcessor
public class AimInputProcessor : MonoBehaviour
{
  private Transform m_transform;

  [Header("Settings")]
  [SerializeField, Range(0f, 1f)] private float sensitivity = 0.15f;

  private float xRotation = 0f;
  private float yRotation = 0f;
  private Vector2 playerInput = Vector2.zero;

  private void Awake() => m_transform = transform;

  private void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
    yRotation = m_transform.localRotation.eulerAngles.y;
  }

  private void Update() => PerformAction();

  // Called by the Player Input component
  public void InputActionHandler(InputAction.CallbackContext value) => playerInput = value.ReadValue<Vector2>();

  private void PerformAction()
  {
    yRotation += playerInput.x * sensitivity;
    xRotation -= playerInput.y * sensitivity;
    xRotation = Mathf.Clamp(xRotation, -85, 80);

    m_transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
  }
}
