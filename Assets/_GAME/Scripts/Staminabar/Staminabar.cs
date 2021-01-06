using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{

	public Slider slider;

	public void SetMaxStamina(int health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

    public void SetStamina(int health)
	{
		slider.value = health;
	}

}
