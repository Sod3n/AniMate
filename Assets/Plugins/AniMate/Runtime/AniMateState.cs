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

        public void Reset()
        {
            State = null;
            OnEnd = () => { };
        }
    }
}
