using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.ProjectileEffect
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Projectile Effects/Particles")]
    public class GenericProjectileEffect : Projectiles.ProjectileEffect
    {
        /// <summary>
        /// This is a ProjectileEffect that allows one to add
        /// a muzzle flash, as well as an effect that follows the projectile
        /// </summary>


        public GameObject[] toInitializeEffects;

        public GameObject[] toFollowEffects;


        public override void OnInit()
        {
            base.OnInit();

            foreach (GameObject GO in toInitializeEffects)
                Instantiate(GO, projectile.instantiatedObject.transform.position, projectile.instantiatedObject.transform.rotation);

            foreach (GameObject GO in toFollowEffects)
            {
                GameObject effect = Instantiate(GO, projectile.instantiatedObject.transform.position, Quaternion.identity) as GameObject;

                // Add projectileEffectable in order for proper Monobehaviour
                MonoHelper.ProjectileEffectable PEable = effect.AddComponent(typeof(MonoHelper.ProjectileEffectable)) as MonoHelper.ProjectileEffectable;
                PEable.parentTransform = projectile.instantiatedObject.transform;
                PEable.objectToFollow = effect;
                PEable.objectToFollow.transform.rotation = Quaternion.Euler(0, projectile.instantiatedObject.transform.rotation.eulerAngles.y, 0);
            }
        }
    }
}