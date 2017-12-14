using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.ImpactEffects
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Impact Effects/Play Sound")]
    public class PlaySound : Projectiles.ImpactEffect
    {

        [Header("PlaySound settings")]

        // Sounds to the sender
        public AudioClip[] toSender;

        public float toSenderVolume = 1;


        // Sounds to the impact
        public AudioClip[] toImpact;

        public float toImpactVolume = 1;


        // Sounds to the impact
        public AudioClip[] toHitObject;

        public float toHitObjectVolume = 1;


        public override void OnCollision(Collision collision, PlayerMetaData sender)
        {

            Vector3 playPos = AverageCollisionPosition(collision);
            Vector3 sendPos = sender.transform.position;

            foreach (AudioClip sound in toSender)
            {
                AudioSource.PlayClipAtPoint(sound , sendPos , toSenderVolume);
            }

            foreach (AudioClip sound in toImpact)
            {
                AudioSource.PlayClipAtPoint(sound , playPos , toImpactVolume);
            }

            foreach (AudioClip sound in toHitObject)
            {
                AudioSource.PlayClipAtPoint(sound , sender.transform.position , toHitObjectVolume);
            }

        }

        public override void OnCollision(Collider other, PlayerMetaData sender)
        {

            Vector3 playPos = other.transform.position;
            Vector3 sendPos = sender.transform.position;

            foreach (AudioClip sound in toSender)
            {
                AudioSource.PlayClipAtPoint(sound, sendPos, toSenderVolume);
            }

            foreach (AudioClip sound in toImpact)
            {
                AudioSource.PlayClipAtPoint(sound, playPos, toImpactVolume);
            }

            foreach (AudioClip sound in toHitObject)
            {
                AudioSource.PlayClipAtPoint(sound, sender.transform.position, toHitObjectVolume);
            }

        }

    }
}