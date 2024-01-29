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
        public float Speed 
        { 
            get => DefaultSpeed / State.speed; 
            set => State.speed = DefaultSpeed / value;
        }

        public float DefaultSpeed { get; set; }

        public void Reset()
        {
            State = null;
            OnEnd = () => { };
            DefaultSpeed = 0f;
        }
    }
}
