using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInputProcessor : MovementModifier
{
  #region Params
  // References
  [SerializeField] private CharacterController controller = null;
  [SerializeField] private Transform POVCameraTransform = null;

  [Header("Settings")]
  [SerializeField] private float movementSpeed = 5f;
  [SerializeField] private float acceleration = 0.1f;

  // Private
  private float currentSpeed = 0f;
  private Vector3 previousVelocity = Vector3.zero;
  private Vector2 playerInput = Vector2.zero;
  #endregion

  // Caled by the Player Input component
  public void InputActionHandler(InputAction.CallbackContext value) => playerInput = value.ReadValue<Vector2>();

  #region [Overrides] - MovementModifier
  protected override void ProcessMovement()
  {
    float targetSpeed = movementSpeed * playerInput.magnitude;

    currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

    Vector3 forward = POVCameraTransform.forward;
    Vector3 right = POVCameraTransform.right;

    forward.y = 0f;
    right.y = 0f;

    forward.Normalize();
    right.Normalize();

    Vector3 movementDirection;

    if (targetSpeed != 0f)
      movementDirection = forward * playerInput.y + right * playerInput.x;
    else
      movementDirection = previousVelocity.normalized;

    Value = movementDirection * currentSpeed;
    previousVelocity = new Vector3(controller.velocity.x, 0f, controller.velocity.z);
    currentSpeed = previousVelocity.magnitude;
  }
  #endregion
}
