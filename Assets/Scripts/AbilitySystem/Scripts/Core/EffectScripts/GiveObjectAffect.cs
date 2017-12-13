using System;
using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;

namespace AbilitySystem.EffectScripts
{

    [CreateAssetMenu(menuName = "Abilities/EffectScripts/Give Affliction")]
    public class GiveObjectAffect : EffectScript
    {

        /// <summary>
        /// 
        /// </summary>

        [Header("GiveObjectAffect settings")]

        public ObjectAffect[] toReciever;

        public ObjectAffect[] toSender;


        public override void Trigger(ObjectMetaData reciever)
        {
            base.Trigger(reciever);


            foreach (ObjectAffect item in toReciever)
            {
                reciever.AddAffect(item);
            }

            foreach (ObjectAffect item in toSender)
            {
                sender.AddAffect(item);
            }

        }

        public override string GenerateDescription()
        {
            throw new NotImplementedException();
        }

    }
}
