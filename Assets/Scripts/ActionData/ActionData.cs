using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionData : ScriptableObject
{
    [SerializeField] protected int p_minDistance;
    [SerializeField] protected int p_maxDistance;
    [Tooltip("If is healing the mofier most be positive, if is attack be negative.")]
    [SerializeField] protected int p_lifeModifier;

    public int minDistance => p_minDistance;
    public int maxDistance => p_maxDistance;
    public int lifeModifier => p_lifeModifier;
}