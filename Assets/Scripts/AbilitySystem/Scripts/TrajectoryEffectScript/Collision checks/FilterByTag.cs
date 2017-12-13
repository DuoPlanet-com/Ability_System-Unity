using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbilitySystem.TrajectorySystem.CollisionCheck
{
    [CreateAssetMenu(menuName = "Abilities/Collision Check/Filter By Tag")]
    public class FilterByTag : Projectiles.CollisionCheck
    {

        /// <summary>
        /// This is a CollisionCheck extension, which allows you to
        /// filter out / in objects by tag.
        /// </summary>


        public string[] tagsToFilter;

        public bool exclude = true;


        public override bool Validate(Collision collision)
        {
            if (exclude)
                foreach (string item in tagsToFilter)
                    if (collision.collider.tag == item)
                        return false;

            else
                foreach (string tagTo in tagsToFilter)
                    if (collision.collider.tag == tagTo)
                        return true;

            return false;
        }

        public override bool Validate(Collider other)
        {
            if (exclude)
                foreach (string item in tagsToFilter)
                    if (other.tag == item)
                        return false;

            else
                foreach (string tagTo in tagsToFilter)
                    if (other.tag == tagTo)
                        return true;

            return false;
        }
    }
}