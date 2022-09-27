using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class StackVisualController : MonoBehaviour
{
    private StackController _stackController;
    private CharacterController _controller;
    [SerializeField] private Transform _stackParent;
    [SerializeField] private float _distance;
    [SerializeField] private Transform _model;
    public List<CollectableController> stackedObjects = new();
    private int _matchCount;

    private void Awake()
    {
        _stackController = GetComponent<StackController>();
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _stackController.OnStackAdded += UpdateVisualAdded;
        _stackController.OnStackAdded += UpdateVisualAddedForMatch;
        _stackController.OnStackLost += UpdateVisualLost;
    }

    private void OnDisable()
    {
        _stackController.OnStackAdded -= UpdateVisualAdded;
        _stackController.OnStackAdded -= UpdateVisualAddedForMatch;
        _stackController.OnStackLost -= UpdateVisualLost;
    }

    private void UpdateVisualAdded(CollectableController obj)
    {
        stackedObjects.Add(obj);
        var objTransform = obj.transform;
        objTransform.DOScale(Vector3.one * .5f, .2f).OnComplete(() => objTransform.DOScale(Vector3.one, .2f));
        objTransform.SetParent(_stackParent);
        objTransform.DOLocalMove(Vector3.up * (-_stackController.Stack + 1 * _distance), .2f);
        _stackParent.DOLocalMove(-(Vector3.up * (-_stackController.Stack + 1 * _distance)), .2f);
        _model.DOLocalJump(Vector3.up * (.5f + _stackController.Stack), 2, 1, .3f).OnComplete(() => _model.DOLocalMove(Vector3.up * (1 + _stackController.Stack), .3f));
        objTransform.localRotation = Quaternion.identity;
    }

    private void UpdateVisualLost()
    {
        //var obj = stackedObjects[^1];
        //obj.transform.SetParent(null);
        _stackParent.DOLocalMove(-(Vector3.up * (-_stackController.Stack + 1 * _distance)), .2f);
        if (_stackController.Stack == 0) return;
        _model.DOLocalMove(Vector3.up * (1 + _stackController.Stack), .2f);
        stackedObjects[^1].ChangeTrail(_controller);
    }

    private void UpdateVisualAddedForMatch(CollectableController obj)
    {
        if (!CheckMatch()) return;
        StartCoroutine(MatchingWhenStackAddedEvent());
    }

    private bool CheckMatch()  //sadece yeni stack eklendiðinde kendinden önceki iki stackin type ile son eklenen type ý karsýlastýrýr aynýysa true döner
    {
        if (stackedObjects.Count < 3) return false;
        if (stackedObjects[^1].color == stackedObjects[^2].color && stackedObjects[^2].color == stackedObjects[^3].color) return true;
        else return false;
        

    }

    private IEnumerator MatchingWhenStackAddedEvent()
    {
        yield return new WaitForSeconds(.1f);
        List<CollectableController> deletedObjects = new();
        for (int i = stackedObjects.Count - 3; i < stackedObjects.Count; i++)
        {
            var tempObj = stackedObjects[i];
            deletedObjects.Add(tempObj);
        }

        foreach (var item in deletedObjects)
        {
            item.DOKill();
            stackedObjects.Remove(item);
            Destroy(item.gameObject, .25f);
            _stackController.LoseStack();
        }
        deletedObjects.Clear();
    }

    public void CheckMatchForAllList()
    {
        if (stackedObjects.Count < 3) return;
        for (int i = stackedObjects.Count - 1; i > 1; i--)
        {
            if (stackedObjects[i].color == stackedObjects[i-1].color && stackedObjects[i].color == stackedObjects[i-2].color)
            {
                Matched(new List<CollectableController> { stackedObjects[i], stackedObjects[i - 1], stackedObjects[i - 2] });
                break;
            }
        }

    }

    private void Matched(List<CollectableController> newList)
    {
        foreach (var item in newList)
        {
            item.DOKill();
            stackedObjects.Remove(item);
            _stackController.LoseStack();
            Destroy(item.gameObject,.25f);
        }

        stackedObjects[^1].ChangeTrail(_controller);
    }

}
