using UnityEngine;
using Cinemachine;
using System;
using System.Collections;

namespace Platformer
{
    public class CameraManager : MonoBehaviour 
    {
        [Header("References")]
        [SerializeField] InputReader inputReader;
        [SerializeField] CinemachineFreeLook freeLookCam;

        [Header("Settings")]
        [SerializeField, Range(.5f, 3f)] float speedMultiplayer = 1f;

        bool isRMBPressed;
        bool cameraMovementLock;

        void OnEnable()
        {
            inputReader.Look += OnLook;
            inputReader.EnableMouseControlCamera += OnEnableMouseControlCamera;
            inputReader.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }

        void OnDisable()
        {
            inputReader.Look += OnLook;
            inputReader.EnableMouseControlCamera += OnEnableMouseControlCamera;
            inputReader.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }
        private void OnLook(Vector2 camMovement, bool isDeviceMouse)
        {
            if (cameraMovementLock) return;

            if (isDeviceMouse && !isRMBPressed) return;

            float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

            freeLookCam.m_XAxis.m_InputAxisValue = camMovement.x * speedMultiplayer * deviceMultiplier;
            freeLookCam.m_YAxis.m_InputAxisValue = camMovement.y * speedMultiplayer * deviceMultiplier;
        }

        void OnDisableMouseControlCamera()
        {
            isRMBPressed = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            freeLookCam.m_XAxis.m_InputAxisValue = 0f;
            freeLookCam.m_YAxis.m_InputAxisValue = 0f;
        }

        void OnEnableMouseControlCamera()
        {
            isRMBPressed = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(DisableMouseForFrame());
        }

         IEnumerator DisableMouseForFrame()
        {
            cameraMovementLock = true;
            yield return new WaitForEndOfFrame();
            cameraMovementLock = false;

        }

        

    }

}
