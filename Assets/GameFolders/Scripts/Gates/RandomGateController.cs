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
        //CharacterConrrollerda tuttu�um stackVisualController scriptinde bulunan stackedObjects listini ald�m.
        // StackedObjectsin eleman say�s� kadar d�necek for yazd�m.
        for (int i = 0; i < stackList.Count; i++)
        {
            var temp = stackList[i];        //Bu d�ng�de �ncelikle bir temp objesi olu�turup ona stackedObjects [i] yi atad�m.Bu sayede hi�bi�i yap�lmam�� stackedList{i] yi elimde tuttum.

            var firstObjectPos = stackList[i].transform.position; // stackedObjects{i} nin pozisyon bilgisini firstObjectPos da tuttum.
            var randomValue = Random.Range(i, stackList.Count);
            var secondObjectPos = stackList[randomValue].transform.position;// stackedObjects(randomvalue nc� indexi)nin pozisyon bilgisini secondObjectPos da tuttum.
            stackList[i].transform.position = secondObjectPos;// stackObjects(i)nin pozisyonunu secondobjpos yani stackedObject{randomValue} nun pozisyonuna e�itledim.Yani stackedObjects{i] di�erinin pozisyonuna ge�ti.
            stackList[i] = stackList[randomValue];//pozisyon de�i�ikli�ini yapt�ktan sonra objeleri de�i�tirdim.
            stackList[randomValue].transform.position = firstObjectPos;//stackedObjects{randomvalue} pozisyonunu firstobjectpos ae�itledim.
            stackList[randomValue] = temp;//stackedObjects{randomValue} ye ilk sat�rdaki temp i yani stackedObject{i]yi atad�m.
        }
    }
}
