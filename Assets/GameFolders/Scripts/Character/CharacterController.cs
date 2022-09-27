using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterController : MonoBehaviour ,IInteractor
{
    //Aþaðýdaki 7 satýr bize odininspectorun saðladýðý olanak.Boxgrouplar oluþturarak state patternindeki scriptleri charactercontroller scriptine tanýttým.
    //Charactercontroller scripti IInteractor sayesinde etkileþime geçtiði her obje tarafýndan çaðýrýlabilecek.
    [SerializeReference, BoxGroup("Idle", false), HorizontalGroup("Idle/Group")] public State IdleState;
    [SerializeReference, BoxGroup("Stack", false), HorizontalGroup("Stack/Group")] public StackState StackState;
    [SerializeReference, BoxGroup("Boost", false), HorizontalGroup("Boost/Group")] public BoostState BoostState;
    [SerializeReference, BoxGroup("Jump", false), HorizontalGroup("Jump/Group")] public JumpState JumpState;
    [SerializeReference, BoxGroup("Fnsh", false), HorizontalGroup("Fnsh/Group")] public FinishState FinishState;
    [SerializeReference, BoxGroup("Win", false), HorizontalGroup("Win/Group")] public State WinState;
    [SerializeReference, BoxGroup("Fail", false), HorizontalGroup("Fail/Group")] public State FailState;
    //Currentstate tutmamýn sebebi sadece karakterin þuan hangi state olduðunu kontrol edebilmem için. oyuzden readonly.
    [ShowInInspector, ReadOnly, BoxGroup("States", false)] public State CurrentState { get; private set; }

    //Karakterin diðer scriptler tarafýndan ulaþmak isteyebileceðim komponentlerini ekledim.
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
        SetState(IdleState);//oyuna baþladýðýnda ýdle statette
    }
    //Bütün projede çalýþan tek fixedupdate.State patternin temeli.
    //Tek bir fixedupdate ya da update çalýþmasý performans açýsýndan çokomelli çünkü bu fonksiyonlar her framede çalýþýr.Birden çok scriptte ayný anda çalýþan update ve fixeduptade fonksiyonlarý sisteme gereksiz yük bindirir.
    private void FixedUpdate()
    {
        CurrentState?.OnStateFixedUpdate(this);
    }
    //Statae pattern uygulamasýnda state deðiþiminde önce bulunan statten çýkýlmasý için yazýldý.
    public void ExitState()
    {
        CurrentState?.StateExit(this);
    }
    //Önce bulunan stateten çýkartýp deðiþken olarak verilen newstate stateini aktif hale getirir.
    public void SetState(State newState)
    {
        ExitState();
        CurrentState = newState;
        CurrentState.StateEnter(this);
    }
}
