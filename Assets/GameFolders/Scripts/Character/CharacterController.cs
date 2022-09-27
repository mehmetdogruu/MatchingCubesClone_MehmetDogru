using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterController : MonoBehaviour ,IInteractor
{
    //A�a��daki 7 sat�r bize odininspectorun sa�lad��� olanak.Boxgrouplar olu�turarak state patternindeki scriptleri charactercontroller scriptine tan�tt�m.
    //Charactercontroller scripti IInteractor sayesinde etkile�ime ge�ti�i her obje taraf�ndan �a��r�labilecek.
    [SerializeReference, BoxGroup("Idle", false), HorizontalGroup("Idle/Group")] public State IdleState;
    [SerializeReference, BoxGroup("Stack", false), HorizontalGroup("Stack/Group")] public StackState StackState;
    [SerializeReference, BoxGroup("Boost", false), HorizontalGroup("Boost/Group")] public BoostState BoostState;
    [SerializeReference, BoxGroup("Jump", false), HorizontalGroup("Jump/Group")] public JumpState JumpState;
    [SerializeReference, BoxGroup("Fnsh", false), HorizontalGroup("Fnsh/Group")] public FinishState FinishState;
    [SerializeReference, BoxGroup("Win", false), HorizontalGroup("Win/Group")] public State WinState;
    [SerializeReference, BoxGroup("Fail", false), HorizontalGroup("Fail/Group")] public State FailState;
    //Currentstate tutmam�n sebebi sadece karakterin �uan hangi state oldu�unu kontrol edebilmem i�in. oyuzden readonly.
    [ShowInInspector, ReadOnly, BoxGroup("States", false)] public State CurrentState { get; private set; }

    //Karakterin di�er scriptler taraf�ndan ula�mak isteyebilece�im komponentlerini ekledim.
    public Rigidbody Rigidbody { get; private set; }
    public Interactor Interactor { get; private set; }
    public CharacterMovement Movement { get; private set; }
    public StackController StackController { get; private set; }
    public StackVisualController StackVisualController { get; private set; }
    public TrailController TrailController { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Interactor = GetComponentInChildren<Interactor>();
        Movement = GetComponent<CharacterMovement>();
        StackController = GetComponent<StackController>();
        StackVisualController = GetComponent<StackVisualController>();
        TrailController = GetComponent<TrailController>();
        SetState(IdleState);//oyuna ba�lad���nda �dle statette
    }
    //B�t�n projede �al��an tek fixedupdate.State patternin temeli.
    //Tek bir fixedupdate ya da update �al��mas� performans a��s�ndan �okomelli ��nk� bu fonksiyonlar her framede �al���r.Birden �ok scriptte ayn� anda �al��an update ve fixeduptade fonksiyonlar� sisteme gereksiz y�k bindirir.
    private void FixedUpdate()
    {
        CurrentState?.OnStateFixedUpdate(this);
    }
    //Statae pattern uygulamas�nda state de�i�iminde �nce bulunan statten ��k�lmas� i�in yaz�ld�.
    public void ExitState()
    {
        CurrentState?.StateExit(this);
    }
    //�nce bulunan stateten ��kart�p de�i�ken olarak verilen newstate stateini aktif hale getirir.
    public void SetState(State newState)
    {
        ExitState();
        CurrentState = newState;
        CurrentState.StateEnter(this);
    }
}
