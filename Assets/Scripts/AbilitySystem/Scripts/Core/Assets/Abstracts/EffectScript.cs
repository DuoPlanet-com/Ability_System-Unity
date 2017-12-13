using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem { 
    public abstract class EffectScript : ScriptableObject
    {

        /// <summary>
        /// The 'EffectScript' class is the last stage of an ability.
        /// It is used for any effects, visual or mechanical, which may occur at any point of an abilities lifetime,
        /// including when on cooldown and whether or not any stacks are present.
        /// 
        /// Should always be extended. And should always be simple.
        /// These EffectScripts are combined into one Effect, of which multiple can be combined in an ability.
        /// 
        /// An example EffectScript would be to heal 10 health on the sender,
        /// then you make a new script, which is a visual effect that plays on the sender.
        /// Combine these two scripts to make a healing effect.
        /// </summary>

        [Header("Effectscript meta data")]

        public string effectScriptName;


        // The Effect parent, which holds stack and cooldown information
        protected Effect container;

        protected MetaData.PlayerMetaData sender;


        public virtual void OnStart(MetaData.PlayerMetaData senderParam,Effect containerParam)
        {
            container = containerParam;
            sender = senderParam;
        }

        public virtual void OnUpdate(float input, MetaData.ObjectMetaData reciever)
        {

        }


        public virtual void OnFireEnter(float input, MetaData.ObjectMetaData reciever)
        {

        }

        public virtual void OnFireStay(float input, MetaData.ObjectMetaData reciever)
        {

        }

        public virtual void OnFireExit(float input, MetaData.ObjectMetaData reciever)
        {

        }


        public virtual void OnCooldown(MetaData.ObjectMetaData reciever)
        {

        }

        public virtual void Passive(MetaData.ObjectMetaData reciever)
        {

        }


        public virtual void WindUp(MetaData.ObjectMetaData reciever)
        {

        }

        public virtual void Channel(MetaData.ObjectMetaData reciever)
        {

        }

        public virtual void Trigger(MetaData.ObjectMetaData reciever)
        {

        }


        public abstract string GenerateDescription();

    }
}