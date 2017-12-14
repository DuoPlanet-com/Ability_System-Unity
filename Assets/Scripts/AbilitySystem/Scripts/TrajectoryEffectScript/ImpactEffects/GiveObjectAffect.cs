using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.ImpactEffects
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Impact Effects/Give ObjectAffect")]
    public class GiveObjectAffect : Projectiles.ImpactEffect
    {

        [Header("Afflictions to give")]

        public ObjectAffect[] affectsToSender;

        public ObjectAffect[] affectsToHit;


        public override void OnCollision(Collision collision, MetaData.PlayerMetaData sender)
        {

          
            foreach (ObjectAffect item in affectsToSender)
            {
                sender.AddAffect(item);

            }

            if (collision.gameObject.GetComponent<MetaData.ObjectMetaData>() != null)
            {
                foreach (ObjectAffect item in affectsToHit)
                {
                    collision.gameObject.GetComponent<MetaData.ObjectMetaData>().AddAffect(item);
                }
            }
           
        }

        public override void OnCollision(Collider other, MetaData.PlayerMetaData sender)
        {
            foreach (var item in affectsToSender)
            {
                sender.AddAffect(item);

            }

            if (other.gameObject.GetComponent<MetaData.ObjectMetaData>() != null)
            {
                foreach (var item in affectsToHit)
                {
                    other.gameObject.GetComponent<MetaData.ObjectMetaData>().AddAffect(item);
                }
            }
        }

    }
}