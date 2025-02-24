using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MoveAbility
{
    public CharacterController characterController;
    public float moveSpeed;

    public Transform transBody;
    // Start is called before the first frame update
    void Start()
    {
        characterBase = transform.GetComponent<CharacterBase>();
        RoleForwardDirection = () => characterController.SimpleMove(characterBase.currentRoleDirection.normalized * Time.deltaTime * moveSpeed);
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        OnRoleMoveController();
    }
    void OnRoleMoveController()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movedir = new Vector3(h,0, v);
        if (movedir.magnitude > 0)
        {
            transBody.forward = movedir.normalized;
            characterBase.characterAnimator.AnimatorDoMove();
            characterBase.currentRoleDirection = movedir.normalized;
        }
        else {
            characterBase.characterAnimator.AnimatorDoIdle();
        }
        characterController.SimpleMove(movedir.normalized * Time.deltaTime * moveSpeed);
    }
}
