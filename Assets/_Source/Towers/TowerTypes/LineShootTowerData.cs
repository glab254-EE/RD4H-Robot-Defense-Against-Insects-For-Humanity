using UnityEngine;

[CreateAssetMenu(fileName = "LineShootTowerData", menuName = "Scriptable Objects/Towers/New Line Shooting Tower Data"), System.Serializable]
public class LineShootTowerData : TowerDataSO
{
    [field: SerializeField]
    public int BaseCooldown { get; protected set; }
    private int CurrentCooldown; // using tick time not float time.
    public override void OnPlace()
    {
    }

    public override void OnRemoval()
    {
    }
    public override void OnTick(GameObject tower, System.Collections.Generic.List<GameObject> enemies, GameObject currenttarget)
    {
        Debug.Log("Tick!");
        CurrentCooldown--;
        if (CurrentCooldown <= 0)
        {
            CurrentCooldown = BaseCooldown;
            OnShoot(currenttarget);
        } 
    }
    private void OnShoot(GameObject target)
    {
        Debug.Log("Pew!");        
    }
}