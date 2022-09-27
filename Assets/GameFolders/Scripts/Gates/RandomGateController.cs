using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class RandomGateController : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; private set; }
    private void Awake()
    {
        IsInteractable = true;
    }
    public void Interact(IInteractor interactor)
    {
        Debug.Log("Ben RandomGate");
        var controller = (CharacterController)interactor;
        ShuffleList(controller.StackVisualController.stackedObjects);
        DOVirtual.DelayedCall(.3f, controller.StackVisualController.CheckMatchForAllList, false);
    }

    private void ShuffleList(List<CollectableController> stackList)
    {
        //CharacterConrrollerda tuttuðum stackVisualController scriptinde bulunan stackedObjects listini aldým.
        // StackedObjectsin eleman sayýsý kadar dönecek for yazdým.
        for (int i = 0; i < stackList.Count; i++)
        {
            var temp = stackList[i];        //Bu döngüde öncelikle bir temp objesi oluþturup ona stackedObjects [i] yi atadým.Bu sayede hiçbiþi yapýlmamýþ stackedList{i] yi elimde tuttum.

            var firstObjectPos = stackList[i].transform.position; // stackedObjects{i} nin pozisyon bilgisini firstObjectPos da tuttum.
            var randomValue = Random.Range(i, stackList.Count);
            var secondObjectPos = stackList[randomValue].transform.position;// stackedObjects(randomvalue ncü indexi)nin pozisyon bilgisini secondObjectPos da tuttum.
            stackList[i].transform.position = secondObjectPos;// stackObjects(i)nin pozisyonunu secondobjpos yani stackedObject{randomValue} nun pozisyonuna eþitledim.Yani stackedObjects{i] diðerinin pozisyonuna geçti.
            stackList[i] = stackList[randomValue];//pozisyon deðiþikliðini yaptýktan sonra objeleri deðiþtirdim.
            stackList[randomValue].transform.position = firstObjectPos;//stackedObjects{randomvalue} pozisyonunu firstobjectpos aeþitledim.
            stackList[randomValue] = temp;//stackedObjects{randomValue} ye ilk satýrdaki temp i yani stackedObject{i]yi atadým.
        }
    }
}
