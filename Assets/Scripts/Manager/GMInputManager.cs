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
        return base.GMAwake();
    }
    private void OnEnable()
    {
        _inputAction.Enable();
    }
    protected override void OnDisable()
    {
        _inputAction.Disable();
        base.OnDisable();
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
    public Vector2 GetMoveValue()
    {
        return _inputAction.PC.Move.ReadValue<Vector2>();
    }
    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(_inputAction.PC.MousePosition.ReadValue<Vector2>());
    }
}
