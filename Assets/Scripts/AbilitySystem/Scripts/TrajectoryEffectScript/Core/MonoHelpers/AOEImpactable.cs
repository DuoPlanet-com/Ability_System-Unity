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

        Projectiles.AOEEffect[] effectsToPlay;

        ImpactEffects.AOEImpact AOE;

        MetaData.PlayerMetaData sender;

        float timer;

        // Use this for initialization
        void Start()
        {
            GrabComponents();
            rb.isKinematic = true;
            sc.radius = radius;
            sc.isTrigger = true;

            timer = AOE.lifeTime;

        }

        private void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            } else
            {
                timer = 0;
                Destroy(gameObject);
            }
        }

        public void SetSettings(ImpactEffects.AOEImpact impactEffect, MetaData.PlayerMetaData senderParam)
        {
            AOE = impactEffect;
            radius = AOE.radius;
            sender = senderParam;
            effectsToPlay = AOE.effectsToPlay;
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
            foreach (Projectiles.AOEEffect effect in effectsToPlay)
            {
                effect.OnCollision(other , sender , transform.position, AOE);
            }
            if (AOE.oneTime) {
                Destroy(gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            foreach (Projectiles.AOEEffect effect in effectsToPlay)
            {
                effect.OnCollision(other, sender, transform.position, AOE);
            }
        }

    }
}