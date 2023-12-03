# AniMate
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
