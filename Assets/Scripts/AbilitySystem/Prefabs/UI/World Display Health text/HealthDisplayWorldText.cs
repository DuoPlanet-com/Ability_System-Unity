using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayWorldText : MonoBehaviour {

    AbilitySystem.MetaData.ObjectMetaData metaData;

    Text healthText;

    private void Start()
    {
        healthText = GetComponent<Text>();
        if (gameObject.GetComponentInParent<AbilitySystem.MetaData.ObjectMetaData>() != null)
        {
            metaData = gameObject.GetComponentInParent<AbilitySystem.MetaData.ObjectMetaData>();
        }
    }

    private void Update()
    {
        healthText.text = metaData.Health(0).ToString();
    }

}
