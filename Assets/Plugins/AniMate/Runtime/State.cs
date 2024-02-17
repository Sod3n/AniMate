using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace AniMate
{
    public class State
    {
        public Animator Animator { get; }
        public AnimatorState AnimatorState { get; set; }
        public Layer Layer { get; }

        public event Action OnEnd;

        /// <summary>
        /// Time in seconds the animation plays
        /// </summary>
        public float Duration 
        { 
            get => DefaultDuration / AnimatorState.speed; 
            set => AnimatorState.speed = DefaultDuration / value;
        }

        public float DefaultDuration { get; set; }

        public void Reset()
        {
            AnimatorState = null;
            var onEnd = OnEnd;
            OnEnd = () =>
            {
                if (Layer != null && Animator != null)
                    Animator.SetLayerWeight(Layer.AnimatorLayerIndex, 0f);
            };
            DefaultDuration = 0f;

            onEnd?.Invoke();
        }

        public State(Layer layer, Animator animator)
        {
            Layer = layer;
            Animator = animator;
            Reset();
        }
    }
}
