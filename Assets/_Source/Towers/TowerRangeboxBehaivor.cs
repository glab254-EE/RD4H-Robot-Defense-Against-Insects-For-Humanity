using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (collision.gameObject != null
        && collision.gameObject.CompareTag("Enemy")
        && potentialTargets.Contains(collision.gameObject) == false
        && collision.gameObject.TryGetComponent(out EnemyHandler damagable)
        && collision.gameObject.TryGetComponent(out NavMeshAgent agent))
        {
            if (damagable.CurrentHealth > 0 && agent.speed > 0)
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
            if (potentialTargets.Count <= i || potentialTargets[i] == null) continue;
            if (potentialTargets[i] == null)
            {
                potentialTargets.Remove(potentialTargets[i]);
                continue;
            }
            if (potentialTargets[i].TryGetComponent(out EnemyHandler handler))
            {
                if (handler.CurrentHealth <= 0)
                {
                    if (potentialTargets[i] == target)
                    {
                        target = potentialTargets.Count >= 1 ? potentialTargets[0] : null;
                    }
                    potentialTargets.Remove(potentialTargets[i]);
                }
            }
            else
            {
                if (potentialTargets[i] == target)
                {
                    target = potentialTargets.Count >= 1 ? potentialTargets[0] : null;
                }
                potentialTargets.Remove(potentialTargets[i]);
            }
        }
    }
    internal bool TryGetLists(out GameObject _target, out List<GameObject> potential)
    {
        ChecklistForDead();
        if (target == null && potentialTargets.Count >= 1)
        {
            target = potentialTargets[0];
        }
        _target = target;
        potential = potentialTargets;
        return true;
    }
}
