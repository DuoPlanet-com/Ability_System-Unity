using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.MonoHelper
{
    public class ProjectileEffectable : MonoBehaviour
    {
        /// <summary>
        /// Tracks an object to the projectile
        /// </summary>


        public Transform parentTransform;

        public GameObject objectToFollow;


        // Update is called once per frame
        void Update()
        {
            if (parentTransform == null)
            {
                //Destroy(objectToFollow);
                Destroy(gameObject);
            }
            else
            {
                objectToFollow.transform.position = parentTransform.position;
            }
        }
    }
}