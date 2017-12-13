using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.MonoHelper { 

    public class Projectilable : MonoBehaviour {

        /// <summary>
        /// 'Projectileable' is a class which is instantiated with script and attatched to the projectile.
        /// It is used for getting easy access to the MonoBehaviour functions, 
        /// and forward them onto the projectile class reference.
        /// 
        /// It is usually not useful for anything besides the ability system.
        /// </summary>


        private Projectiles.ImpactEffect[] impactEffects;

        // Projectile reference should be set in init and never is never to change since.
        private Projectiles.Projectile projectile;

        private MetaData.PlayerMetaData sender;

        private bool hasHit;

        public void Init(   Projectiles.Projectile projectileParam , 
                            Projectiles.ImpactEffect[] impactEffectsParam,
                            MetaData.PlayerMetaData senderParam)
        {
            hasHit = false;
            sender = senderParam;
            projectile = projectileParam;
            impactEffects = impactEffectsParam;

            if (impactEffects.Length == 0)
                print("<color=olive>Warning ! no impact effects found</color>\n"+
                    "Making no effort to collision check");
        }


        // Collision handling

        private void OnCollisionEnter(Collision collision)
        {
            hasHit = true;
            if (impactEffects.Length > 0) { 
                foreach (Projectiles.ImpactEffect effect in impactEffects) { 
                    if (effect.ValidateChecks(collision)) {
                        effect.OnCollision(collision, sender);
                        projectile.OnCollisionEnter(collision);
                    }
                }
            }
            else
            {
                projectile.OnCollisionEnter(collision);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (impactEffects.Length > 0)
            {
                foreach (Projectiles.ImpactEffect effect in impactEffects)
                {
                    if (effect.ValidateChecks(collision))
                    {
                        //effect.OnCollision(collision, sender);
                        projectile.OnCollisionStay(collision);
                    }
                }
            }
            else
            {
                projectile.OnCollisionStay(collision);
            }
        }

        private void OnCollisionExit(Collision collision)
        {

            if (impactEffects.Length > 0)
            {
                foreach (Projectiles.ImpactEffect effect in impactEffects)
                {
                    if (effect.ValidateChecks(collision))
                    {
                        //effect.OnCollision(collision, sender);
                        projectile.OnCollisionExit(collision);
                    }
                }
            }
            else
            {
                projectile.OnCollisionExit(collision);
            }
        }


        // Trigger handling
        
        private void OnTriggerEnter(Collider other)
        {
            hasHit = true;
            if (impactEffects.Length > 0)
            {
                foreach (Projectiles.ImpactEffect effect in impactEffects)
                {
                    if (effect.ValidateChecks(other))
                    {
                        effect.OnCollision(other, sender);
                        projectile.OnTriggerEnter(other);
                    }
                }
            }
            else
            {
                projectile.OnTriggerEnter(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (impactEffects.Length > 0)
            {
                foreach (Projectiles.ImpactEffect effect in impactEffects)
                {
                    if (effect.ValidateChecks(other))
                    {
                        //effect.OnCollision(other, sender);
                        projectile.OnTriggerStay(other);
                    }
                }
            }
            else
            {
                projectile.OnTriggerStay(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (impactEffects.Length > 0)
            {
                foreach (Projectiles.ImpactEffect effect in impactEffects)
                {
                    if (effect.ValidateChecks(other))
                    {
                        //effect.OnCollision(other, sender);
                        projectile.OnTriggerExit(other);
                    }
                }
            }
            else
            {
                projectile.OnTriggerExit(other);
            }
        }

        public bool HasHit()
        {
            return hasHit;
        }

    }
}