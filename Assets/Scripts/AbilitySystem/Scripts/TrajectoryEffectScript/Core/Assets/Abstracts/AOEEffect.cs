using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem.TrajectorySystem.Projectiles
{

    public abstract class AOEEffect : ScriptableObject
    {

        public Projectiles.CollisionCheck[] collisionChecks;

        protected Collider other;
        protected MetaData.PlayerMetaData sender;
        protected Vector3 center;
        protected ImpactEffects.AOEImpact AOE;


        public void OnCollision(Collider otherCollider, MetaData.PlayerMetaData senderParam, Vector3 AOEcenter, ImpactEffects.AOEImpact impactEffect)
        {
            AOE = impactEffect;
            center = AOEcenter;
            sender = senderParam;
            other = otherCollider;

            foreach (Projectiles.CollisionCheck check in collisionChecks)
            {
                if (check.Validate(other))
                {
                    OnHit();
                    break;
                }
            }

        }


        public abstract void OnHit();


        protected float GetDistance()
        {
            return Vector3.Distance(other.transform.position, center);
        }

        protected float GetModifier()
        {
            return GetDistance() / AOE.radius;
        }
    }
}
