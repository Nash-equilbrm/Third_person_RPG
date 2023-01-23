using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocoMotion : MonoBehaviour
{
    private PlayerInputManager m_inputManager;
    [SerializeField] private Transform m_cameraObject;
    private Vector3 m_moveDirection;

    private Rigidbody m_rb;
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private float m_rotateSpeed = 10f;


    private void Awake()
    {
        m_inputManager = GetComponent<PlayerInputManager>();
        m_rb = GetComponent<Rigidbody>();
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        m_moveDirection = m_cameraObject.forward * m_inputManager.GetVerticalInput();
        m_moveDirection = m_moveDirection + m_cameraObject.right * m_inputManager.GetHorizontalInput();
        
        m_moveDirection.Normalize();

        m_moveDirection.y = 0;
        // velocity = move direction
        Vector3 velocity = m_moveDirection * m_speed;
        m_rb.velocity = velocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = m_cameraObject.forward * m_inputManager.GetVerticalInput();
        targetDirection = targetDirection + m_cameraObject.right * m_inputManager.GetHorizontalInput();

        targetDirection.Normalize();

        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, m_rotateSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    
}
