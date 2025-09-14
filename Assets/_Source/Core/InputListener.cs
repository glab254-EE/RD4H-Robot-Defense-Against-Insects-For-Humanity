using UnityEngine;

public class InputListener : MonoBehaviour
{
    [field: SerializeField]
    private LayerMask towerBaseMask;
    private InputSystem_Actions inputActions;
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
        inputActions = new();
    }
    void OnMouseClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 150, towerBaseMask))
        {
            if (hit.collider.gameObject.TryGetComponent<TowerHandler>(out _))
            {
                // TODO: IMPLEMENT TOWER PLACING
            }
        }
    }
}
