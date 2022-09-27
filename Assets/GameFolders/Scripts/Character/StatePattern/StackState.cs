
public class StackState : State
{
    protected override void OnStateEnter(CharacterController controller)
    {
        controller.Movement.forwardSpeed = 10;
    }

    public override void OnStateFixedUpdate(CharacterController controller)
    {
        controller.Movement.Movement();
        controller.Movement.ClampX();
    }

    protected override void OnStateExit(CharacterController controller)
    {
        controller.Movement.forwardSpeed = 0;
    }
}
