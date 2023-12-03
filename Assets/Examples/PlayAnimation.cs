using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private AniMate.AniMateComponent _aniMate;

    // Start is called before the first frame update
    void Start()
    {
        _aniMate.Play(_animationClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
