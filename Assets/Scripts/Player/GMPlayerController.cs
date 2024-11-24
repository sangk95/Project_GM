using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
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
        private int _hashCurMoveX;
        private int _hashCurMoveY;

        private GMPlayerScript _myScript;
        public void Init(GMPlayerScript playerScript)
        {
            _isFacingLeft = false;
            _myScript = playerScript;

            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
            _animator = GetComponentInChildren<Animator>();
            if(_animator != null)
            {
                _hashLastMoveX = Animator.StringToHash("LastMoveX");
                _hashLastMoveY = Animator.StringToHash("LastMoveY");
                _hashCurMoveX = Animator.StringToHash("CurMoveX");
                _hashCurMoveY = Animator.StringToHash("CurMoveY");
            }
            GMInputManager.Instance.AddAction("Move", OnMove);
            GMInputManager.Instance.AddAction("NormalAttack", OnAttack_Normal);
            GMInputManager.Instance.AddAction("Dash", OnDash);
        }
        public void OnDisable()
        {
            if (GMInputManager.Instance != null)
            {
                GMInputManager.Instance.RemoveAction("Move", OnMove);
                GMInputManager.Instance.RemoveAction("NormalAttack", OnAttack_Normal);
                GMInputManager.Instance.RemoveAction("Dash", OnDash);
            }
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
                if(_isFacingLeft == true && _moveDirection.x > 0 ||
                        _isFacingLeft == false && _moveDirection.x < 0)
                {
                    _isFacingLeft = !_isFacingLeft;
                    if (_sprite != null)
                        _sprite.flipX = _isFacingLeft;
                }
                _rigidbody.velocity = new Vector2(_moveDirection.x * MOVE_SPEED, 0);
            }
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if(_myScript == null)
                return;
            if (context.canceled)
            {
                _moveDirection = Vector2.zero;
                _myScript.SetChangeAnim(GMPlayerAnim.Player_Idle);
            }
            else if (context.started)
            {
                _myScript.SetChangeAnim(GMPlayerAnim.Player_Run);
            }
        }
        private void OnAttack_Normal(InputAction.CallbackContext context)
        {

        }
        private void OnDash(InputAction.CallbackContext context)
        {
            if (_myScript == null)
                return;
            if (context.started)
            {
                _myScript.SetChangeAnim(GMPlayerAnim.Player_Dash);
            }
        }
    }
}
