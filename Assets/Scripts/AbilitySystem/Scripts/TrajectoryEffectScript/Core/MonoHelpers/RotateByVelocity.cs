using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.MonoHelper
{
    public class RotateByVelocity : MonoBehaviour
    {

        public Vector3 offset;

        Rigidbody rb;

        private void Awake()
        {
            if (GetComponent<Rigidbody>() != null)
            {
                rb = GetComponent<Rigidbody>();
            } else
            {
                print("<color=red>Error! No rigidbody found on projectile</color>");
            }
        }

        private void Update()
        {
            if (rb.velocity != Vector3.zero) { 
                Vector3 newVectorRotation = new Vector3(
                    rb.velocity.normalized.x,
                    rb.velocity.normalized.y,
                    rb.velocity.normalized.z);

                Quaternion newRotation = Quaternion.LookRotation(newVectorRotation,Vector3.up);

                rb.rotation = newRotation * Quaternion.Euler(offset);
            }

            //  transform.rotation = Quaternion.Euler(Vector3.up);
        }
    }
}