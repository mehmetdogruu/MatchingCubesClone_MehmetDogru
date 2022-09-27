using System;

[Serializable]
public abstract class State
{
    // Abstract class ba�ka classalra kal�t�m verebilen classt�r.Monobeheavior olmad��� i�in unity sahnesine at�lamaz.State patterndeki her bir statein temel scripti burdad�r.
    //Bu scripte g�re her statein bir OnstateEnter bir OnstateExit birde OnstateFixedUpdate i vard�r.Bu 3 fonksiyon characterController scriptinde yazd���mm setState ExitState ve FixedUpdate i�inde �a��r�l�r.
    //���r�lma y�ntemi altta belirtti�im 2 eventin invokelanmas�yla olur.
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
