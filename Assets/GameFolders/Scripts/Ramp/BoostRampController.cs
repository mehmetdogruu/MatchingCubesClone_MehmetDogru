using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BoostRampController : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; private set; }
    private float _boostTime = 7f;
    private void Awake()
    {
        IsInteractable = true;
    }

    public void Interact(IInteractor interactor)
    {
        Debug.Log("Ben BoostRamp");
        var controller = (CharacterController)interactor;
        controller.SetState(controller.BoostState);
        StartCoroutine(StateChangeCo(controller));
    }
    
    IEnumerator StateChangeCo(CharacterController controller)
    {
        yield return new WaitForSeconds(_boostTime);
        controller.SetState(controller.StackState);
    }
}
