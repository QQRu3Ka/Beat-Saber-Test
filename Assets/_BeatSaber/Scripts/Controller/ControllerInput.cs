using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [field:SerializeField] public InputActionProperty PauseAction { get; set; }

    private void OnEnable()
    {
        PauseAction.action.performed += TogglePause;
    }

    private void OnDisable()
    {
        PauseAction.action.performed -= TogglePause;
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (!_menu.IsPaused)
        {
            _menu.PauseGame();
        }
    }
}
