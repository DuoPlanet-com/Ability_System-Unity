using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;


namespace AbilitySystem.Affects
{
    [CreateAssetMenu(menuName = "Abilities/Affects/Add resources")]

    public class AddResources : ObjectAffect
    {

        /// <summary>
        /// This is an MetaData affliction, that adds an amount of resource
        /// </summary>


        public float amount;

        float resourcePerSecond;

        public override void AtStart(ObjectMetaData objectData)
        {
            base.AtStart(objectData);

            if (lifeTime == 0 && !oneTime)
            {
                Debug.Log("<color=olive>Warning ! Lifetime too short</color>\n" +
                    "Add some lifetime to health over time or remove oneTime");
            }

            if (oneTime)
            {
                objectData.GiveResource(amount);
                resourcePerSecond = 0;
            } else
            {
                resourcePerSecond = amount / lifeTime;
            }


        }

        public override void AtUpdate(ObjectMetaData objectData)
        {
            base.AtUpdate(objectData);

            objectData.GiveResource(resourcePerSecond * Time.deltaTime);

        }

    }
}
