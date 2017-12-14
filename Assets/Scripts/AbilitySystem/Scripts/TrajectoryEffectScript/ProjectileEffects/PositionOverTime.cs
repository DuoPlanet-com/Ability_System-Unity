using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem.TrajectorySystem.ProjectileEffects
{

    [CreateAssetMenu(menuName = "Abilities/Trajectory System/Projectile Effects/Position Over Time")]
    public class PositionOverTIme : Projectiles.ProjectileEffect
    {

        public AnimationCurve curve1;

        public AnimationCurve curve2;

        public AnimationCurve curve3;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}