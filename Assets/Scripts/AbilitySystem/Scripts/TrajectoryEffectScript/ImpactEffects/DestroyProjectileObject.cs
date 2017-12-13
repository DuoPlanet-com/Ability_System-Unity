using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.ImpactEffect
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Impact Effects/Destroy Projectile Object")]
    public class DestroyProjectileObject : Projectiles.ImpactEffect
    {

        [Header("Destruction settings")]

        [Tooltip("Do you want to disable any renderer there might be?\n"+
            "This will hide the object, but keep the particles")]
        public bool hideOnImpact;

        [Tooltip("Do you want to freeze the object upon impact?\n" +
            "This is useful if you are hiding on impact, since physics will keep being simulated after renderers are disabled")]
        public bool freezeOnImpact;

        public bool disableColliders;

        [Tooltip("After how many seconds should the object be destroyed completely")]
        public float destroyAfter;

        public override void OnCollision(Collider other, PlayerMetaData sender)
        {
            

        }

        public override void OnCollision(Collision collision, PlayerMetaData sender)
        {
            if (projectile == null)
            {
                Debug.Log("<color=red>Error! Projectile was found to be null upon collision</color>");
            }
            else
            {
                if (hideOnImpact)
                {
                    foreach (Transform item in projectile.instantiatedObject.transform)
                    {
                        item.GetComponent<Renderer>().enabled = false;
                    }
                }
                if (freezeOnImpact)
                {
                    projectile.instantiatedObject.GetComponent<Rigidbody>().isKinematic = true;
                }

                if (disableColliders)
                {
                    foreach (Collider item in projectile.instantiatedObject.GetComponents<Collider>())
                    {
                        item.enabled = false;
                    } 
                }

                Destroy(projectile.instantiatedObject, destroyAfter);
            }
        }

    }
}