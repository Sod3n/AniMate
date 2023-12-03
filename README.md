Small plugin adding animation tool.

## How to install

1. Open Package Manager at Unity project.
2. Click on “plus” at left up corner.
3. Choose “Add package from git URL…”, past link https://github.com/Sod3n/AniMate.git?path=/Assets/Plugins/AniMate and click “add” button.

## How to play animation

1. Add Animator to gameobject.
2. Then add AniMateComponent to gameobject and attach already created animator component.
3. Now you can use this AniMateComponent to play AnimationClip. For example:

```csharp
public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private AniMateComponent _aniMate;

    void Start()
    {
        _aniMate.Play(_animationClip);
    }
}
```

## AniMateState

Play method of AniMateComponent return AniMateState. For now its pretty small class:

```csharp
public class AniMateState
{
    public AnimatorState State { get; set; }
    public Action OnEnd { get; set; }

    public void Reset()
    {
        /* Reset logic */
    }
}
```

As you can see it contains OnEnd Action. It invokes only after not looping animation fully played. For example:

```csharp
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
```

In this example only “End 2” will be showed in console.

Also you can modify some AnimatorState properties and that will affect on animation.
