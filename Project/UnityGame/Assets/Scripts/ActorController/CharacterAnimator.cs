using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;
    public bool isAttacking;

    protected string horizontalMove = "Horizontal";
    protected string verticalMove = "Vertical";
    protected string attackAnimatorName = "TriNormalAttack";
    protected string isCombatState = "IsCombatState";
    protected string comboCount = "Combo";
    protected string isMove = "IsMove";
    protected string isDash = "IsDash";
    protected string isJump = "IsJump";
    protected int currentComboCount = 0;
    public int comboMax = 0;

    public AnimatorStateInfo CurrentSatetInfo
    {
        get
        {
            return animator.GetCurrentAnimatorStateInfo(0);
        }
    }
    public int CurrentCombo
    {
        get
        {
            return animator.GetInteger(comboCount);
        }
    }
    private void Awake()
    {
        if (animator == null)
        {
            animator = transform.GetComponentInChildren<Animator>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("CombatIdle") && currentComboCount == 3)
        {
            currentComboCount = 0;
            animator.SetBool(attackAnimatorName, false);
        }
    }
    public void AnimatorDoMove()
    {
        animator.SetBool(isMove, true);
    }
    /// <summary>
    /// 进行待机
    /// </summary>
    /// <param name="isForce">是否强制待机</param>
    public void AnimatorDoIdle(bool isForce = false)
    {
        if (isForce)
        {
            animator.SetBool(isMove, false);
            animator.SetInteger(comboCount, 0);
            animator.SetBool(attackAnimatorName, false);
        }
        else
        {
            animator.SetBool(isMove, false);
        }
    }
    /// <summary>
    /// 进行普攻
    /// </summary>
    public void AnimatorDoNormalAttack()
    {
        animator.SetTrigger(attackAnimatorName);
    }
    public void OnAnimatorJump()
    {
        animator.SetTrigger(isJump);
        animator.SetBool(attackAnimatorName, false);
    }
}
