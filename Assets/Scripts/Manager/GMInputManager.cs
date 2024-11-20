using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GMInputManager : GMManagerBase<GMInputManager>
{
    private GMInputAction _inputAction;

    public override IEnumerator GMAwake()
    {
        _inputAction = new GMInputAction();
        _inputAction.Enable();
        return base.GMAwake();
    }
    public void AddAction(string inputType, Action<InputAction.CallbackContext> callback)
    {
        InputAction action = _inputAction.asset.FindAction(inputType);
        if (action != null)
        {
            action.started -= callback;
            action.started += callback;
            action.performed -= callback;
            action.performed += callback;
            action.canceled -= callback;
            action.canceled += callback;
        }
    }
    public void RemoveAction(string inputType, Action<InputAction.CallbackContext> callback)
    {
        InputAction action = _inputAction.asset.FindAction(inputType);
        if (action != null && callback != null)
        {
            action.started -= callback;
            action.performed -= callback;
            action.canceled -= callback;
        }
    }
    public Vector2 GetMoveValue()
    {
        return _inputAction.PC.Move.ReadValue<Vector2>();
    }
    public Vector2 GetMousePosition()
    {
        if (Camera.main != null)
            return Camera.main.ScreenToWorldPoint(_inputAction.PC.MousePosition.ReadValue<Vector2>());
        return Vector2.zero;
    }
}
