using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/New trajectory")]
    public sealed class TrajectoryEffectScript : EffectScript
    {

        /// <summary>
        /// This is the main class for the modular projectile script
        /// </summary>


        [Header("Trajectory Settings")]

        [Tooltip("Where should the object be spawned")]
        public TrajectoryOrigins origin = TrajectoryOrigins.CAMERA;


        [Header("Origin")]

        [Tooltip("An offset which is applied to the origin")]
        public Vector3 originOffset = Vector3.zero;

        [Tooltip("In what space should the offset be applied?")]
        public Space offsetSpace = Space.Self;


        [Header("Direction")]

        [Tooltip("The direction to fire in")]
        public Vector3 direction = Vector3.forward;

        [Tooltip("In what space should this direction be?")]
        public Space directionSpace = Space.Self;

        [Tooltip("How much force should be applied\n\n"+
            
            "Only a useful option if using AddForce")]
        public float force = 500;


        [Header("Projectile")]

        [Tooltip("The projectile asset, which will be fired\n\n"+
            
            "Only valid if not using raycast as TrajectoryType")]
        public Projectiles.Projectile projectile;

        [Space(10)]

        [Tooltip("The ProjectileEffect asset which dictates any effects that should be played on the projectile and upon fireing")]
        public Projectiles.ProjectileEffect[] projectileEffects;

        public Projectiles.ImpactEffect[] impactEffects;

        public enum TrajectoryTypes
        {
            RAYCAST, ADDFORCE
        }

        public enum TrajectoryOrigins
        {
            CAMERA, SENDER_ORIGIN
        }

        public override void OnUpdate(float input, ObjectMetaData reciever)
        {
            base.OnUpdate(input, reciever);

            if (projectile.instantiatedObject != null) { 
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

        Vector3 GetProjectileOrigin()
        {
            Vector3 spawnPoint = Vector3.zero;
            if (origin == TrajectoryOrigins.CAMERA)
                if (offsetSpace == Space.Self)
                {
                    spawnPoint = Camera.main.transform.position;
                    spawnPoint += Camera.main.transform.forward.normalized * originOffset.z;
                    spawnPoint += Camera.main.transform.up.normalized * originOffset.y;
                    spawnPoint += Camera.main.transform.right.normalized * originOffset.x;
                }
                else
                {
                    spawnPoint = Camera.main.transform.position + originOffset;
                }
            else if (origin == TrajectoryOrigins.SENDER_ORIGIN)
                if (offsetSpace == Space.Self)
                {
                    spawnPoint = sender.transform.position;
                    spawnPoint += sender.transform.forward.normalized * originOffset.z;
                    spawnPoint += sender.transform.up.normalized * originOffset.y;
                    spawnPoint += sender.transform.right.normalized * originOffset.x;
                }
                else
                {
                    spawnPoint = sender.transform.position + originOffset;
                }
            
            return spawnPoint;
        }

    }
}
