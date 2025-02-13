using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    public bool abilityEnable = true;
    public CharacterBase characterBase;

    private void Awake()
    {
        characterBase = transform.GetComponent<CharacterBase>();
    }

    public virtual void Update() {
        if (!abilityEnable) return;
        OnUpdate();
    }
    public virtual void OnUpdate() { }
}
