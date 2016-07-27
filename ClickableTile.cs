using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableTile : MonoBehaviour {
/*
* 	Myöhemmin tässä scriptissä käytetään kyseisiä numeroita merkkaamaan kyseisiä objecteja
*	1. Machine
*	2. Worker
*	3. Storage
*	4. Product
*/

	// public variablet tilen x ja y koridinaateille
	// kertoo missä kohtaa karttaa juuri tämä tile sijaitsee
	// määritellää Tilemapissa kartanluonti vaiheessa
	public int tileX;
	public int tileY;

	private bool inUse;

	// pääsy "ProgramControlleriin"
	private ProgramController control;

	// tapahtumat, joita tapahtuu heti kun tile luodaan
	void Start(){
		// luo polun luodulle "control" muuttujalle
		control = ProgramController.controller;
	}

	// tapahtumat, joita tapahtuu, kun tätä tilee painetaan
	void OnMouseUp ()
	{
		
		// Tarkistaa onko hiiri mahdollisesti UI elementin päällä
		// mikäli on, tämä estää tuotteiden lisäämisen kartalle
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		// ensimmäinen if varmistaa, että jokin ProgramController scirptin objecti on valittu
		if (control.SelectedObject != 0 && !inUse) {

			// luodaan "switch" katsomaan minkä objectin kanssa ollaan tekemisissä
			switch (control.SelectedObject) {
			case 1:
				// Hakee ProgramControllersta koneen, johon tehty halutut säädöt
				control.InstantiateMachine (tileX, tileY);
				break;
			case 2:
				// Hakee ProgramControllersta työntekijän, johon tehty halutut säädöt
				control.InstantiateWorker (tileX, tileY);
				break;
			case 3:
				// Hakee ProgramControllersta varaston, johon tehty halutut säädöt
				control.InstantiateStorage (tileX, tileY);
				break;
			case 4:
				// Hakee ProgramControllersta tuotteen, johon tehty halutut säädöt
				control.InstantiateProduct (tileX, tileY);
				break;
			default:
				break;
			}
		}
		// merkitsee tämän "tilen" käytetyksi
		inUse = true;

		PlaceholderReset ();
		control.MachinesAdded ();
	}

	// Palautetaan placeholderit lähtöarvoihin
	// ja estää uuden tuotteen lisäämisen heti perään
	void PlaceholderReset()
	{
		control.placeHolderSlot = 0;
		control.SelectedObject = 0;
		control.placeHolderValue = 0;
		control.placeholderId = 0;
		control.placeHolderName = "0";

	}
}
