using UnityEngine;

namespace Player
{
    public class GMPlayerScript : MonoBehaviour
    {
        private GMPlayerController _controller;
        private GMPlayerStateMachine _stateMachine;

        public void SetCharacter()
        {
            _controller = GetComponent<GMPlayerController>();
            _stateMachine = GetComponent<GMPlayerStateMachine>();
            if (_controller != null)
                _controller.Init(this);
            if(_stateMachine != null)
                _stateMachine.Init();
            
            // Set character's sprite
        }

        public void SetChangeAnim(GMPlayerAnim anim)
        {
            if(_stateMachine != null)
                _stateMachine.SetChangeAnim(anim);
        }
    }
}
