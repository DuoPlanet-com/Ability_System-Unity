using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.ProjectileEffect
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Projectile Effects/Rotate By Velocity")]
    public class RotateByVelocity : Projectiles.ProjectileEffect
    {
        public Vector3 offset;
        MonoHelper.RotateByVelocity monoHelper;
        public override void OnInit()
        {
            monoHelper = projectile.instantiatedObject.AddComponent<MonoHelper.RotateByVelocity>() as MonoHelper.RotateByVelocity;
            monoHelper.offset = offset;
            base.OnInit();
        }
    }
}