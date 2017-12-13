using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem { 
    public abstract class ObjectAffect : ScriptableObject {

        /// <summary>
        /// The ObjectAffect are for particular effects that should occur
        /// on the object it is on. They have a special quirk, which is
        /// it cannot be used twice in one script. 
        /// Only one instance can exist. This instance may be tossed around, but may not
        /// exist on more than one object at once. Do not use these sparingly.
        /// 
        /// These afflictions could be damage over time, damage flat,
        /// healing, visual effects. The world's your oyster.
        /// 
        /// These are expected to have short lifetimes and then die.
        /// Should not act as permanent stat upgrades.
        /// </summary>


        [Tooltip("If a character already has this affliction, should the time left be reset?")]
        public bool reapplicible;

        [Tooltip("Should this be played only once?")]
        public bool oneTime = true;

        [Tooltip("How long should this affect last?")]
        public float lifeTime;

        // OneTime trigger
        protected bool doneTime = false;

        // Life Timer
        protected float lifeLeft;


        // Run by ObjectMetaData once added to it.
        public virtual void AtStart(MetaData.ObjectMetaData objectData)
        {
            lifeLeft = lifeTime;
        }

        public virtual void AtUpdate(MetaData.ObjectMetaData objectData)
        {
            lifeLeft -= Time.deltaTime;
            doneTime = true;
        }

        public virtual void AtDestroy(MetaData.ObjectMetaData objectData)
        {

        }

        // Handled by the MetaData
        public void OnUpdate(MetaData.ObjectMetaData objectData)
        {
            if (!oneTime)
                AtUpdate(objectData);
            else
                if (!doneTime)
                    AtUpdate(objectData);
        }


        // Getters and setters

        public void LifeLeft(float amount)
        {
            lifeLeft = amount;
        }

        public float LifeLeft()
        {
            return lifeLeft;
        }


        public void DoneTime(bool status)
        {
            doneTime = status;
        }

        public bool DoneTime()
        {
            bool oneTimeCheck = (oneTime && doneTime);

            bool lifeTimeCheck = (lifeLeft <= 0 && lifeTime > 0);


            return !( !oneTimeCheck && !lifeTimeCheck );
        }

    }
}