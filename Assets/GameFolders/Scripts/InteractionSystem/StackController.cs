using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class StackController : MonoBehaviour
{
    public event Action<CollectableController> OnStackAdded;
    public event Action OnStackLost;


    [ShowInInspector, ReadOnly, PropertyOrder(-1)] public int Stack { get; private set; }

    public void AddStack(CollectableController obj)
    {
        Stack++;
        OnStackAdded?.Invoke(obj);
    }

    public void LoseStack()
    {
        Stack --;
        OnStackLost?.Invoke();
    }

}
