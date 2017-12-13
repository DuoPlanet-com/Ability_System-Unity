using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.CollisionCheck
{
    [CreateAssetMenu(menuName = "Abilities/Collision Check/Filter By Component")]
    public class FilterByComponents : Projectiles.CollisionCheck
    {

        /// <summary>
        /// This is a CollisionCheck extension, which allows you to
        /// filter out / in objects by component name.
        /// </summary>


        public string[] componentsToFilter;

        public bool exclude = false;


        public override bool Validate(Collision collision)
        {
            foreach (var toFilter in componentsToFilter)
                if (collision.gameObject.GetComponent(toFilter) != null) {
                    return !exclude;
                }

            return false;
        }

        public override bool Validate(Collider other)
        {
            foreach (var toFilter in componentsToFilter)
                if (other.gameObject.GetComponent(toFilter) != null)
                    return !exclude;

            return false;
        }

    }
}
