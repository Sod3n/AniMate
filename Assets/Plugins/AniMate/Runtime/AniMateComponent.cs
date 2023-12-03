using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

namespace AniMate
{
    public class AniMateComponent : MonoBehaviour
    {
        private static UnityEditor.Animations.AnimatorController _animatorController;

        [SerializeField] private Animator _animator;

        private AniMateState _lastState = new AniMateState();
        private bool _startPlaying = false;
        private IEnumerator _waitEnd; 
        public AniMateState Play(AnimationClip clip)
        {
            if (_animatorController is null)
                CreateAnimatorController();

            if(_animator is null)
            {
                throw new Exception("Animator is not assigned!");
            }

            _lastState.Reset();

            if(_waitEnd != null)
                StopCoroutine(_waitEnd);

            // uncomment it and it wont work. Magic(or maybe compiler tricks).
            //_animator.runtimeAnimatorController = _animatorController;

            var structStates = _animatorController.layers[0].stateMachine.states;

            foreach (var structState in structStates)
            {

                _animatorController.layers[0].stateMachine.RemoveState(structState.state);
            }

            _lastState.State = _animatorController.AddMotion(clip);

            // as i see, all changes at animator controller marks it as dirty,
            // so we need to set it after all changes is done
            _animator.runtimeAnimatorController = _animatorController;

            if (!clip.isLooping)
            {
                _waitEnd = WaitEnd();
                StartCoroutine(_waitEnd);
            }

            _startPlaying = false;

            return _lastState;
        }

        private void FixedUpdate()
        {
            if (!_startPlaying) return;

            _animator.PlayInFixedTime(_lastState.State.nameHash);
            _startPlaying = true;
        }

        private void CreateAnimatorController()
        {
            _animatorController = new UnityEditor.Animations.AnimatorController();
            _animatorController.name = "AniMate";
            _animatorController.hideFlags = HideFlags.HideAndDontSave;
            _animatorController.AddLayer("Layer 0");
        }

        public IEnumerator WaitEnd()
        {
            while (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash != _lastState.State.nameHash)
            {
                yield return null;
            }

            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return null;
            }

            _lastState.OnEnd.Invoke();
        }
    }
}
