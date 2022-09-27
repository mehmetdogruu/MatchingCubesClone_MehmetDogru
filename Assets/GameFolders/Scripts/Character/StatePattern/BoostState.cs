using DG.Tweening;

public class BoostState : State
{
    //State scriptinde bulunan virtualOnstateenter ve OnStateFixedUpdate fonksiyonlar�n� override eder.
    protected override void OnStateEnter(CharacterController controller)
    {
        controller.Movement.forwardSpeed = 20;
    }

    public override void OnStateFixedUpdate(CharacterController controller)
    {
        controller.Movement.Movement();
        controller.Movement.ClampX();
    }
}
