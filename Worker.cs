using UnityEngine;
using System.Collections.Generic;

public class Worker : MonoBehaviour {

	public int tileX;
	public int tileY;
	// kertoo millä pisteellä kyseinen työntekijä on töissä
	public int WorkSlotId;
	// työntekijän Id
	public int WorkerId;
	// prosentuaalinen kerroin kuinka tehokkaasti tämä työntekijä käyttää kyseistä työpistettä
	public float WorkSpeed = 5;
}
