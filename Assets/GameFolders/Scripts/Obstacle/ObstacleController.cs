using UnityEngine;

public class ObstacleController : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; private set; }

    private Rigidbody _rigidbody;

    private float _horizontalForce = 300;
    private float _verticalForce = 500;

    private void Awake()
    {
        IsInteractable = true;
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Interact(IInteractor interactor)
    {
        Debug.Log("Ben Obstacle");
        var controller = (CharacterController)interactor;
        if (controller.CurrentState == controller.BoostState)
        {
            FlyingUp();
            return;
        }
        if (controller.StackController.Stack <= 0) controller.SetState(controller.FailState);
        else
        {
            controller.StackController.LoseStack();
            controller.StackVisualController.stackedObjects[^1].transform.SetParent(null);
            controller.StackVisualController.stackedObjects.RemoveAt(controller.StackVisualController.stackedObjects.Count-1);
            controller.StackVisualController.stackedObjects[^1].ChangeTrail(controller);
        }
    }

    private void FlyingUp()
    {
        Vector3 force = Random.insideUnitCircle * _horizontalForce;
        force = new Vector3(force.x, Random.value * _verticalForce, force.y);
        _rigidbody.AddForce(force);
    }
}
