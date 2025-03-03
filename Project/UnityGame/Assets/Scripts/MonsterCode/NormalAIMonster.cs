using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalAIMonster : MonoBehaviour
{
    public float checkEnemyRadius;
    public float attackEnemyRadius;
    public float moveSpeed;
    public List<GameObject> attackBullets;
    public CharacterBase characterBase;
    public CharacterBase enemyCharacterBase;
    /// <summary>
    /// ÒÆ¶¯
    /// </summary>
    public NavMeshAgent navMeshAgent;
    public CharacterAnimator characterAnimator;
    /// <summary>
    /// ¹¥»÷¼ä¸ô
    /// </summary>
    public float attackInterval;
    /// <summary>
    /// µ±Ç°¹¥»÷¼ä¸ô
    /// </summary>
    public float curAttackInterval;
    public bool doAttack;
    private void Awake()
    {
        characterAnimator = GetComponent<CharacterAnimator>();
        characterBase = GetComponent<CharacterBase>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        FindEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        CombatLogic();
    }

    public void FindEnemy()
    {
        if (enemyCharacterBase == null)
        {
            enemyCharacterBase = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBase>();
        }
    }
    /// <summary>
    /// Õ½¶·Âß¼­
    /// </summary>
    public void CombatLogic()
    {
        if (enemyCharacterBase == null)
        {
            return;
        }
        if (Vector3.Distance(enemyCharacterBase.position, transform.position) < attackEnemyRadius)
        {
            navMeshAgent.SetDestination(transform.position);
            characterAnimator.AnimatorDoNormalAttack();
        }
        else if (Vector3.Distance(enemyCharacterBase.position, transform.position) < checkEnemyRadius)
        {
            navMeshAgent.SetDestination(enemyCharacterBase.position);
            characterAnimator.AnimatorDoMove();
        }

    }
}
