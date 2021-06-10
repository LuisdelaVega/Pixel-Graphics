using UnityEngine;

public abstract class MovementModifier : MonoBehaviour, IMovementModifier
{
  [Header("References")]
  [SerializeField] protected MovementHandler movementHandler = null;

  #region IMovementModifier Params
  public Vector3 Value { get; protected set; }
  #endregion

  #region Unity Events
  private void OnEnable() => movementHandler.AddModifier(this);
  private void OnDisable() => movementHandler.RemoveModifier(this);

  private void Update() => ProcessMovement();
  #endregion

  #region Abstracts Methods
  protected abstract void ProcessMovement();
  #endregion
}
