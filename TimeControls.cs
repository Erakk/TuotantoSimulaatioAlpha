using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeControls : MonoBehaviour {
	
	public static TimeControls time;
	// shortcut controlleriin
	private ProgramController control;

	// UI teksti joka muuttuu valitun nopeuden mukaan
	Text speedText;

	// CustomSpeedin Placeholder
	public int customSpeedPlaceholder = 5;

	void Awake()
	{
		time = this;
	}

	// Tekee halutut alku säädöt ohjelman alkaessa ( tässä tapauksessa täyttää variablet)
	void Start()
	{
		control = ProgramController.controller;
		speedText = GameObject.Find ("SelectedSpeedText").GetComponent<Text>();
	}
		
	// nopeus vaikuttaa aikaan kuinka kauan koneet työstävät tuotteita

	// Simulaation pysäytysnappi ominaisuudet
	public void StopTime()
	{
		speedText.text = "Selected speed: 0";
		control.programSpeed = 0;
	}
	// Simulaation normaalinopeus ominaisuudet
	public void NormalSpeed()
	{
		speedText.text = "Selected speed: 1";
		control.programSpeed = 1;
	}
	// Simulaation nopea vauhti ominaisuudet
	public void HighSpeed()
	{
		speedText.text = "Selected speed: 10";
		control.programSpeed = 10;

	}
	//itse määritetty vauhti simulaatioon
	public void CustomSpeed()
	{
		speedText.text = "Selected speed: " + customSpeedPlaceholder;
		control.programSpeed = customSpeedPlaceholder;

	}
}
