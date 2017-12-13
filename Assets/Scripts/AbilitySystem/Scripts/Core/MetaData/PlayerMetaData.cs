using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitySystem.MetaData
{
    public sealed class PlayerMetaData : ObjectMetaData
    {
        /// <summary>
        /// The MetaData of a player. Contains logic that would work for a player
        /// 
        /// Currently holds logic for abilities
        /// </summary>

        [Header("PlayerMetaData - Internals")]
        
        [SerializeField]
        bool abilityReady;

        protected override void Start()
        {
            base.Start();
            isInvulnerable = false;
            AbilityReady(true);
        }

        protected override void Update()
        {
            base.Update();
        }


        public bool AbilityReady()
        {
            return abilityReady;
        }

        public void AbilityReady(bool status)
        {
            abilityReady = status;
        }


    }
}