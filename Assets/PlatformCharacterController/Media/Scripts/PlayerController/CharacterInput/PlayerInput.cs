using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformCharacterController
{
    [RequireComponent(typeof(MovementCharacterController))]
    public class PlayerInput : Inputs
    {
        public override float GetHorizontal()
        {
            return Input.GetAxis("Horizontal");
        }

        public override float GetVertical()
        {
            return Input.GetAxis("Vertical");
        }

        public override bool Jump()
        {
            return Input.GetButtonDown("Jump");
        }

        public override bool Dash()
        {
            return Input.GetButtonDown("Dash");
        }

        public override bool JetPack()
        {
            return Input.GetButton("JetPack");
        }

        public override bool Parachute()
        {
            return Input.GetButtonDown("Parachute");
        }

        public override bool DropCarryItem()
        {
            return Input.GetButtonDown("PickUp");
        }
    }
}