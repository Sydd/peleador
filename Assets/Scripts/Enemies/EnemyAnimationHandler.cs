using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeleton;
    [SerializeField] private List<Bone> _bones;

    private void Start()
    {
        Idle();
        _skeleton.AnimationState.Complete += AnimationCompleteHandler;
    }

    private void AnimationCompleteHandler(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "shoot" || trackEntry.Animation.Name == "hurt")
        {
            Idle();
        }
    }

    private void Update()
    {
    }

    public TrackEntry SetAnimation(int layer, string animation, bool loop)
    {
        return _skeleton.AnimationState.SetAnimation(layer, animation, loop);
    }

    public void Idle()
    {
        SetAnimation(0, "idle", true);
    }

    internal void Attack()
    {
        SetAnimation(0, "shoot", false);
    }

    internal void Hurt()
    {
        SetAnimation(0, "shoot", false);
    }
}

[System.Serializable]
public class Bone
{
    [field: SerializeField]
    public Rigidbody Body { get; set; }

    [field: SerializeField]
    public Collider Collider { get; set; }
}