using UnityEngine;
using System;
using DG.Tweening;

[Serializable]
public class JumpState : State
{
    [SerializeField] private float _jumpDistance;
    private bool _isJumped;

    protected override void OnStateEnter(CharacterController controller)
    {
        _isJumped = false;
        JumpToTheOtherSide(controller);
    }
    private void JumpToTheOtherSide(CharacterController controller)
    {
        _isJumped = true;
        controller.transform.DOJump(new Vector3(controller.transform.position.x,controller.transform.position.y,controller.transform.position.z + _jumpDistance), 5f, 1, 2f).OnComplete(() => controller.SetState(controller.StackState));
    }
}
