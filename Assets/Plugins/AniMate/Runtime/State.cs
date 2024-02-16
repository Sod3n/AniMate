using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace AniMate
{
    public class State
    {
        public AnimatorState AnimatorState { get; set; }

        public Action OnEnd { get; set; } = () => { };

        public bool IsEnded { get; private set; }

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
            if(!IsEnded) OnEnd.Invoke();


            AnimatorState = null;
            IsEnded = false;
            OnEnd = () => { IsEnded = true; };
            DefaultDuration = 0f;
        }

        public State()
        {
            Reset();
        }
    }
}
