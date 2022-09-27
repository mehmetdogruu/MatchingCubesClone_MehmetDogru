
public interface IInteractable 
{
    bool IsInteractable { get; }
    void Interact(IInteractor interactor);
}
