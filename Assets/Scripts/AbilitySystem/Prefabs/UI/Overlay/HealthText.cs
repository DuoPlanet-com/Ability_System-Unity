using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public sealed class HealthText : MonoBehaviour
{

    public AbilitySystem.MetaData.ObjectMetaData healthToDisplay;

    Text text;

    private void Start()
    {
        if (GetComponent<Text>() != null)
            text = GetComponent<Text>();
        else
            print("<color=red>Error ! Text for health could not be found</color>\n" +
                "Please make sure HealthText is attatched to an object with the Text component");
    }

    private void Update()
    {
        if (text != null)
        {
            text.text = healthToDisplay.Health(0).ToString();
        }
    }
}