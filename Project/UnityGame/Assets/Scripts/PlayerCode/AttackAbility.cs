using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackAbility : CharacterAbility
{
    public MoveAbility moveAbility;
    public bool isCanAttackMove;
    public List<float> moveTime;
    public List<float> attackMoveSpeed;
    [HideInInspector]
    public float currentMoveTime = 0;
    private NavMeshAgent navMeshAgent;
    private CharacterAnimator characterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        moveAbility = transform.GetComponent<MoveAbility>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        OnRoleAttack();
        OnAttackingStopMove();
        OnAttackMoveRun();
    }
    void OnAttackingStopMove()
    {
        if (characterAnimator.isAttacking)
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = true;
            }
        }
        else {
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = false;
            }
        }
    }
    void OnAttackMoveRun()
    {
        if (currentMoveTime > 0 && moveAbility != null)
        {
            currentMoveTime -= Time.deltaTime;
            //moveAbility.RoleForwardDirection(attackMoveSpeed[]);
        }
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
}
