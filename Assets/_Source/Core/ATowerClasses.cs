using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
[System.Serializable]
public abstract class TowerDataSO : ScriptableObject
{
    [Header("Tower Visuals")]
    [field: SerializeField]
    public Animator Animator { get; protected set; }
    [field: SerializeField]
    public Mesh TowerMesh { get; protected set; }
    [Header("Tower Behaivor")]
    [field: SerializeField]
    public double BaseDamage { get; protected set; }
    [field: SerializeField]
    public float BaseRange { get; protected set; }
    public abstract void OnTick(GameObject tower, List<GameObject> enemies, GameObject currenttarget);
    public abstract void OnPlace();
    public abstract void OnRemoval();
}
