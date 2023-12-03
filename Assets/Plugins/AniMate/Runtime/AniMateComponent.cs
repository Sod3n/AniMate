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

        public void Play(AnimationClip clip)
        {
            if (_animatorController is null)
                CreateAnimatorController();

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

            _animator.PlayInFixedTime(state.nameHash);
        }

        private void CreateAnimatorController()
        {
            _animatorController = new UnityEditor.Animations.AnimatorController();
            _animatorController.name = "AniMate";
            _animatorController.hideFlags = HideFlags.HideAndDontSave;
            _animatorController.AddLayer("Layer 0");
        }
    }
}
