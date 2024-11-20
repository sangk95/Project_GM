using UnityEditor.Animations;
using UnityEngine;

namespace Player
{
    public class GMPlayerStateMachine : MonoBehaviour
    {
        private Animator _animator;

        public void Init()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void SetChangeAnim(GMPlayerAnim anim)
        {
            int hashCode = GMCharacterManager.Instance.GetAnimHash(anim);
            if(_animator)
                _animator.Play(hashCode);
        }

        public void ChangeState(GMPlayerState state)
        {

        }
    }
}
