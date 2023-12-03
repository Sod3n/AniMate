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
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _useFixedTime;
        [SerializeField] private UnityEditor.Animations.AnimatorController _animatorController;
        [SerializeField] private AnimationClip _animationClip;

        public void Play(AnimationClip clip)
        {
            // uncomment it and it wont work. Magic(or maybe compiler tricks).
            //_animator.runtimeAnimatorController = _animatorController; 

            var structStates = _animatorController.layers[0].stateMachine.states;

            foreach (var structState in structStates)
            {

                _animatorController.layers[0].stateMachine.RemoveState(structState.state);
            }

            AnimatorState state = _animatorController.AddMotion(clip);

            // as i see, all changes at animator controller marks it as dirty,
            // so we need to set it after all changes is done
            _animator.runtimeAnimatorController = _animatorController;


            if (_useFixedTime)
                _animator.PlayInFixedTime(state.nameHash);
            else
                _animator.Play(state.nameHash);
        }

        private void Start()
        {
            Play(_animationClip);
        }
    }
}
