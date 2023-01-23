using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    private Animator m_animator;
    private int m_horizontal, m_vertical;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_horizontal = Animator.StringToHash("Horizontal");
        m_vertical = Animator.StringToHash("Vertical");
    }

    private Vector2 SnapMovementInput(float horizontalMovement, float verticalMovement)
    {
        float snappedHorizontal, snappedVertical;
        // horizontal
        if(horizontalMovement > 0f && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if(horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if(horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if(horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1f;
        }
        else
        {
            snappedHorizontal = 0f;
        }

        // vertical
        if (verticalMovement > 0f && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0f;
        }

        return new Vector2(snappedHorizontal, snappedVertical);
    }


    // ================ publics ================
    public void UpdateAnimatorParams(float horizontalMovement, float verticalMovement)
    {
        Vector2 snappedInput =  SnapMovementInput(horizontalMovement, verticalMovement);
        m_animator.SetFloat(m_horizontal, snappedInput.x, 0.1f, Time.deltaTime);
        m_animator.SetFloat(m_vertical, snappedInput.y, 0.1f, Time.deltaTime);
    }

}
