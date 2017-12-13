using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem.TrajectorySystem.MonoHelper
{
    public class AOEImpactable : MonoBehaviour
    {

        float radius;

        SphereCollider sc;

        Rigidbody rb;

        ImpactEffect.AOEImpact AOE;

        // Use this for initialization
        void Start()
        {
            GrabComponents();
            sc.radius = radius;
            sc.isTrigger = true;

        }

        public void SetSettings(float AOERadius, ImpactEffect.AOEImpact impactEffect)
        {
            AOE = impactEffect;
            radius = AOERadius;
        }

        void GrabComponents()
        {
            if (GetComponent<Rigidbody>() != null)
            {
                rb = GetComponent<Rigidbody>();
            }
            else
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }

            if (GetComponent<SphereCollider>() != null)
            {
                sc = GetComponent<SphereCollider>();
            }
            else
            {
                sc = gameObject.AddComponent<SphereCollider>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {

        }

    }
}