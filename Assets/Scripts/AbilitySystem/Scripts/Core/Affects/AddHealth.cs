using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;


namespace AbilitySystem.Affects {
    [CreateAssetMenu(menuName ="Abilities/Affects/Add health")]

    public class AddHealth : ObjectAffect {

        /// <summary>
        /// This is an MetaData affliction, that adds an amount 
        /// of health over time.
        /// </summary>


        public float amount;

        float healthPerSecond;

        public override void AtStart(ObjectMetaData objectData)
        {
            base.AtStart(objectData);

            if (lifeTime == 0 && !oneTime)
            {
                Debug.Log("<color=olive>Warning ! Lifetime too short</color>\n"+
                    "Add some lifetime to health over time or remove oneTime");
            }


            if (oneTime)
            {
                objectData.GiveHealth(amount);
                healthPerSecond = 0;
            } else { 

                healthPerSecond = amount / lifeTime;
            }
        }

        public override void AtUpdate(ObjectMetaData objectData)
        {
            base.AtUpdate(objectData);

            objectData.GiveHealth(healthPerSecond * Time.deltaTime);

        }

    }
}
