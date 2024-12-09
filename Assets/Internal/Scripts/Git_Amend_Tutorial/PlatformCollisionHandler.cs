using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{

    public class PlatformCollisionHandler : MonoBehaviour
    {
        Transform platform;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                ContactPoint contact = other.GetContact(0);
                if (contact.normal.y < .5f) return;

                platform = other.transform;
                transform.SetParent(platform);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(null);
                platform = null;
            }   
        }
    }
}
