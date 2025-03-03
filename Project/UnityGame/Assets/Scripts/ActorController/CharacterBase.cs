using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    /// <summary>
    /// ½ÇÉ«ÊôÐÔ
    /// </summary>
    public Dictionary<RoleProperty, float> attribute;
    public bool isAIController;
    public List<CharacterAbility> characterAbilities;
    public CharacterAnimator characterAnimator;
    public Vector3 currentRoleDirection;
    public Vector3 position
    {
        get {
            return transform.position;
        }
    }
    public virtual void Start() { }
    public virtual void Update() { }
}

public enum RoleProperty
{
    Attack = 100000,
    Hp,
    MaxHp,
    Sp,
    MaxSp,
    Defend,
}