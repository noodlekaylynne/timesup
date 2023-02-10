using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ChronaAnimator : MonoBehaviour
{
    public SkeletonAnimation anim;
    public SkeletonGraphic graphic;
    public AnimationReferenceAsset idle, clock, walkR;
    void Start() {}
    void Update() {}

    private AnimationReferenceAsset currentAnim;
    public void StartMoving()
    {
        PlayLooping(walkR);
    }
    public void StopMoving()
    {
        PlayLooping(idle);
        AddAnim(clock, true);
    }
    public void PlayLooping(AnimationReferenceAsset a)
    {
        SetAnim(a, true);
    }
    public void PlayOnce(AnimationReferenceAsset a)
    {
        SetAnim(a, false);
    }
    public void SetAnim(AnimationReferenceAsset a, bool loop)
    {
        if (a != currentAnim)
        {
            currentAnim = a;
            anim.state.SetAnimation(0, a, loop).TimeScale = 1f;
        }
    }
    public void AddAnim(AnimationReferenceAsset a, bool loop)
    {
        Spine.TrackEntry animEntry = anim.state.AddAnimation(1, a, loop, delay: 0f);
    }
    public void ToggleColour(bool isGreen)
    {
        anim.Skeleton.SetSkin(isGreen ? "Green" : "Purple");
    }

}
