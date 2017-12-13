using System.Collections;
using System.Collections.Generic;
using AbilitySystem.MetaData;
using UnityEngine;

namespace AbilitySystem.Affects
{
    [CreateAssetMenu(menuName = "Abilities/Affects/Targetability")]
    public class Targetability : ObjectAffect
    {

        public bool state = false;

        public override void AtStart(ObjectMetaData objectData)
        {
            objectData.Targetable(state);
            base.AtStart(objectData);
        }

        public override void AtDestroy(ObjectMetaData objectData)
        {
            objectData.Targetable(!state);
            base.AtDestroy(objectData);
        }
    }
}