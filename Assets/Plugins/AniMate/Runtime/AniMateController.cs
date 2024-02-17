using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditorInternal.VersionControl.ListControl;

namespace AniMate
{
    public class AniMateController : MonoBehaviour
    {
        [SerializeField] private Avatar _avatar;
        public Animator Animator { get => _animator; }

        private AnimatorController _animatorController;
        private Animator _animator;
        private bool _isInitialized = false;
        private Dictionary<int, State> layerToState = new Dictionary<int, State>();

        private void TryInitialize()
        {
            if(_isInitialized) return;

            if (!gameObject.TryGetComponent(out _animator))
            {
                _animator = gameObject.AddComponent<Animator>();
                _animator.avatar = _avatar;
            }

            CreateAnimatorController();

            layerToState.Add(0, new State(null, _animator));

            _isInitialized = true;
        }

        public State Play(AnimationClip clip, Layer layer = null)
        {
            TryInitialize();

            TryAddLayer(layer);

            var animatorLayer = layer?.AnimatorLayer ?? _animatorController.layers[0];
            int layerIndex = layer?.AnimatorLayerIndex ?? 0;
            var state = layerToState[layerIndex];

            layerToState[layerIndex].Reset();

            AddClipToAnimator(clip, animatorLayer, layerIndex, state);

            state.OnEnd += () => { ClearStates(animatorLayer); };

            PlayClip(layerIndex);

            if (!clip.isLooping)
            {
                var waitEnd = WaitEnd(layerIndex, state);
                StartCoroutine(waitEnd);
                state.OnEnd += () => { StopCoroutine(waitEnd); };
            }

            return state;
        }

        public void AddClipToAnimator(AnimationClip clip, AnimatorControllerLayer layer, int layerIndex, State state)
        {
            if (_animator is null)
            {
                throw new Exception("Animator is not assigned!");
            }

            state.AnimatorState = _animatorController.AddMotion(clip, layerIndex);
            state.DefaultDuration = clip.length;
        }

        public IEnumerator WaitEnd(int layerIndex, State state)
        {
            
            while (_animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime < 1)
            {
                yield return null;
            }
            state.Reset();
        }

        private void TryAddLayer(Layer layer)
        {
            if (layer != null && layer.AnimatorLayer == null)
            {
                var animatorLayer = new AnimatorControllerLayer();
                animatorLayer.avatarMask = layer.AvatarMask;
                animatorLayer.blendingMode = layer.BlendingMode;
                animatorLayer.stateMachine = new AnimatorStateMachine();
                animatorLayer.name = layer.AvatarMask.name;
                animatorLayer.defaultWeight = 1f;
                _animatorController.AddLayer(animatorLayer);
                layer.AnimatorLayer = _animatorController.layers.Last();
                layer.AnimatorLayerIndex = _animatorController.layers.Length - 1;
                layerToState.Add(layer.AnimatorLayerIndex, new State(layer, _animator));
            }
        }

        private void CreateAnimatorController()
        {
            _animatorController = new AnimatorController();
            _animatorController.name = "AniMate" + _animatorController.GetInstanceID();
            _animatorController.hideFlags = HideFlags.HideAndDontSave;
            _animatorController.AddLayer("Layer 0");
            _animator.runtimeAnimatorController = _animatorController;
        }

        private void ClearStates(AnimatorControllerLayer layer)
        {
            var structStates = layer.stateMachine.states;

            foreach (var structState in structStates)
            {
                layer.stateMachine.RemoveState(structState.state);
            }
        }

        private void PlayClip(int layerIndex)
        {
            _animator.Update(Time.deltaTime);
            _animator.PlayInFixedTime(layerToState[layerIndex].AnimatorState.nameHash);
        }
    }
}
