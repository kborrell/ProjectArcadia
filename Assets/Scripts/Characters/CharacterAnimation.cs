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

    enum SkeletonType
    {
        front,
        back,
        side
    }

    State currentState = State.idle;

    [SerializeField]
    SkeletonAnimation[] skeletonAnimation;

    [SerializeField]
    CharacterMovement characterMovement;

    private void Update()
    {
        UpdateAnimation();
    }


    void UpdateAnimation()
    {
        var dir = characterMovement.GetFacingDirection();
        var isMoving = characterMovement.IsMoving();

        if (isMoving)
        {
            if (dir == new Vector3(0, 0, 1) && currentState != State.up)
            {
                currentState = State.up;
                ActivateSkeleton(SkeletonType.back);
                skeletonAnimation[1].AnimationState.SetAnimation(0, "1_back_walk", true);
                skeletonAnimation[0].Initialize(true);
            }
            else if (dir == new Vector3(0, 0, -1) && currentState != State.down)
            {
                currentState = State.down;
                ActivateSkeleton(SkeletonType.front);
                skeletonAnimation[0].AnimationState.SetAnimation(0, "1_front_walk", true);
                skeletonAnimation[0].Initialize(true);
            }
            else if (dir == new Vector3(1, 0, 0) && currentState != State.right)
            {
                ActivateSkeleton(SkeletonType.side);
                skeletonAnimation[2].transform.localScale = new Vector3(-1, 1, 1);
                skeletonAnimation[2].AnimationState.SetAnimation(0, "1_side_walk", true);
                skeletonAnimation[0].Initialize(true);
            }
            else if (dir == new Vector3(-1, 0, 0) && currentState != State.left)
            {
                ActivateSkeleton(SkeletonType.side);
                skeletonAnimation[2].transform.localScale = new Vector3(1, 1, 1);
                skeletonAnimation[2].AnimationState.SetAnimation(0, "1_side_walk", true);
                skeletonAnimation[0].Initialize(true);
            }
        }
        else
        {
            currentState = State.idle;
            ActivateSkeleton(SkeletonType.front);
            
            skeletonAnimation[0].AnimationState.SetAnimation(1, "1_idle", true);
            skeletonAnimation[0].Initialize(true);
        }
    }

    void ActivateSkeleton(SkeletonType type)
    {
        skeletonAnimation[0].gameObject.SetActive(type == SkeletonType.front);
        skeletonAnimation[1].gameObject.SetActive(type == SkeletonType.back);
        skeletonAnimation[2].gameObject.SetActive(type == SkeletonType.side);
    }
}
