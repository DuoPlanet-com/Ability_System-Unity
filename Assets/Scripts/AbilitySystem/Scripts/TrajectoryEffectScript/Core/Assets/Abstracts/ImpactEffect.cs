using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitySystem.TrajectorySystem.Projectiles
{
    public abstract class ImpactEffect : ScriptableObject
    {

        /// <summary>
        /// Impact effects occur on impact, and it passes along the data of 
        /// the collision as well as the sender.
        /// </summary>


        [Tooltip("The CollisionCheck asset which dictates what the projectile may and may not hit.")]
        public CollisionCheck[] collisionChecks;

        protected Projectile projectile;

        public abstract void OnCollision(Collision collision, MetaData.PlayerMetaData sender);

        public abstract void OnCollision(Collider other, MetaData.PlayerMetaData sender);

        public bool ValidateChecks(Collision collision)
        {
            if (collisionChecks.Length > 0) { 
                foreach (var check in collisionChecks)
                {
                    if (check.Validate(collision))
                    {
                        return true;
                    }
                }
            } else
            {
                return true;
            }
            return false;
        }

        public bool ValidateChecks(Collider other)
        {
            if (collisionChecks.Length > 0)
            {
                foreach (var check in collisionChecks)
                {
                    if (check.Validate(other))
                    {
                        return true;
                    }
                }
            } else
            {
                return true;
            }
            return false;
        }

        public void SetProjectile(Projectile newProjectile)
        {
            if (projectile == null)
            {
                Debug.Log("<color=blue>Notice! Projectile has already been set</color>\n"+
                    "This could potentially be unwanted");
            }
            projectile = newProjectile;
        }


        protected Vector3 AverageCollisionPosition(Collision collision)
        {
            if (collision.contacts.Length > 0)
            {
                Vector3 averagePos = Vector3.zero;

                foreach (ContactPoint point in collision.contacts)
                {
                    Vector3 pos = point.point;
                    averagePos += pos;
                }
                return averagePos / collision.contacts.Length;

            }
            else
            {
                throw new System.ArgumentNullException();
            }
        }

        protected Vector3 AverageCollisionNormal(Collision collision)
        {
            if (collision.contacts.Length > 0)
            {
                Vector3 averageRot = Vector3.zero;

                foreach (ContactPoint point in collision.contacts)
                {
                    Vector3 rot = point.normal;
                    averageRot += rot;
                }
                return averageRot / collision.contacts.Length;

            }
            else
            {
                throw new System.ArgumentNullException();
            }
        }

    }
}