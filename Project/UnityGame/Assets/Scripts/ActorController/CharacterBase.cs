using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    /// <summary>
    /// 角色属性
    /// </summary>
    public Dictionary<RoleProperty, float> attribute;
    public bool isAIController;
    public CharacterAbility[] characterAbilities;
    public CharacterAnimator characterAnimator;
    public Vector3 currentRoleDirection;
    public Vector3 position
    {
        get
        {
            return transform.position;
        }
    }
    public virtual void Start() {
        characterAbilities = transform.GetComponents<CharacterAbility>();
        OnStart();
    }
    public virtual void OnStart() { }
    public virtual void Update() {
        OnUpdate();
    }
    public virtual void OnUpdate() { }
    /// <summary>
    /// 关闭全部能力
    /// </summary>
    /// <param name="ignoreAbility">忽略某些能力</param>
    public void StopAllAbility(List<CharacterAbility> ignoreAbility)
    {

        for (int i = 0; i < characterAbilities.Length; i++)
        {
            if (ignoreAbility == null || !ignoreAbility.Contains(characterAbilities[i]))
            {
                characterAbilities[i].abilityEnable = false;
            }
        }
        for (int i = 0; ignoreAbility != null && i <  ignoreAbility.Count; i++)
        {
            ignoreAbility[i].abilityEnable = true;
        }
    }

    public void StartAllAbility(List<CharacterAbility> ignoreAbility) {
        for (int i = 0; i < characterAbilities.Length; i++)
        {
            if (ignoreAbility == null || !ignoreAbility.Contains(characterAbilities[i]))
            {
                characterAbilities[i].abilityEnable = true;
            }
        }
    }
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