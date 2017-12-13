using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.Projectiles
{
    public abstract class CollisionCheck : ScriptableObject
    {

        /// <summary>
        /// Handles validations of collisions.
        /// Who to collide or trigger with
        /// </summary>


        public abstract bool Validate(Collision collision);

        public abstract bool Validate(Collider other);


    }
}