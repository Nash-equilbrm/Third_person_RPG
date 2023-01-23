using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager m_playeInputManager;
    [SerializeField] private Transform m_targetTransform;
    [SerializeField] private Transform m_cameraPivot;

    private Vector3 m_cameraFollowVelocity = Vector3.zero;
    [SerializeField] private float m_cameraFollowSpeed = 0.2f;
    [SerializeField] private float m_cameraLookSpeed = 1f;
    [SerializeField] private float m_cameraMaxVerticalAngle = 60f;
    [SerializeField] private float m_cameraMinVerticalAngle = -60f;

    private float m_lookAngle = 0f; // Camera looking up and down
    private float m_pivotAngle = 0f; // Camera looking left and right

    [SerializeField] private Transform m_cameraTransform;
    private float m_defaultPosition;
    [SerializeField] private float m_cameraCollisionRadius = 0.5f;
    [SerializeField] private LayerMask m_cameraCollisionLayer;
    [SerializeField] private float m_cameraCollisionOffset;
    [SerializeField] private float m_minimumCollisionOffset = 0.2f;

    private void Awake()
    {
        m_defaultPosition = m_cameraTransform.localPosition.z;
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, m_targetTransform.position,
            ref m_cameraFollowVelocity, m_cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotateAmount;
        Quaternion targetRotation;

        m_lookAngle = m_lookAngle + (m_playeInputManager.GetCameraInputX() * m_cameraLookSpeed); // mouse also can be joystick,...
        m_pivotAngle = m_pivotAngle - (m_playeInputManager.GetCameraInputY() * m_cameraLookSpeed);
        m_pivotAngle = Mathf.Clamp(m_pivotAngle, m_cameraMinVerticalAngle, m_cameraMaxVerticalAngle);

        // up and down
        rotateAmount = Vector3.zero;
        rotateAmount.y = m_lookAngle;
        targetRotation = Quaternion.Euler(rotateAmount);
        transform.rotation = targetRotation;

        // left and right
        rotateAmount = Vector3.zero;
        rotateAmount.x = m_pivotAngle;
        targetRotation = Quaternion.Euler(rotateAmount);
        m_cameraPivot.localRotation = targetRotation;

    }

    private void HandleCameraCollision()
    {
        float targetPosition = m_defaultPosition;
        RaycastHit hit;
        Vector3 direction = m_cameraTransform.position - m_cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(
            m_cameraPivot.transform.position, m_cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), m_cameraCollisionLayer))
        {
            float distance = Vector3.Distance(m_cameraPivot.position, hit.point);
            targetPosition =- (distance - m_cameraCollisionOffset);
        }

        if(Mathf.Abs(targetPosition) < m_minimumCollisionOffset)
        {
            targetPosition -= m_minimumCollisionOffset;
        }
        Vector3 cameraNewPosition = Vector3.zero;
        cameraNewPosition.z = Mathf.Lerp(m_cameraTransform.localPosition.z, targetPosition, 0.2f);
        m_cameraTransform.localPosition = cameraNewPosition;
    }


    // ======== publics ========
    public void HandleAllCameraInput()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollision();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(m_cameraPivot.transform.position, m_cameraCollisionRadius);
    }
}
