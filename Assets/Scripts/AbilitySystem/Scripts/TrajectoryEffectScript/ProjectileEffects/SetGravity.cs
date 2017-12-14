using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.ProjectileEffect
{

    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Projectile Effects/Set Gravity")]
    public class SetGravity : Projectiles.ProjectileEffect
    {

        public float gravity;
        

        public override void OnInit()
        {
            projectile.instantiatedObject.GetComponent<Rigidbody>().useGravity = false;
            ConstantForce gravityForce = projectile.instantiatedObject.AddComponent<ConstantForce>();
            gravityForce.force = new Vector3(0, gravity, 0);
            base.OnInit();
        }

    }
}