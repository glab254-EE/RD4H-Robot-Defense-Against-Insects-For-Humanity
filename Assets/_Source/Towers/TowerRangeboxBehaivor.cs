using System.Collections.Generic;
using UnityEngine;

public class TowerRangeboxBehaivor : MonoBehaviour
{
    private GameObject target;
    private System.Collections.Generic.List<GameObject> potentialTargets;
    void Start()
    {
        potentialTargets = new();
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("New enemy?");
        if (collision.gameObject != null && collision.gameObject.CompareTag("Enemy") && potentialTargets.Contains(collision.gameObject) == false && collision.gameObject.TryGetComponent<EnemyHandler>(out EnemyHandler damagable))
        {
            if (damagable.CurrentHealth > 0)
            {
            Debug.Log("New enemy!!");
            potentialTargets.Add(collision.gameObject);
            if (target == null)
            {
                target = collision.gameObject;
            }                
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject != null && potentialTargets.Contains(collision.gameObject) == true)
        {
            potentialTargets.Remove(collision.gameObject);
            if (target != null && target == collision.gameObject)
            {
                target = potentialTargets.Count >= 1 ? potentialTargets[0] : null;
            }
        }
    }
    internal bool TryGetLists(out GameObject _target, out List<GameObject> potential)
    {
        _target = target;
        potential = potentialTargets;
        return true;
    }
}
