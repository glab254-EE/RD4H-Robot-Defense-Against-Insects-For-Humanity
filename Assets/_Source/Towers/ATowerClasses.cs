using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
[System.Serializable]
public abstract class TowerDataSO : ScriptableObject
{
    [Header("Tower Visuals")]
    [field: SerializeField]
    public string TowerName { get; protected set; }
    [field: SerializeField]
    public Animator Animator { get; protected set; }
    [field: SerializeField]
    public Mesh TowerMesh { get; protected set; }
    [field: SerializeField]
    public Vector3 PositionOffset { get; protected set; }
    [field: SerializeField]
    public Vector3 SizeOffset { get; protected set; }
    [field: SerializeField]
    [Header("Tower Behaivor")]
    public double BaseDamage { get; protected set; }
    [field: SerializeField]
    public float BaseRange { get; protected set; }
    [field: SerializeField]
    public SDouble BaseCost { get; protected set; }
    [field: SerializeField]
    public TowerDataSO NextUpgradeTower { get; protected set; }
    public abstract void OnTick(GameObject tower, List<GameObject> enemies, GameObject currenttarget);
    public abstract void OnPlace(GameObject tower);
    public abstract void OnUpgrade(TowerBaseHandler parent,GameObject oldTower);
}
