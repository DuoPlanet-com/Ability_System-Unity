using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitySystem.TrajectorySystem.Projectiles
{
    public abstract class ProjectileEffect : ScriptableObject
    {
        /// <summary>
        /// The 'ProjectileEffect' class is an asset that contains effects,
        /// which are applied when the projectile spawns.
        /// 
        /// Think muzzle flash and trails.
        /// </summary>


        protected Projectile projectile;


        public virtual void OnInit()
        {
            if (projectile == null)
            {
                Debug.Log("<color=red>Error ! Projectile not found</color>\n"+
                    "Please set the projectile");
            }
        }

        public virtual void OnUpdate()
        {
            if (projectile == null)
            {
                Debug.Log("<color=red>Error ! Projectile not found</color>\n"+
                    "Please set the projectile");
            }
        }


        public void SetProjectile(Projectile project)
        {
            projectile = project;
        }

    }
}