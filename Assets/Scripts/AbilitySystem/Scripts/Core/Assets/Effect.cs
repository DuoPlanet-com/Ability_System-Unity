using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem { 
    [CreateAssetMenu(menuName = "Abilities/New Effect")]
    public class Effect : ScriptableObject {

        /// <summary>
        /// The 'Effect' class is a container class which handles stacks and cooldowns of stacks.
        /// If one or more stacks are available, this effect considers itself off cooldown and ready to use.
        /// 
        /// This class contains EffectScript classes which handles specific Effects
        /// A visual EffectScript and a mechanical EffectScript together combines Effects.
        /// If this effect has one or more stacks those EffectScripts are also triggering a Passive() function.
        /// </summary>

        [Header("Meta data")]

        public string effectName;

        public string effectDescription;


        [Header("Settings")]
    
        [Tooltip("How many charges of this ability can be stored at once?")]
        public int maxStacks = 0;

        [Tooltip("How often should this effect recieve a stack")]
        public float stackTimer = 1;

        [Tooltip("When should a stack pop?\n" +
            "OnTrigger theoretically allows for one to cancel an ability before it triggered\n\n " + 

            "While OnWind, would force one to commit to the attack (Or interrupt and lose mana)")]
        public StackCommitment stackCommitment = StackCommitment.ONTRIGGER;


        [Header("Scripts")]

        [Tooltip("The effect scripts to play on this effect\n"+
            "This could be a visual effect, along with mechanical effect")]
        public EffectScript[] effectScripts;


        int stacks;

        float cooldown;


        // for state handling
        bool isChanneling;

        bool isWinding;

        bool oneTaken = false;


        MetaData.ObjectMetaData sender;


        public enum StackCommitment
        {
            ONWIND, ONCHANNEL, ONTRIGGER, NEVER
        }

        public void OnStart(MetaData.PlayerMetaData senderParam)
        {
            // loop throug every EffectScript
            foreach (EffectScript effectScript in effectScripts)
            {
                // Trigger OnStart and send 'sender' parameter and this container along
                // This is necessary for future use, such as 
                // playing visual effects on our character
                effectScript.OnStart(senderParam, this);
            }
            // set our own sender variable
            sender = senderParam;
            // Set cooldown to 0 for easier testing
            cooldown = 0;
        }

        public void OnUpdate(float input, MetaData.ObjectMetaData reciever)
        {

            RunTimer();

            foreach (EffectScript effectScript in effectScripts)
                effectScript.OnUpdate(input, reciever);

            // passive logic checks whether or not we have stacks and act accordingly
            PassiveLogic(reciever);

        }


        // For button inputs exclusively
        public void OnFireEnter(float input, MetaData.ObjectMetaData reciever)
        {
            foreach (EffectScript effectScript in effectScripts)
                effectScript.OnFireEnter(input, reciever);
        }

        public void OnFireStay(float input, MetaData.ObjectMetaData reciever)
        {
            foreach (EffectScript effectScript in effectScripts)
                effectScript.OnFireStay(input, reciever);
        }

        public void OnFireExit(float input, MetaData.ObjectMetaData reciever)
        {
            foreach (EffectScript effectScript in effectScripts)
                effectScript.OnFireExit(input, reciever);
        }


        // Different ability state logics
        // These are operated by the Ability script

        public void WindUpLogic(MetaData.ObjectMetaData reciever)
        {
            oneTaken = false;
            if (IsReady() || isWinding)
            {
                foreach (EffectScript effectScript in effectScripts)
                    effectScript.WindUp(reciever);
                
                if (stackCommitment == StackCommitment.ONWIND && !isWinding) 
                    UseStacks(1);
                
                isWinding = true;
            }
        }

        public void ChannelLogic(MetaData.ObjectMetaData reciever)
        {
            if (IsReady() || isChanneling || isWinding)
            {
                foreach (EffectScript effectScript in effectScripts)
                    effectScript.Channel(reciever);


                bool commitmentCheck = // Check whether or not to commit stack now.
                    (stackCommitment == StackCommitment.ONCHANNEL
                    || stackCommitment == StackCommitment.ONWIND);

                if ((commitmentCheck && !isChanneling) 
                        && !oneTaken) {
                    UseStacks(1);
                }


                isChanneling = true;
                isWinding = false;
            }
        }

        public void TriggerLogic(MetaData.ObjectMetaData reciever)
        {
            if (IsReady() || isChanneling)
            {
                foreach (EffectScript effectScript in effectScripts)
                    effectScript.Trigger(reciever);

                bool commitmentCheck = // Check whether or not to commit stack now.
                    stackCommitment == StackCommitment.ONTRIGGER 
                    || stackCommitment == StackCommitment.ONCHANNEL 
                    || stackCommitment == StackCommitment.ONWIND;

                if (commitmentCheck && !oneTaken) 
                    UseStacks(1);

                isChanneling = false;
                isWinding = false;
                oneTaken = false;
            }
        }


        // Passive is handled by this Effect script.
        private void PassiveLogic(MetaData.ObjectMetaData reciever)
        {
            if (stacks > 0)
            {
                foreach (var effectScript in effectScripts)
                    effectScript.Passive(reciever);
            }
            else
            {
                foreach (var effect in effectScripts)
                    effect.OnCooldown(reciever);
            }
        }

        private void RunTimer()
        {
            // Run the stacktimer
            if (stacks < maxStacks)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                cooldown = stackTimer;
            }

            // If the stacktimer is out, give our effect a stack
            if (cooldown <= 0)
            {
                if (stacks < maxStacks)
                {
                    UseStacks(-1);
                    cooldown = stackTimer;
                }
            }
        }


        // Getters and setters 
        public virtual bool IsReady()
        {
            if (stacks > 0)
                return true;

            return false;
        }


        public int Stacks()
        {
            return stacks;
        }

        public void Stacks(int amount)
        {
            stacks = amount;
        }

        public void UseStacks(int amount)
        {
            oneTaken = true;
            stacks -= amount;
        }


        public void Cooldown(float newCooldown)
        {
            cooldown = newCooldown;
        }

        public float Cooldown()
        {
            if (cooldown == stackTimer)
            {
                return 0;
            }
            return cooldown;
        }

    }
}