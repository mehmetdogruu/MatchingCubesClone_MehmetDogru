using UnityEngine;

public class FinishLineController : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; private set; }
    private void Awake()
    {
        IsInteractable = true;
    }

    public void Interact(IInteractor interactor)
    {
        //IInteractor = characterController olmasýna raðmen yine de bir interface þeklinde deðiþken olarak atandýðý için, controller diye bir deðiþken oluþturup IInteractor ýcasting yöntemi ile characterController a dönüþtürüyorum ve sonra
        //characterControllerýn currentStete finishState ini atýyorum. 
        Debug.Log("Ben FinishLine");
        var controller = (CharacterController)interactor;
        controller.SetState(controller.FinishState);
    }
}
