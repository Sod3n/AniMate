using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace AniMate
{
    public class AniMateState
    {
        public AnimatorState State { get; set; }

        public Action OnEnd { get; set; } = () => { };

        /// <summary>
        /// Time in seconds the animation plays
        /// </summary>
        public float Duration 
        { 
            get => DefaultDuration / State.speed; 
            set => State.speed = DefaultDuration / value;
        }

        public float DefaultDuration { get; set; }

        public void Reset()
        {
            State = null;
            OnEnd = () => { };
            DefaultDuration = 0f;
        }
    }
}
