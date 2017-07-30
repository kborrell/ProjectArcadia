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
        idle,
		die
    }

	Character m_character;

    State currentState = State.idle;

    [SerializeField]
    SkeletonAnimation[] skeletonAnimation;

	void Awake()
	{
		// init navmeshagent
		m_character = gameObject.GetComponent<Character>();
	}

    private void Update()
    {
        UpdateAnimation();
    }


    void UpdateAnimation()
    {
		var dir = m_character.getMovementDirection ();
		var isMoving = m_character.isMoving();

		if (m_character.Dead) 
		{
			if (currentState != State.die) 
			{
				currentState = State.die;
				ActivateSkeleton (SkeletonType.die);
			}
		}
        else if (isMoving)
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
		skeletonAnimation[4].gameObject.SetActive(type == SkeletonType.die);
    }
}
