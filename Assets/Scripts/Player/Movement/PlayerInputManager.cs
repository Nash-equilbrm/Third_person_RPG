using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerControl m_playerActionControl;
    private PlayerAnimatorManager m_animatorManager;

    private Vector2 m_movementInput;

    private float m_hzInput;
    private float m_vInput;
    private float m_moveAmount;

    private Vector2 m_cameraInput;
    private float m_cameraInputX;
    private float m_cameraInputY;


    private void Awake()
    {
        m_animatorManager = GetComponent<PlayerAnimatorManager>();
    }
    private void OnEnable()
    {
        if(m_playerActionControl is null)
        {
            m_playerActionControl = new PlayerControl();
            m_playerActionControl.PlayerMovement.Movement.performed += context => {
                m_movementInput = context.ReadValue<Vector2>();
            };
            m_playerActionControl.PlayerMovement.Camera.performed += context => {
                m_cameraInput = context.ReadValue<Vector2>();
            };
        }
        m_playerActionControl.Enable();
    }

    private void OnDisable()
    {
        m_playerActionControl.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput(); // button inputs
        HandleCameraInput(); // mouse inputs
        // jump input, shoot input, ...
    }
    
    private void HandleMovementInput()
    {
        m_vInput = m_movementInput.y;
        m_hzInput = m_movementInput.x;

        m_moveAmount = Mathf.Clamp01(Mathf.Abs(m_hzInput) + Mathf.Abs(m_vInput));
        m_animatorManager.UpdateAnimatorParams(0, m_moveAmount);
    }

    private void HandleCameraInput()
    {
        m_cameraInputX = m_cameraInput.x;
        m_cameraInputY = m_cameraInput.y;
    }




    #region getter setter

    public float GetHorizontalInput()
    {
        return m_hzInput;
    }

    public float GetVerticalInput()
    {
        return m_vInput;
    }

    public float GetCameraInputX()
    {
        return m_cameraInputX;
    }

    public float GetCameraInputY()
    {
        return m_cameraInputY;
    }
    #endregion

}
