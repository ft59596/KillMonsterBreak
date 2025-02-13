using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public bool isAIController;
    public List<CharacterAbility> characterAbilities;
    public CharacterAnimator characterAnimator;
    public Vector3 currentRoleDirection;
    public virtual void Start() { }
    public virtual void Update() { }
}
