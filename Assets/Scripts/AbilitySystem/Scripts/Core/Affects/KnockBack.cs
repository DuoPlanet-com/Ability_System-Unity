using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;

namespace AbilitySystem.Affects
{
    [CreateAssetMenu(menuName = "Abilities/Affects/Knockback")]
    public class KnockBack : ObjectAffect
    {

        /// <summary>
        /// This is an affliction, which allows one to knock the object 
        /// in different directions.
        /// </summary>

        [Header("KnockBack Settings")]

        public Space space = Space.Self;
        public Vector3 direction = Vector3.up;
        public float force = 500;

        float originalStickHeight;

        public override void AtDestroy(ObjectMetaData objectData)
        {
            objectData.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().advancedSettings.groundCheckDistance = originalStickHeight;
            base.AtDestroy(objectData);
        }

        public override void AtStart(ObjectMetaData objectData)
        {
            base.AtStart(objectData);

            // record the check distance, and set the check distance to negative
            // in order for
            originalStickHeight = objectData.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().advancedSettings.groundCheckDistance;
            objectData.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().advancedSettings.groundCheckDistance = -10;

            //add force
            if (space == Space.World) { 
                objectData.GetComponent<Rigidbody>().AddForce(direction * force);
            } else
            {
                objectData.GetComponent<Rigidbody>().AddRelativeForce(direction * force);
            }
        }

        public void DirectionFromHit(RaycastHit hit)
        {

            direction = -hit.normal;
            space = Space.World;

        }

    }
}
