using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerBaseHandler : MonoBehaviour
{
    [field: SerializeField]
    private GameObject towerPrefab;
    [field: SerializeField]
    internal TowerDataSO currentTowerSO{ get; private set; }
    [field: SerializeField]
    private Collider rangeBox;
    [field: SerializeField]
    private TowerRangeboxBehaivor towerRangebox;
    private GameObject currentTowerObject;
    private GameObject target;
    private List<GameObject> potentialTargets;
    void Start()
    {
        if (rangeBox == null) rangeBox = GetComponent<Collider>();
        GameManager.instance.ConnectEvent(OnTick, 0);
        if (currentTowerSO != null)
        {
            TowerDataSO temp = currentTowerSO;
            currentTowerSO = null;
            PlaceTower(temp);
        }
        if (towerRangebox == null)
        {
            towerRangebox = GetComponentInChildren<TowerRangeboxBehaivor>();
        }
        potentialTargets = new();
    }
    void OnTick()
    {
        if (currentTowerSO == null || gameObject == null) return;
        if (currentTowerObject == null)
        {
            currentTowerSO = null;
            return;
        }
        if (towerRangebox!=null && towerRangebox.TryGetLists(out target, out potentialTargets))
        {
            currentTowerSO.OnTick(currentTowerObject, potentialTargets, target);            
        }
    }
    internal void PlaceTower(TowerDataSO newTower)
    {
        if (currentTowerSO != null || currentTowerObject != null || newTower == null) return;
        /*if (isTowerPlaced)
        {
            if (!ResourceManager.instance.TryBuy(newTower.BaseCost))
            {
                Debug.LogWarning("Potential exploiting detected.");
                return;
            }
        }*/ 
        currentTowerSO = newTower;
        currentTowerObject = Instantiate(towerPrefab, transform, false);
        if (rangeBox is SphereCollider && newTower.BaseRange >= 0)
        {
            SphereCollider sphereCollider = rangeBox as SphereCollider;
            sphereCollider.radius = newTower.BaseRange;
        }
        if (newTower.Animator != null && currentTowerObject.TryGetComponent<Animator>(out Animator animator))
        {
            // TODO: ADD ANIMATIONS AND DAM. DAMN.
        }
        if (newTower.TowerVisualPrefab != null)
        {
            GameObject cloned = Instantiate(newTower.TowerVisualPrefab, currentTowerObject.transform);
            if (newTower.PositionOffset != Vector3.zero)
            {
                cloned.transform.localPosition = newTower.PositionOffset;
            }
            if (newTower.SizeOffset != Vector3.zero)
            {
                cloned.transform.localScale = newTower.SizeOffset;
            }
        }
        if (newTower.PositionOffset != null && newTower.PositionOffset.magnitude != 0) currentTowerObject.transform.Translate(newTower.PositionOffset);
        if (newTower.SizeOffset != null && newTower.SizeOffset.magnitude != 0) currentTowerObject.transform.localScale = newTower.SizeOffset;
        currentTowerSO.OnPlace(currentTowerObject);
    }
}