using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/New Projectile Ability")]
    public sealed class ModularProjectileAbility : ModularAbility
    {

        /// <summary>
        /// This is the main class for the modular projectile script
        /// </summary>


        [Header("Projectile Settings")]


        [Header("Direction")]

        [Tooltip("How much force should be applied\n\n" +

            "Only a useful option if using AddForce")]
        public float force = 500;


        [Header("Projectile")]

        [Tooltip("The projectile asset, which will be fired\n\n" +

            "Only valid if not using raycast as TrajectoryType")]
        public Projectiles.Projectile projectile;

        [Space(10)]

        [Tooltip("The ProjectileEffect asset which dictates any effects that should be played on the projectile and upon fireing")]
        public Projectiles.ProjectileEffect[] projectileEffects;



        public override void OnUpdate(float input, ObjectMetaData reciever)
        {
            base.OnUpdate(input, reciever);

            if (projectile.instantiatedObject != null)
            {
                foreach (Projectiles.ProjectileEffect PE in projectileEffects)
                {

                    PE.OnUpdate();
                }
            }
        }

        public override string GenerateDescription()
        {
            throw new NotImplementedException();
        }

        public override void Trigger(ObjectMetaData reciever)
        {
            base.Trigger(reciever);

            InstantiateProjectile();
            AddEffects();
        }

        void InstantiateProjectile()
        {
            Vector3 spawnPoint = GetProjectileOrigin();

            if (origin == TrajectoryOrigins.CAMERA)
                projectile.InstantiateObject(spawnPoint, Camera.main.transform.rotation, sender, impactEffects);
            else
                projectile.InstantiateObject(spawnPoint, sender.transform.rotation, sender, impactEffects);

            if (directionSpace == Space.World)
            {
                if (projectile.instantiatedObject.GetComponent<Rigidbody>() == null)
                    projectile.instantiatedObject.AddComponent(typeof(Rigidbody));

                projectile.instantiatedObject.GetComponent<Rigidbody>().AddForce(direction * force);
            }
            else
            {
                if (projectile.instantiatedObject.GetComponent<Rigidbody>() == null)
                    projectile.instantiatedObject.AddComponent(typeof(Rigidbody));

                projectile.instantiatedObject.GetComponent<Rigidbody>().AddRelativeForce(direction * force);
            }
        }

        void AddEffects()
        {
            foreach (Projectiles.ProjectileEffect PE in projectileEffects)
            {
                PE.SetProjectile(projectile);
                PE.OnInit();
            }
        }
    }
}
