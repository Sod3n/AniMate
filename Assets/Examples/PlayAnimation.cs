using AniMate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private AniMateController _aniMate;
    [SerializeField] private Layer _layer;

    void Start()
    {
        var state = _aniMate.Play(_animationClip, _layer);
        state.OnEnd += WaiterOne;
        state.Duration = 0.1f;

    }

    private void WaiterOne()
    {
        var state = _aniMate.Play(_animationClip, _layer);
        state.OnEnd += WaiterTwo;
        state.Duration = 1f;
        Debug.Log("End 1 " + gameObject.name + " " + _layer.AnimatorLayer);
    }
    private void WaiterTwo()
    {
        var state = _aniMate.Play(_animationClip, _layer);
        state.Duration = 3f;
        Debug.Log("End 2");
    }
}
