using UnityEngine;
using UnityEngine.InputSystem;
using static RetroARGInput;
using UnityEngine.Events;
using System;

namespace Platformer
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "RetroARG/InputReader")]


    public class InputReader : ScriptableObject, IPlayerActions
    {

        RetroARGInput inputActions;

        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction EnableMouseControlCamera = delegate { };
        public event UnityAction DisableMouseControlCamera = delegate { };
        public event UnityAction<bool> Jump = delegate { };

        public Vector3 Direction => (Vector3)inputActions.Player.Move.ReadValue<Vector2>();

        void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new RetroARGInput();
                inputActions.Player.SetCallbacks(instance: this);
            }
            

        }

        public void EnablePlayerActions()
        {
            inputActions.Enable();

        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(arg0: context.ReadValue<Vector2>());
                
        }
        public void OnAttack1(InputAction.CallbackContext context)
        {
            // noop
        }

        public void OnAttack2(InputAction.CallbackContext context)
        {
            // noop
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Jump.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Jump.Invoke(false);
                    break;
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
        }

        bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
        

        public void OnRun(InputAction.CallbackContext context)
        {
            // noop
        }

        public void OnMouseControlCamera(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    EnableMouseControlCamera.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    DisableMouseControlCamera.Invoke();
                    break;
            }

        }
    }

}
