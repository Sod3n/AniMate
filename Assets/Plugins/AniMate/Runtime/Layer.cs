using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace AniMate
{
    public class Layer : MonoBehaviour
    {
        [SerializeField] AvatarMask _avatarMask;
        [SerializeField] AnimatorLayerBlendingMode _blendingMode;

        public AvatarMask AvatarMask { get { return _avatarMask; } }
        public AnimatorLayerBlendingMode BlendingMode { get { return _blendingMode; } }
        public AnimatorControllerLayer AnimatorLayer { get; set; }
        public int AnimatorLayerIndex { get; set; }
    }
}
