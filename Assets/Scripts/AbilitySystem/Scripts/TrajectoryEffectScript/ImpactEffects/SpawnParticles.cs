using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.ImpactEffect
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Impact Effects/Spawn Particles")]
    public class SpawnParticles : Projectiles.ImpactEffect
    {

        /// <summary>
        /// This is an impact effect, which spawns a gameobject.
        /// 
        /// Its settings can be modified to achieve different rotations and positions
        /// 
        /// Todo: rotation offset
        /// </summary>


        [Header("SpawnParticles Settings")]

        public GameObject[] toSpawn;

        public Vector3 offset;

        public OrientationMethod orientation;


        public enum OrientationMethod
        {
            IDENTITY , NORMAL , FORWARD
        }


        public override void OnCollision(Collision collision, PlayerMetaData sender)
        {
            Spawn(collision);
        }

        public override void OnCollision(Collider other, PlayerMetaData sender)
        {
            Spawn(other);
        }

        void Spawn(Collision collision)
        {
            Vector3 averagePos = Vector3.zero;
            Vector3 averageNorm = Vector3.zero;

            foreach (ContactPoint point in collision.contacts)
            {
                Vector3 pos = point.point;
                Vector3 rot = point.normal;
                averagePos += pos;
                averageNorm += rot;

            }


            averageNorm = averageNorm / collision.contacts.Length;
            averagePos = averagePos / collision.contacts.Length;

            Vector3 spawnPosition = new Vector3(
                averagePos.x + offset.x,
                averagePos.y + offset.y,
                averagePos.z + offset.z);

            SpawnAt(spawnPosition, averageNorm);

        }

        void Spawn(Collider other)
        {
            Vector3 position = new Vector3(
                other.transform.position.x + offset.x,
                other.transform.position.y + offset.y,
                other.transform.position.z + offset.z);

            Vector3 rotation = Vector3.zero;

            if (orientation == OrientationMethod.FORWARD) { 
                rotation = other.transform.forward;
            } else if (orientation == OrientationMethod.NORMAL)
            {
                Debug.Log("<color=olive>Warning! Incompatible orientation method using isTrigger:" + other.isTrigger.ToString() + "</color>\n" +
                    "Please make sure either the orientation method is not set to NORMAL or remove isTrigger from the collider");
            }

            SpawnAt(position, rotation);

        }
            
        void SpawnAt(Vector3 position, Vector3 eulerRotation)
        {

            Quaternion rotation = Quaternion.Euler(eulerRotation);

            if (orientation == OrientationMethod.IDENTITY) {
                foreach (GameObject go in toSpawn)
                {
                    Instantiate(go, position,Quaternion.identity);

                }
            } else if (orientation == OrientationMethod.NORMAL)
            {
                foreach (GameObject go in toSpawn)
                {
                    Instantiate(go, position, rotation);
                }
            } else
            {
                foreach (GameObject go in toSpawn)
                {
                    Instantiate(go, position, rotation);
                }
            }
        }

    }

}