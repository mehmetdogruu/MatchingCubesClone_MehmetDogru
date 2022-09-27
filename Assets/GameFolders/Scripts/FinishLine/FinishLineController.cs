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
        //IInteractor = characterController olmas�na ra�men yine de bir interface �eklinde de�i�ken olarak atand��� i�in, controller diye bir de�i�ken olu�turup IInteractor �casting y�ntemi ile characterController a d�n��t�r�yorum ve sonra
        //characterController�n currentStete finishState ini at�yorum. 
        Debug.Log("Ben FinishLine");
        var controller = (CharacterController)interactor;
        controller.SetState(controller.FinishState);
    }
}
