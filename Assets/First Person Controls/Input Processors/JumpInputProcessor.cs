using UnityEngine;

public class JumpInputProcessor : MovementModifier
{
  #region Params
  [SerializeField] protected CharacterController controller = null;

  [Header("Settings")]
  [SerializeField] private float mass = 1f;
  [SerializeField, Tooltip("Dampens the force")] private float drag = 5f;
  [SerializeField] private float jumpSpeed = 20f;

  private bool wasGroundedLastFrame;
  #endregion

  public void AddForce(Vector3 force) => Value += force / mass;

  // Called by the Player Input component
  public void InputActionHandler()
  {
    if (!controller.isGrounded) return;

    AddForce(new Vector3(0f, jumpSpeed, 0f));
  }

  #region [Overrides] - MovementModifier
  protected override void ProcessMovement()
  {
    if (!wasGroundedLastFrame && controller.isGrounded)
    {
      Value = new Vector3(Value.x, 0f, Value.z);
    }

    wasGroundedLastFrame = controller.isGrounded;

    if (Value.magnitude < 0.2f)
    {
      Value = Vector3.zero;
    }

    Value = Vector3.Lerp(Value, Vector3.zero, drag * Time.deltaTime);
  }
  #endregion
}
