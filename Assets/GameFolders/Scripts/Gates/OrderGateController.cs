using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class OrderGateController : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; private set; }
    private void Awake()
    {
        IsInteractable = true;
    }

    public void Interact(IInteractor interactor)
    {
        Debug.Log("Ben OrderGate");
        var controller = (CharacterController)interactor;
        OrderSort(controller.StackVisualController.stackedObjects,controller);
        for (int i = 0; i <= controller.StackVisualController.stackedObjects.Count/3; i++)
        {
            DOVirtual.DelayedCall(.5f, controller.StackVisualController.CheckMatchForAllList, false);
            if (i == 3) controller.SetState(controller.BoostState);
        }
        
    }

    private void OrderSort(List<CollectableController> stackedObjects,CharacterController controller)
    {
        for (int i = 0; i < stackedObjects.Count-1; i++)
        {
            for (int j = i+1; j < stackedObjects.Count; j++)
            {
                if (stackedObjects[i].color.Equals(stackedObjects[j].color))
                {
                    (stackedObjects[i + 1].transform.localPosition, stackedObjects[j].transform.localPosition) = (stackedObjects[j].transform.localPosition, stackedObjects[i + 1].transform.localPosition);
                    (stackedObjects[i + 1], stackedObjects[j]) = (stackedObjects[j], stackedObjects[i + 1]);
                    break;
                }
            }
        }
    }
    
}
