using UnityEngine;
using System;

public class CollectableController : MonoBehaviour,IInteractable
{
    public event Action OnCollected;

    public CollectableColor color;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    public bool IsInteractable { get; private set; }

    private void Awake()
    {
        IsInteractable = true;
    }
    public void Interact(IInteractor interactor)
    {
        Debug.Log("Ben Collectable");
        var controller = (CharacterController)interactor;
        Collect(controller);
    }

    private void Collect(CharacterController controller)
    {
        OnCollected?.Invoke();
        controller.StackController.AddStack(this);
        ChangeTrail(controller);
        SetInteractable(false);
    }

    public void SetInteractable(bool condition)
    {
        _collider.enabled = condition;
        _rigidbody.isKinematic = !condition;
        IsInteractable = condition;
    }

    public void ChangeTrail(CharacterController controller)
    {
        if (controller.StackVisualController.stackedObjects.Count > 0)
        {
            controller.TrailController.ChangeTrailColor(controller.StackVisualController.stackedObjects[^1].color);
            controller.TrailController.SetActiveTrail(true);
        }
        else
        {
            controller.TrailController.SetActiveTrail(false);
        }
    }
}
