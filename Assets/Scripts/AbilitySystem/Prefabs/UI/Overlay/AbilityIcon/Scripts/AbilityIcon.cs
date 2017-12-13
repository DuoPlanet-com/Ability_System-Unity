using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour {

    public AbilitySystem.Ability ability;

    public Text cdText;

    public string axis;

    bool beenPressed = false;

    float abilityInput;

    AbilitySystem.MetaData.PlayerMetaData sender;

    private void Start()
    {
        sender = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.MetaData.PlayerMetaData>();
        ability.OnStart(sender);
    }

    private void Update()
    {

        float result = ability.Cooldown();

        if (result > 1)
        {
            result = Mathf.Floor(result);
        }

        cdText.text = result.ToString();

        abilityInput = Input.GetAxis(axis);

        // add target instead of null
        ability.OnUpdate(abilityInput, GetTarget());

        CheckInput();
        
    }

    private AbilitySystem.MetaData.ObjectMetaData GetTarget()
    {
        return null;
    }

    private void CheckInput()
    {

        if (beenPressed)
        {
            if (Input.GetButtonUp(axis))
            {
                ability.OnFireExit(abilityInput, GetTarget());
                beenPressed = false;
            }
        }

        if (!ability.OnCooldown())
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1);
            if (Input.GetButtonDown(axis))
            {
                beenPressed = true;
                if (sender.AbilityReady()) { 
                    ability.OnFireEnter(abilityInput, GetTarget());
                }
            }

            if (Input.GetButton(axis))
            {
                  ability.OnFireStay(abilityInput, GetTarget());
            }

        }
        else
        {

            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, .5f);
        }
    }


}
