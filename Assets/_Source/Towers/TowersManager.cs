using System.Collections.Generic;
using UnityEngine;

public class TowersManager : MonoBehaviour
{
    public static TowersManager instance { get; private set; }
    [field: SerializeField]
    public List<TowerDataSO> AvailableTowers;
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    internal protected bool RequestToBuyTower(GameObject towerBaseGO, TowerDataSO tower)
    {
        if (towerBaseGO == null || AvailableTowers.Contains(tower) == false || towerBaseGO.TryGetComponent<TowerBaseHandler>(out TowerBaseHandler towerBase) == false) return false;
        if (ResourceManager.instance.TryBuy(tower.BaseCost))
        {
            towerBase.PlaceTower(tower);
            return true;
        }
        return false;
    }
    internal protected bool TryRequestTowersList(out List<TowerDataSO> towers)
    {
        if (AvailableTowers == null || AvailableTowers.Count <= 0)
        {
            towers = null;
            return false;
        }
        towers = AvailableTowers;
        return true;
    }
    internal protected List<TowerDataSO> RequestTowersList()
    {
        if (AvailableTowers == null || AvailableTowers.Count <= 0)
        {
            return null;
        }
        return AvailableTowers;
    }
}
