using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TowerBaseHandler : MonoBehaviour
{
    [field:SerializeField]
    private GameObject towerPrefab;
    [Space]
    [field: SerializeField]
    private TowerDataSO currentTowerSO;
    [field: SerializeField]
    private Collider rangeBox;
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
    }
    void OnTick()
    {
        Debug.Log("Tick!");
        if (currentTowerObject == null || currentTowerSO == null) return;
        currentTowerSO.OnTick(gameObject,potentialTargets, target);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.collider.gameObject != null && potentialTargets.Contains(collision.gameObject) == false)
        {
            potentialTargets.Add(collision.gameObject);
            if (target == null)
            {
                target = collision.gameObject;
            }
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider != null && collision.collider.gameObject != null && potentialTargets.Contains(collision.gameObject) == true)
        {
            potentialTargets.Remove(collision.gameObject);
            if (target != null && target == collision.gameObject)
            {
                target = potentialTargets.First();
            }
        }        
    }
    internal void PlaceTower(TowerDataSO newTower)
    {
        if (currentTowerSO != null || currentTowerObject != null || newTower == null) return;
        currentTowerSO = newTower;
        currentTowerObject = Instantiate(towerPrefab, transform, false);
        if (rangeBox is SphereCollider && newTower.BaseRange >= 0)
        {
            SphereCollider sphereCollider = rangeBox as SphereCollider;
            sphereCollider.radius = newTower.BaseRange;
        }
        if (newTower.Animator != null && currentTowerObject.TryGetComponent<Animator>(out Animator animator))
        {
            // TODO: ADD ANIMATIONS AND DAM
        }
        if (newTower.TowerMesh != null)
        {
            MeshFilter meshRenderer = currentTowerObject.GetComponentInChildren<MeshFilter>();
            if (meshRenderer != null)
            {
                meshRenderer.mesh = newTower.TowerMesh;
            }
        }
        currentTowerSO.OnPlace();
    }
}