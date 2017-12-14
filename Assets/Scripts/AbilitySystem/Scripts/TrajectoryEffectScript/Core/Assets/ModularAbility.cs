using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem
{
    public abstract class ModularAbility : EffectScript
    {

        /// <summary>
        /// This is the main class for the modular projectile script
        /// </summary>


        [Header("Trajectory Settings")]

        [Tooltip("Where should the object be spawned")]
        public TrajectoryOrigins origin = TrajectoryOrigins.CAMERA;


        [Header("Origin")]

        [Tooltip("An offset which is applied to the origin")]
        public Vector3 originOffset = Vector3.forward;

        [Tooltip("In what space should the offset be applied?")]
        public Space offsetSpace = Space.Self;


        [Header("Direction")]

        [Tooltip("The direction to fire in")]
        public Vector3 direction = Vector3.forward;

        [Tooltip("In what space should this direction be?")]
        public Space directionSpace = Space.Self;

        public Projectiles.ImpactEffect[] impactEffects;


        public enum TrajectoryOrigins
        {
            CAMERA, SENDER_ORIGIN
        }


        public override void OnUpdate(float input, ObjectMetaData reciever)
        {
            base.OnUpdate(input, reciever);
        }

        public override void Trigger(ObjectMetaData reciever)
        {
            base.Trigger(reciever);
        }

        protected Vector3 GetProjectileOrigin()
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
