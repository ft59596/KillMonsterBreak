using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : CharacterAbility
{
    public MoveAbility moveAbility;
    public bool isCanAttackMove;
    public List<float> moveTime;
    public List<float> attackMoveSpeed;
    [HideInInspector]
    public float currentMoveTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveAbility = transform.GetComponent<MoveAbility>();
    }

    // Update is called once per frame
   public override void OnUpdate()
    {
        OnRoleAttack();
        OnAttackMoveRun();
    }

    public void OnRoleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            characterBase.characterAnimator.AnimatorDoNormalAttack();
        }
        if (moveAbility != null)
        {
            moveAbility.abilityEnable = !characterBase.characterAnimator.isAttacking;
        }

    }

    void OnAttackMoveRun() {
        if (currentMoveTime > 0 && moveAbility != null) {
            currentMoveTime -= Time.deltaTime;
            //moveAbility.RoleForwardDirection(attackMoveSpeed[]);
        }
    }
}
