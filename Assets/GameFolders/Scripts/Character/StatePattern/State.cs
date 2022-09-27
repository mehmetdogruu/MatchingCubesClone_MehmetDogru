using System;

[Serializable]
public abstract class State
{
    // Abstract class baþka classalra kalýtým verebilen classtýr.Monobeheavior olmadýðý için unity sahnesine atýlamaz.State patterndeki her bir statein temel scripti burdadýr.
    //Bu scripte göre her statein bir OnstateEnter bir OnstateExit birde OnstateFixedUpdate i vardýr.Bu 3 fonksiyon characterController scriptinde yazdýðýmm setState ExitState ve FixedUpdate içinde çaðýrýlýr.
    //Çðýrýlma yöntemi altta belirttiðim 2 eventin invokelanmasýyla olur.
    public event Action<CharacterController> OnStateEntered;
    public event Action<CharacterController> OnStateExited;

    protected virtual void OnStateEnter(CharacterController controller) { }

    public virtual void OnStateFixedUpdate(CharacterController controller) { }

    protected virtual void OnStateExit(CharacterController controller) { }

    public void StateEnter(CharacterController controller)
    {
        OnStateEnter(controller);
        OnStateEntered?.Invoke(controller);
    }

    public void StateExit(CharacterController controller)
    {
        OnStateExit(controller);
        OnStateExited?.Invoke(controller);
    }
}
