using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputManager m_inputManager;
    private PlayerLocoMotion m_locoMotion;

    [SerializeField] private CameraManager m_camera;
    private void Awake()
    {
        m_inputManager = GetComponent<PlayerInputManager>();
        m_locoMotion = GetComponent<PlayerLocoMotion>();
    }

    private void Update()
    {
        m_inputManager.HandleAllInputs();
    }


    private void FixedUpdate()
    {
        m_locoMotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        if (m_camera != null)
        {
            m_camera.HandleAllCameraInput();
        }
    }
}
