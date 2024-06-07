using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter
{
    private readonly Dictionary<Type, Func<ActionData, ActionRules>> converters = new()
    {
        { typeof(MeleeAttackData), data => new MeeleAttack { actionData = data } },
        { typeof(HealerData), data => new Health { actionData = data } },
        { typeof(SelfHealerData), data => new SealfHealth { actionData = data } },
        { typeof(RangeData), data => new RangeAttack { actionData = data } }
    };

    public Character CreateNewCharacter(CharacterData data, Vector2 position)
    {
        Character character = new();
        character.CreateCharacter(data);
        character.SetPosition(position);
        character._characterData = data;
        foreach (var effect in data.effectData)
        {
            if (converters.TryGetValue(effect.GetType(), out var converter))
            {
                var rule = converter(effect);
                character.SetActionRules(rule);
            }
        }
        return character;
    }
}
