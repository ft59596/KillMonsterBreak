using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : CharacterAbility
{
    public Transform transBody;
    private CharacterController characterController;
    /// <summary>
    /// ��Ծ��ȴʱ��
    /// </summary>
    [Header("��Ծ��ȴʱ��")]
    public float jumpCoolTime = 0;
    /// <summary>
    /// ��ǰ��ȴʱ��
    /// </summary>
    private float curJumpCoolTime = 0;
    [Header("��Ծ����")]
    public float jumpDistance = 0.5f;
    /// <summary>
    /// ��ǰ��Ծ����
    /// </summary>
    private float curJumpDistance = 0;
    /// <summary>
    /// ��Ծ�ٶ�
    /// </summary>
    [Header("��Ծ�ٶ�")]
    public float jumpSpeed = 10;
    /// <summary>
    /// �Ƿ�ʼ��Ծ
    /// </summary>
    [Header("�Ƿ�ʼ��Ծ")]
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
            Debug.Log("��Ծ");
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
