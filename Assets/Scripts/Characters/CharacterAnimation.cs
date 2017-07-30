using UnityEngine;
using Spine.Unity;

public class CharacterAnimation : MonoBehaviour
{
    enum State
    {
        up,
        down,
        left,
        right,
        idle,
        die
    }

    State currentState = State.idle;

    [SerializeField]
    SkeletonDataAsset[] skeletons;

    [SerializeField]
    SkeletonAnimation skeletonAnimation;

    [SerializeField]
    CharacterMovement characterMovement;

    private void Update()
    {
        UpdateAnimation();
    }


    void UpdateAnimation()
    {
        var dir = characterMovement.GetFacingDirection();
        if(dir == new Vector3(0,0,1) && currentState != State.up)
        {
            currentState = State.up;
            skeletonAnimation.skeletonDataAsset = skeletons[0];
            skeletonAnimation.Initialize(true);
            skeletonAnimation.AnimationState.SetAnimation(0, "1_back_walk", true);
        }
        else if(dir == new Vector3(0, 0, -1) && currentState != State.down)
        {
            currentState = State.down;
            skeletonAnimation.skeletonDataAsset = skeletons[1];
            skeletonAnimation.Initialize(true);
            skeletonAnimation.AnimationState.SetAnimation(0, "1_front_walk", true);
        }

        //characterMovement.GetSpeed();
    }
}
