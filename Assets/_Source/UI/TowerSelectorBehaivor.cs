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
    private Transform cameraTransform;
    private Camera _camera;
    void Start()
    {
        towerSelections = new();
        if (TowersManager.instance.TryRequestTowersList(out towerSelections))
        {
            UpdateTowerSelection();
        }
        _camera = Camera.main;
        cameraTransform = _camera.transform;
        GetComponent<Canvas>().worldCamera = _camera;
    }
    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            transform.LookAt(2*transform.position-cameraTransform.transform.position);
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
