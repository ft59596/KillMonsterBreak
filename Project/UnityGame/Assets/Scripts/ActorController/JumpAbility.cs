using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : CharacterAbility
{
    public Transform transBody;
    private CharacterController characterController;
    /// <summary>
    /// 跳跃冷却时间
    /// </summary>
    [Header("跳跃冷却时间")]
    public float jumpCoolTime = 0;
    /// <summary>
    /// 当前冷却时间
    /// </summary>
    private float curJumpCoolTime = 0;
    [Header("跳跃距离")]
    public float jumpDistance = 0.5f;
    /// <summary>
    /// 当前跳跃距离
    /// </summary>
    private float curJumpDistance = 0;
    /// <summary>
    /// 跳跃速度
    /// </summary>
    [Header("跳跃速度")]
    public float jumpSpeed = 10;
    /// <summary>
    /// 是否开始跳跃
    /// </summary>
    [Header("是否开始跳跃")]
    public bool startJump;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (transBody == null)
        {
            transBody = transform.GetChild(0);
        }
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();
       
        if (curJumpCoolTime >= 0)
        {
            curJumpCoolTime -= Time.deltaTime;
        }
        if (characterBase.characterAnimator.isAttacking) return;
        if (Input.GetKeyDown(KeyCode.Space) && curJumpCoolTime < 0 && !startJump)
        {
            Debug.Log("跳跃");
            curJumpCoolTime = jumpCoolTime;
            curJumpDistance = jumpDistance;
            characterBase.StopAllAbility(new List<CharacterAbility>() { this });
            characterBase.characterAnimator.OnAnimatorJump();
            startJump = true;
        }
        if (startJump && curJumpDistance > 0)
        {
            float curJumpAdd = jumpSpeed * Time.deltaTime;
            curJumpDistance -= curJumpAdd;
            //float dir = 1;
            //if (curJumpDistance < jumpDistance / 2)
            //{
            //    dir = -1;
            //}
            characterController.Move(transBody.forward * curJumpAdd);
        }
        else if (curJumpDistance <= 0 && startJump)
        {
            startJump = false;
            characterBase.StartAllAbility(null);
        }
    }
    
}
