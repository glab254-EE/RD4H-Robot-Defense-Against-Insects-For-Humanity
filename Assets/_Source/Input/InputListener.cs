using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    [field: SerializeField]
    private GameObject ReferenceTowerSelectorTemplate;
    [field: SerializeField]
    private CinemachineInputAxisController cameraInputActions;
    [field: SerializeField]
    private LayerMask towerBaseMask;
    private InputSystem_Actions inputActions;
    private Camera _camera;
    private GameObject CurrentOpenUI;
    void Start()
    {
        _camera = Camera.main;
        inputActions = new();
        inputActions.Player.SelectOrPlace.performed += OnLeftMouseClick;
        inputActions.Player.ToggleMoveCamera.performed += OnRightMouseToggle;
        inputActions.Player.ToggleMoveCamera.canceled += OnRightMouseToggle;
        inputActions.Player.ToggleMoveCamera.Enable();
        inputActions.Player.SelectOrPlace.Enable();
        cameraInputActions.enabled = false;
    }
    void OnDestroy()
    {
        inputActions.Player.SelectOrPlace.performed -= OnLeftMouseClick;
        inputActions.Player.ToggleMoveCamera.performed -= OnRightMouseToggle;
        inputActions.Player.ToggleMoveCamera.canceled -= OnRightMouseToggle;
        inputActions.Player.ToggleMoveCamera.Disable();
        inputActions.Player.SelectOrPlace.Disable();
        inputActions.Disable();
    }
    void ToggleCameraMovementState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.Confined : CursorLockMode.None;
        if (state)
        {
            if (!inputActions.Player.Zoom.enabled) inputActions.Player.Zoom.Enable();
            if (!inputActions.Player.Look.enabled) inputActions.Player.Look.Enable();
        }
        else
        {
            if (inputActions.Player.Zoom.enabled) inputActions.Player.Zoom.Disable();
            if (inputActions.Player.Look.enabled) inputActions.Player.Look.Disable();
        }
        List<InputAxisControllerBase<CinemachineInputAxisController.Reader>.Controller> controllers = cameraInputActions.Controllers;
        foreach (InputAxisControllerBase<CinemachineInputAxisController.Reader>.Controller controller in controllers)
        {
            controller.Enabled = state;
        }
    }
    void OnRightMouseToggle(InputAction.CallbackContext callbackContext)
    {
        ToggleCameraMovementState(callbackContext.ReadValueAsButton());
        if (callbackContext.ReadValueAsButton())
        {
            cameraInputActions.enabled = true;
        }
        else
        {
            cameraInputActions.enabled = false;
        }
    }
    void OnLeftMouseClick(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.ReadValueAsButton()) return;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 250, towerBaseMask))
        {
            if (hit.collider.gameObject.TryGetComponent<TowerBaseHandler>(out _))
            {
                if (CurrentOpenUI != null)
                {
                    Destroy(CurrentOpenUI);
                }
                CurrentOpenUI = Instantiate(ReferenceTowerSelectorTemplate,hit.transform.position,Quaternion.identity,hit.transform);
                CurrentOpenUI.transform.localPosition = new Vector3(0, 2, 0);
                if (CurrentOpenUI.TryGetComponent<TowerSelectorBehaivor>(out TowerSelectorBehaivor component))
                {
                    component.towerBaseModule = hit.transform.gameObject;
                }
            }
        }
    }

}
