using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ChronaAnimator : MonoBehaviour
{
    public SkeletonAnimation anim;
    public AnimationReferenceAsset idle, clock, walkR;
    void Start() {}
    void Update() {}

    public void StartMoving()
    {
        Debug.Log("start moving!");
        PlayLooping(walkR);
    }
    public void StopMoving()
    {
        Debug.Log("idle");
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
        Debug.Log("set anim");
        Debug.Log(a);
        Debug.Log(anim);
        anim.state.SetAnimation(0, a, loop).TimeScale = 1f;
    }
    public void AddAnim(AnimationReferenceAsset a, bool loop)
    {
        Spine.TrackEntry animEntry = anim.state.AddAnimation(1, a, loop, delay: 0f);
        //animEntry.Complete += AnimationEntry_Complete;
    }
    // This would change one skin for another after playing a non-looping blend
    /*public void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        anim.Skeleton.SetSkin("")
    }*/

}
