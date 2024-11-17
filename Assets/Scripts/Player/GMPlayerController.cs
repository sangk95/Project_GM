using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GMPlayerController : MonoBehaviour
{
    private const float MOVE_SPEED = 2.5f;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;

    private SpriteRenderer _sprite;

    // Animation -------------------------
    private Animator _animator;
    private bool _isFacingLeft;
    private int _hashLastMoveX;
    private int _hashLastMoveY;
    public void Init()
    {
        _isFacingLeft = false;

        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        if(_animator != null)
        {
            _hashLastMoveX = Animator.StringToHash("LastMoveX");
            _hashLastMoveY = Animator.StringToHash("LastMoveY");
        }
        GMInputManager.Instance.AddAction("Move", OnMove);
        GMInputManager.Instance.AddAction("NormalAttack", OnAttack_Normal);
        GMInputManager.Instance.AddAction("Dash", OnDash);
    }
    public void OnDestroy()
    {
        GMInputManager.Instance.RemoveAction("Move", OnMove);
        GMInputManager.Instance.RemoveAction("NormalAttack", OnAttack_Normal);
        GMInputManager.Instance.RemoveAction("Dash", OnDash);
    }
    private void Update()
    {
        ProcessInput();
    }
    private void ProcessInput()
    {
        if (_rigidbody != null)
        {
            _moveDirection = GMInputManager.Instance.GetMoveValue();
            if (_moveDirection == Vector2.zero)
            {
                if (_rigidbody.velocity != Vector2.zero)
                {
                    _animator.SetFloat(_hashLastMoveX, _rigidbody.velocity.x);
                    _animator.SetFloat(_hashLastMoveY, _rigidbody.velocity.y);
                }
            }
            else if(_isFacingLeft == true && _moveDirection.x > 0 ||
                    _isFacingLeft == false && _moveDirection.x < 0)
            {
                _isFacingLeft = !_isFacingLeft;
                if (_sprite != null)
                    _sprite.flipX = _isFacingLeft;
            }

            _rigidbody.velocity = _moveDirection * MOVE_SPEED;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _moveDirection = Vector2.zero;
            return;
        }

    }
    private void OnAttack_Normal(InputAction.CallbackContext context)
    {

    }
    private void OnDash(InputAction.CallbackContext context)
    {

    }
}
