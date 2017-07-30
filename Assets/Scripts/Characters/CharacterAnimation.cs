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
        side,
        idle
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
            }
            else if (dir == new Vector3(0, 0, -1) && currentState != State.down)
            {
                currentState = State.down;
                ActivateSkeleton(SkeletonType.front);
            }
            else if (dir == new Vector3(1, 0, 0) && currentState != State.right)
            {
                currentState = State.right;
                ActivateSkeleton(SkeletonType.side);
                skeletonAnimation[2].transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (dir == new Vector3(-1, 0, 0) && currentState != State.left)
            {
                currentState = State.left;
                ActivateSkeleton(SkeletonType.side);
                skeletonAnimation[2].transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if(currentState != State.idle)
        {
            currentState = State.idle;
            ActivateSkeleton(SkeletonType.idle);
        }
    }

    void ActivateSkeleton(SkeletonType type)
    {
        skeletonAnimation[0].gameObject.SetActive(type == SkeletonType.front);
        skeletonAnimation[1].gameObject.SetActive(type == SkeletonType.back);
        skeletonAnimation[2].gameObject.SetActive(type == SkeletonType.side);
        skeletonAnimation[3].gameObject.SetActive(type == SkeletonType.idle);
    }
}
