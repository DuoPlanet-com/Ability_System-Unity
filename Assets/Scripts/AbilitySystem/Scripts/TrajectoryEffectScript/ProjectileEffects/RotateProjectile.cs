using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.ProjectileEffect
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Projectile Effects/Rotate Projectile")]
    public class RotateProjectile : Projectiles.ProjectileEffect
    {

        public bool stopAfterHit;

        public Vector3 direction = Vector3.up;
        public float speed = 5;
        public Space space = Space.Self;

        public override void OnInit()
        {
            base.OnInit();
        }


        public override void OnUpdate()
        {
            if (!projectile.instantiatedObject.GetComponent<MonoHelper.Projectilable>().HasHit() || !stopAfterHit) { 
                projectile.instantiatedObject.transform.Rotate(direction, speed * 10 * Time.deltaTime, space);
            }
            base.OnUpdate();
        }

    }
}
