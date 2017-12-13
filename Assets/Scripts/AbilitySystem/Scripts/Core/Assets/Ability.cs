using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem { 
    [CreateAssetMenu(menuName = "Abilities/New Ability")]
    public class Ability : ScriptableObject {

        /// <summary>
        /// The 'Ability' class is the master asset that will contain all the ability does.
        /// It is also a container class, containing 'Effect' script assets.
        /// 
        /// It is in charge of sending input to the 'Effect' scripts which in turn
        /// sends the function further down the chain.
        /// Ability -> Effect[] -> EffectScript[]
        /// </summary>

        [Header("Meta Data")]

        public string abilityName;
        public string abilityDescription;
        public Sprite abilityIcon;


        [Header("Settings")]

        [Tooltip("A sound that will play upon pressing the button")]
        public AudioClip activateSound;
        public float activateVolume = 1f;

        [Tooltip("A sound that will play upon pressing the button, if the ability is on cooldown")]
        public AudioClip activateCooldownSound;
        public float activateCooldownVolume = 1f;

        [Space(12)]

        [Tooltip("Think anti-spam cooldown\n"+
            "Use to prevent audio or animation spam")]
        public float baseCooldown = 0f;

        [Tooltip("The time it will take before an ability channel is triggered")]
        public float windupTime;

        [Tooltip("The time it will take before the ability channel finished\n"+
            "Channel time will be performed after windup")]
        public float channelTime;


        [Header("Effects to play")]

        [Tooltip("Which effects should this ability have?")]
        public Effect[] effects;

        [Tooltip("Should we check all scripts and let them determine cooldown?\nThis will render the baseCooldown obsolete")]
        public bool scriptCooldown;

        [Tooltip("Should we ignore the ability cooldown altogether and use the scripts to determine cooldown?\n"+
            "This will render both the baseCooldown and scriptCooldown variables obsolete")]
        public bool forceScriptCooldown;

        // States
        bool isWinding;

        bool isChanneling;


        // Timers
        float windup;

        float channel;

        float cooldown;


        MetaData.PlayerMetaData sender;


        public void OnStart(MetaData.PlayerMetaData senderParam)
        {
            // Reset states
            isChanneling = false;
            isWinding = false;
            
            // Reset timers
            channel = 0;
            windup = 0;
            cooldown = 0;

            // Check if we are using scriptCooldown
            if (scriptCooldown) {
                // if so we will render the baseCooldown obsolete,
                baseCooldown = 0;
                // and loop through the effects to find the effect, 
                foreach (var effect in effects) { 
                    // that has the highest cooldown and use that as the ability cooldown
                    if (effect != null && effect.stackTimer > baseCooldown)
                        baseCooldown = effect.stackTimer;
                    else if (effect == null)
                        Debug.Log("<color=red>Error ! Effect is null.</color>\n"+
                                            "Please add effect to ability.");
                }
            }

            // error check and pass OnStart method on
            if (senderParam == null)
            {
                Debug.Log("<color=red>Error ! No sender of type 'ObjectMetaData' was found</color>\n"+
                                    "You must pass an ability sender at start!");
            } else { 
                sender = senderParam;
                foreach (var effect in effects)
                {
                    if (effect != null)
                        effect.OnStart(sender);
                    else
                        Debug.Log("<color=red>Error ! Effect is null.</color>\n" +
                                            "Please add effect to slot in ability.");
                }
            }
        }

        public void OnUpdate(float input, MetaData.ObjectMetaData reciever)
        {
            if (!forceScriptCooldown) { 
                cooldown -= Time.deltaTime;
            } else
            {
                float newCool = 0;
                foreach (Effect item in effects)
                {
                    if (newCool < item.Cooldown())
                    {
                        newCool = item.Cooldown();
                    }
                }
                cooldown = newCool;
            }

            foreach (Effect effect in effects)
            {
                if (effect != null)
                    effect.OnUpdate(input, reciever);
                else
                    Debug.Log("<color=red>Error ! Effect is null.</color>\n" +
                                        "Please add effect to slot in ability.");
            }
                if (isWinding)
                WindUpLogic(reciever);
            

            if (isChanneling)
                ChannelLogic(reciever);
        
        }

        // For inputs
        public void OnFireEnter(float input, MetaData.ObjectMetaData reciever)
        {
            if (cooldown < 0)
                ActivateEvent(input, reciever);
            else
                ActivateEvent(input, reciever);
        }

        public void OnFireStay(float input, MetaData.ObjectMetaData reciever)
        {
            foreach (Effect effect in effects)
                effect.OnFireStay(input, reciever);
        }

        public void OnFireExit(float input, MetaData.ObjectMetaData reciever)
        {
            foreach (Effect effect in effects)
                effect.OnFireExit(input, reciever);
        }


        public float Cooldown()
        {
            // We return 0 if the cooldown is less than that, because
            // the cooldown timer doesnt care whether or not its on cooldown
            if (cooldown > 0) 
                return cooldown;
            
            return 0;
        }

        public bool OnCooldown()
        {
            foreach (Effect effect in effects)
                if (effect != null) { 
                    if (effect.IsReady()) { 
                    return false;
                    }
                }
                else
                    Debug.Log("<color=red>Error ! Effect is null.</color>\n" +
                                        "Please add effect to slot in ability.");

            if (cooldown > 0)
                return true;

            return false;
        }


        void WindUp(MetaData.ObjectMetaData reciever)
        {
            foreach (Effect effect in effects)
                effect.WindUpLogic(reciever);
        }

        void Channel(MetaData.ObjectMetaData reciever)
        {
            foreach (Effect effect in effects)
                effect.ChannelLogic(reciever);

        }

        void Trigger(MetaData.ObjectMetaData reciever)
        {
            foreach (Effect effect in effects)
                effect.TriggerLogic(reciever);
        }


        // Particular events in this ability script

        void OnCooldownEvent(float input, MetaData.ObjectMetaData reciever)
        {
            if (activateCooldownSound != null)
                AudioSource.PlayClipAtPoint(activateCooldownSound, sender.transform.position, activateCooldownVolume);
        }

        void ActivateEvent(float input, MetaData.ObjectMetaData reciever)
        {
            // reset cooldown
            cooldown = baseCooldown;

            // play sound
            if (activateSound != null)
                AudioSource.PlayClipAtPoint(activateSound , 
                    sender.transform.position , activateVolume);

            // play effects
            foreach (Effect effect in effects)
                effect.OnFireEnter(input, reciever);

            // start windup
            windup = windupTime;
            isWinding = true;
            WindUpLogic(reciever);
        }


        // State logics

        void WindUpLogic(MetaData.ObjectMetaData reciever)
        {
            if (!isChanneling)  
                if (windupTime == 0 && channelTime == 0)
                {
                    isWinding = false;
                    // skip channel step in order to save us from a bit of input lag.
                    WindUp(reciever);
                    Channel(reciever);
                    Trigger(reciever);

                }
                else
                {
                    if (windup > 0)
                    {
                        windup -= Time.deltaTime;
                        WindUp(reciever);
                        sender.AbilityReady(false);
                    }
                    else
                    {
                        isWinding = false;
                        isChanneling = true;
                        channel = channelTime;
                    }
                }

        }

        void ChannelLogic(MetaData.ObjectMetaData reciever)
        {

            if (channel > 0)
            {
                channel -= Time.deltaTime;
                Channel(reciever);
                sender.AbilityReady(false);
            }
            else
            {
                isChanneling = false;
                Trigger(reciever);
                sender.AbilityReady(true);
            }
        }

    }
}