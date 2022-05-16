using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.States;
using Core.Characters;
using NaughtyAttributes;
using Core.Stats;
using System;

[CreateAssetMenu(fileName = "Attribute Modifier Effect", menuName = "Game/States/Effects/Attribute Modifier Effect")]
public class AttributeEffect : Effect
{

    [System.Flags] public enum AttributeAffected { bravery, constitution, agility, vitality, determination }

    [EnumFlags] public AttributeAffected attributeAffected;
    public Modifier.Type modifierType;
    public float factor;

    private Character actor;
    private float power;

    private List<Modifier> _trackedModifiers;
    private Core.Stats.Attribute[] _attributes;

    public override void OnEffectActivated(Character actor) {

        base.OnEffectActivated(actor);

        // Set array of actor attributes
        _attributes = new Core.Stats.Attribute[] {

            actor.stats.bravery,
            actor.stats.constitution,
            actor.stats.agility,
            actor.stats.vitality,
            actor.stats.determination

        };

        Type _t = typeof(AttributeAffected);

        // Add modifier for each attribute in enum flags
        for(int i = 0; i < Enum.GetValues(_t).Length; i++) {

            AttributeAffected _value = (AttributeAffected)Enum.GetValues(_t).GetValue(i);

            if(attributeAffected.HasFlag(_value)) {

                Modifier _modifier = new Modifier(modifierType, factor * power);

                _attributes[i].modifiers.Add(_modifier);
                _trackedModifiers.Add(_modifier);

            }

        }

    }

    public override void OnEffectExpired(Character actor) {

        base.OnEffectExpired(actor);

        // Remove each tracked modifier when the effect is expired
        for(int i = 0; i < _attributes.Length; i++) {

            foreach (Modifier modifier in _trackedModifiers) {
                
                _attributes[i].modifiers.Remove(modifier);

            }

        }

    }

}
