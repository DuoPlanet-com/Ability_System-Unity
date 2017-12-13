using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.Projectiles
{
    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Projectiles/Generic")]
    public class Projectile : ScriptableObject
    {

        /// <summary>
        /// The 'Projectile' class contains all of the logic of the projectile.
        /// It handles the module after the projectile has been fired,
        /// and is fed information through the TrajectoryEffectScript
        /// </summary>


        public GameObject projectilePrefab;

        [HideInInspector]
        public GameObject instantiatedObject;

        private Collider[] objectColliders;

        protected MonoHelper.Projectilable projectileable;

        protected ImpactEffect[] impactEffects;

        protected MetaData.PlayerMetaData sender;


        public virtual void InstantiateObject(Vector3 position, Quaternion rotation, MetaData.PlayerMetaData senderParam, ImpactEffect[] effects)
        {
            foreach (var effect in effects) 
            {
                effect.SetProjectile(this);
            }
            impactEffects = effects;
            sender = senderParam;

            instantiatedObject = Instantiate(projectilePrefab, position, rotation) as GameObject;
            projectileable = instantiatedObject.AddComponent(typeof(MonoHelper.Projectilable)) as MonoHelper.Projectilable;
            projectileable.Init(this, impactEffects,sender);
            OnInstantiate();
        }


        public virtual void OnCollisionEnter(Collision collision)
        {

        }

        public virtual void OnCollisionStay(Collision collision)
        {

        }

        public virtual void OnCollisionExit(Collision collision)
        {

        }


        public virtual void OnTriggerEnter(Collider other)
        {

        }

        public virtual void OnTriggerStay(Collider other)
        {
            Debug.Log("trigstay");

        }

        public virtual void OnTriggerExit(Collider other)
        {
            Debug.Log("trigexit");

        }


        private void OnInstantiate()
        {

            if (instantiatedObject == null)
                Debug.Log("<color=olive>Warning ! Initialising a projectile without projectile object</color>\nThe object must be instantiated");

            bool errCheck = true;

            if (instantiatedObject.GetComponents<Collider>().Length > 0)
            {
                Collider[] colliderList = instantiatedObject.GetComponents<Collider>();
                objectColliders = new Collider[colliderList.Length];

                for (int i = 0; i < colliderList.Length; i++)
                    objectColliders[i] = colliderList[i];

                foreach (Collider objectCollider in objectColliders)
                    if (objectCollider.isTrigger == false)
                        errCheck = false;
            } else
                Debug.Log("<color=red>Error ! A projectile was initiated without any collider</color>\nYou need add a non-triggerable collider to the object, associated with this projectile");

            if (errCheck)
                Debug.Log("<color=red>Error ! A projectile was initiated without solid collider</color>\nYou need add a non-triggerable collider to the object, associated with this projectile");
        }

    }
}
