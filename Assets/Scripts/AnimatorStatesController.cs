using UnityEngine;
using System.Collections;

public class AnimatorStatesController
{
    public Animator animator;

    public AnimatorStatesController(Animator _Animator) { animator = _Animator; }

    public void SetActionType(float _State) { animator.SetFloat("actionType", _State); }
    public float GetActionType() { return animator.GetFloat("actionType"); }
    public void SetFacingDirection(float _Direction) { animator.SetFloat("facingDirection", _Direction); }
    public float GetFacingDirection() { return animator.GetFloat("facingDirection"); }

}
