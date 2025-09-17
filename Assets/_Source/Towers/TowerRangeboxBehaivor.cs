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
        if (collision.gameObject != null && collision.gameObject.CompareTag("Enemy") && potentialTargets.Contains(collision.gameObject) == false && collision.gameObject.TryGetComponent<EnemyHandler>(out EnemyHandler damagable))
        {
            if (damagable.CurrentHealth > 0)
            {
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
    private void ChecklistForDead()
    {
        for (int i = 0; i < potentialTargets.Count; i++)
        {
            if (potentialTargets.Count <= i) continue;
            if (potentialTargets[i].TryGetComponent(out EnemyHandler handler))
            {
                if (handler.CurrentHealth <= 0) potentialTargets.RemoveAt(i);
            }
            else
            {
                potentialTargets.RemoveAt(i);
            }
        }
    }
    internal bool TryGetLists(out GameObject _target, out List<GameObject> potential)
    {
        ChecklistForDead();
        _target = target;
        potential = potentialTargets;
        return true;
    }
}
