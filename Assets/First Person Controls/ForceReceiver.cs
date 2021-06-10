using UnityEngine;

// [DEPRECATED]
public class ForceReceiver : MovementModifier
{
  [SerializeField] protected CharacterController controller = null;

  [Header("Settings")]
  [SerializeField] private float mass = 1f;
  [SerializeField, Tooltip("Dampens the force")] private float drag = 5f;

  private bool wasGroundedLastFrame;

  public void AddForce(Vector3 force) => Value += force / mass;

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
}
