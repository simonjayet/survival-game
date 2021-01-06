using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hungerbar : MonoBehaviour
{

	public Slider slider;

	public void SetMaxHunger(int health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

    public void SetHunger(int health)
	{
		slider.value = health;
	}

}
