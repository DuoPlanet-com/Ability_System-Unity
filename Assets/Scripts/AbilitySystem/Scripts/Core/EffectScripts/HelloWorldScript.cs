using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.EffectScripts {

    [CreateAssetMenu (menuName = "Abilities/EffectScripts/Hello World Script")]
    public class HelloWorldScript : EffectScript {

        /// <summary>
        /// The Hello world script for testing effect scripts.
        /// 
        /// Good for beginners and for debugging and testing
        /// </summary>


        public override void OnStart(MetaData.PlayerMetaData senderParam, Effect containerParam)
        {
            base.OnStart(senderParam, containerParam);
            debuglog("OnStart","One time, on Start()");
        }
        public override void OnFireEnter(float input, MetaData.ObjectMetaData reciever)
        {
            base.OnFireEnter(input, reciever);
            debuglog("FireEnter","The exact moment the ability is pressed");
        }

        public override void OnFireStay(float input, MetaData.ObjectMetaData reciever)
        {
            base.OnFireStay(input, reciever);
            debuglog("FireStay","The continueous push of the ability button");
        }

        public override void OnFireExit(float input, MetaData.ObjectMetaData reciever)
        {
            base.OnFireExit(input, reciever);
            debuglog("FireExit","The release of the ability button");
        }

        public override void OnUpdate(float input, MetaData.ObjectMetaData reciever)
        {
            base.OnUpdate(input, reciever);
            debuglog("OnUpdate","Runs regardless of cooldowns and stack counts");
        }

        public override void Trigger(MetaData.ObjectMetaData reciever)
        {
            base.Trigger(reciever);
            debuglog("Ability triggered","This will only happen once, after the ability has wound up and channeled");
        }

        public override void Passive(MetaData.ObjectMetaData reciever)
        {
            base.Passive(reciever);
            debuglog("Passive", "Runs when we have at least 1 stack, ignoring base cooldown");
        }

        public override void OnCooldown(MetaData.ObjectMetaData reciever)
        {
            base.OnCooldown(reciever);
            debuglog("On cooldown", "Runs when we have no more stacks and are waiting for more");
        }

        public override void Channel(MetaData.ObjectMetaData reciever)
        {
            base.Channel(reciever);
            debuglog("Channel", "Every frame the ability is channeling (Before trigger)");
        }

        public override void WindUp(MetaData.ObjectMetaData reciever)
        {
            base.WindUp(reciever);
            debuglog("WindUp", "Every frame the ability is winding up (Before channeling)");
        }

        public override string GenerateDescription()
        {
            return "Hello world script.";
        }

        void debuglog(string stri)
        {
            Debug.Log("<color=blue>" + stri + "</color>");
        }

        void debuglog(string stri, string stri2)
        {
            Debug.Log("<color=blue>" + stri + "</color>\n" + stri2);
        }
    }
}
