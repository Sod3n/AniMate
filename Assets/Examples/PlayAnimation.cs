using AniMate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private AniMateComponent _aniMate;

    void Start()
    {
        var state = _aniMate.Play(_animationClip);
        state.OnEnd += WaiterOne;

        state = _aniMate.Play(_animationClip);
        state.OnEnd += WaiterTwo;
    }

    private void WaiterOne()
    {
        Debug.Log("End 1");
    }
    private void WaiterTwo()
    {
        Debug.Log("End 2");
    }
}
