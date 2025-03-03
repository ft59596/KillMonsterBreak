using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCloseAtk : StateMachineBehaviour
{
    public int nextComboIndex = 0;
    public bool reSetCombo;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (nextComboIndex > animator.GetComponentInParent<CharacterAnimator>().comboMax) {
            nextComboIndex = animator.GetComponentInParent<CharacterAnimator>().comboMax;
        }
        animator.SetInteger("Combo", nextComboIndex);
        animator.GetComponentInParent<CharacterAnimator>().isAttacking = true;
        do
        {
            if (nextComboIndex > -1)
            {
                AttackAbility attackAbility = animator.GetComponentInParent<AttackAbility>();
                if (nextComboIndex >= attackAbility.moveTime.Count)
                {
                    break;
                }
                attackAbility.currentMoveTime = attackAbility.moveTime[nextComboIndex];
            }

        } while (false);
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (reSetCombo)
        {
            animator.SetBool("TriNormalAttack", false);
            animator.SetInteger("Combo", 0);
            animator.GetComponentInParent<CharacterAnimator>().isAttacking = false;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("CombatIdle"))
        {
            animator.GetComponentInParent<CharacterAnimator>().isAttacking = false;
            animator.SetInteger("Combo", 0);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
