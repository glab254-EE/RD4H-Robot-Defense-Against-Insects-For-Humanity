using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class TowerSelectorBehaivor : MonoBehaviour
{
    [field: SerializeField]
    protected internal GameObject towerBaseModule;
    [field: SerializeField]
    private GameObject towerSelectionTemplate;
    [field: SerializeField]
    private Transform towerSelectorParent;
    private List<TowerDataSO> towerSelections;
    private Camera _camera;
    void Start()
    {
        towerSelections = new();
        _camera = Camera.main;
        GetComponent<Canvas>().worldCamera = _camera;
        if (TowersManager.instance.TryRequestTowersList(out towerSelections))
        {
            UpdateTowerSelection();
        }
    }
    void Update()
    {
        if (_camera != null)
        {
            transform.LookAt(transform.position-_camera.transform.position);
        }
    }
    void UpdateTowerSelection()
    {
        if (towerSelections == null || towerSelections.Count <= 0) return;
        for (int i = 0; i < towerSelections.Count; i++)
        {
            TowerDataSO tower = towerSelections[i];
            GameObject newUI = Instantiate(towerSelectionTemplate, towerSelectorParent);
            TowerSelectOptionBehaivor button = newUI.GetComponentInChildren<TowerSelectOptionBehaivor>();
            if (button != null)
            {
                button.selectedTower = tower;
                button.towerBaseModule = towerBaseModule;
                button.parentUI = gameObject;
            }
            else
            {
                Destroy(newUI);
                Debug.LogWarning("Failed to add tower. Index: " + i.ToString());
            }
        }
    }
}
