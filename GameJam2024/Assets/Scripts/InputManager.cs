using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputControls _input;

    private void Awake()
    {
        _input = new InputControls();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDestroy()
    {
        _input.Disable();
    }

    private void Start()
    {
        _input.Controls.Tick.started += ctx => StartTick(ctx);   
        _input.Controls.Tick.canceled += ctx => EndTick(ctx);    
    }

    private void StartTick(InputAction.CallbackContext context)
    {
        Debug.Log("Tick started");
    }

    private void EndTick(InputAction.CallbackContext context)
    {
        Debug.Log("Tick ended");
    }

}
