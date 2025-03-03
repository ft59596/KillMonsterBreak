using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterBase characterBase;
    private MoveAbility  moveAbility;
    // Start is called before the first frame update
    void Start()
    {
        characterBase = GetComponent<CharacterBase>();
        moveAbility = GetComponent<MoveAbility>();  
    }

    // Update is called once per frame
    public void Update()
    {
        OnRoleAttack();
 
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
