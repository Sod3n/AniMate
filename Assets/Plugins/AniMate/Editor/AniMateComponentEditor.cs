using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AniMate
{
    [CustomEditor(typeof(AniMateComponent))]
    public class AniMateComponentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var component = (AniMateComponent)target;
            
            if (component.PreviewClip == null) return;

            component.AddClipToAnimator(component.PreviewClip);
            EditorUtility.SetDirty(component.Animator);
        }
    }
}
