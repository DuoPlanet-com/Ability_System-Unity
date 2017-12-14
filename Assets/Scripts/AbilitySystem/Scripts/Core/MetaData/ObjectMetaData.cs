using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.MetaData {
    public class ObjectMetaData : MonoBehaviour {

        /// <summary>
        /// 'ObjectMetaData' is any meta data any object could or should have.
        /// Used for damage, buffs and ability resource management.
        /// 
        /// This should be and is intended to be extended see PlayerMetaData for
        /// examplary use.
        ///</summary>


        [Header("ObjectMetaData Settings")]

        public float maxHealth = 100;

        public bool canOverHeal;

        [Tooltip("This is used for mana, energy ect.. This script does not use resource.\n"+
            "Extend this script if you want to use resources")]
        public float maxResource = 100;


        protected float originalMaxHealth;

        protected float originalMaxResource;


        [Header("ObjectMetaData Internals")]

        [SerializeField]
        protected float health;

        [SerializeField]
        protected float resource;


        [SerializeField]
        protected List<ObjectAffect> affects = new List<ObjectAffect>();

        [SerializeField]
        protected bool isInvulnerable;

        [SerializeField]
        protected bool isTargetable;


        protected virtual void Start()
        {
            // GameObject player = GameObject.FindGameObjectWithTag("Player");

            originalMaxHealth = maxHealth;
            originalMaxResource = maxResource;

            health = maxHealth;
            resource = maxResource;

            isInvulnerable = false;
            isTargetable = true;
        }

        protected virtual void Update()
        {
            // Check if it can overheal, if not set to max health upon overheal
            if (!canOverHeal)
                if (health > maxHealth)
                    health = maxHealth;
            
            
            // Loop through each affliction
            foreach (ObjectAffect affect in affects.ToArray())
            {
                // check if their lifetime is over.
                if (!affect.DoneTime()) { 
                    // If not play the afflictions update function
                    affect.OnUpdate(this);
                }
                else
                {
                    // If it has expired, remove it from the array and let it know.
                    affect.AtDestroy(this);
                    affects.Remove(affect);
                }
            }
        }

        
        // Used for adding afflictions to the object.
        public bool AddAffect(ObjectAffect affect)
        {

            if (!affects.Contains(affect))
            {
                affect.DoneTime(false);
                affect.AtStart(this);
                affects.Add(affect);
                return true;
            } else
            {
                if (affect.reappliable)
                {
                    affect.LifeLeft(affect.lifeTime);
                    affects.Remove(affect);
                    affects.Add(affect);
                } else
                {
                    print("<color=blue>Notice! The affect, which you tried to apply, was already present</color>\n" +
                    "You may want to make the affliction reapplicable");
                }
            }

            return false;

        }


        // Getter and setter functions for :
        //  - Resources
        //  - Health
        //  - Vulnerability state 
        //  - Targetability state
        public float Resource()
        {
            return resource;
        }

        public void Resource(float newResource)
        {
            resource = newResource;
        }


        public float Health(int decimals)
        {
            decimal cimal = (decimal)health;
            return (float) decimal.Round(cimal , decimals);
        }

        public void Health(float newHealth)
        {
            health = newHealth;
        }


        public bool Invulnerable()
        {
            return isInvulnerable;
        }

        public void Invulnerable(bool state)
        {
            isInvulnerable = state;
        }


        public bool Targetable()
        {
            return isTargetable;
        }

        public void Targetable(bool state)
        {
            isTargetable = state;
        }


        // These 'givers' could be overridden, so that we may inject code into it. 
        // This could be by modifying the amount for amour
        public virtual float GiveHealth(float amount)
        {
            if (!isInvulnerable)
                health += amount;

            return health;
        }

        public virtual float GiveResource(float amount)
        {
            resource += amount;
            return resource;
        }


        // This is something that should be unique to each MetaData script.
        // Override this
        public bool IsUsingResource()
        {
            return false;
        }

    }
}

