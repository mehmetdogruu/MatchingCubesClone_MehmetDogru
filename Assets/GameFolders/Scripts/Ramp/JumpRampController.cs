using UnityEngine;

public class JumpRampController : MonoBehaviour,IInteractable
{
    public bool IsInteractable { get; private set; }
    private void Awake()
    {
        IsInteractable = true;
    }

    public void Interact(IInteractor interactor)
    {
        Debug.Log("Ben JumpRamp");
        var controller = (CharacterController)interactor;
        controller.SetState(controller.JumpState);
    }
}
