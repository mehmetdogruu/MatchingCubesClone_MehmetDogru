using DG.Tweening;

public class FailState : State
{
    protected override void OnStateEnter(CharacterController controller)
    {
        controller.DOKill();
        controller.Rigidbody.isKinematic = true;
    }
}
