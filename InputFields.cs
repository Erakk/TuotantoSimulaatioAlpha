using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputFields : MonoBehaviour {

	private ProgramController control;
	private TimeControls time;

	// tapahtumat, joita tapahtuu heti kun tile luodaan
	void Start(){
		// luo polun luodulle "control" muuttujalle
		control = ProgramController.controller;
		time = TimeControls.time;
	}

	// Input filedin function muuttuvan arvon
	public void InputValue(string newText)
	{
		int testerInt;

		// testaa onko syötetty arvo hyväksyttävä numero
		bool testerBool = int.TryParse(newText, out testerInt);

		if (testerBool) {
			// laittaa syötetyn arvon placeholderiin
			control.placeHolderValue = int.Parse(newText);
		}

		ButtonChecker ();
	}

	// Input filedin function "Nimen"
	public void InputName(string newText)
	{
		// laitaa syötetyn teksin ProgramControlleriin talteen.
		control.placeHolderName = newText;

		ButtonChecker ();
	}

	// Input filedin function "id"
	public void InputId(string newText)
	{
		int testerInt;
		Debug.Log (newText);

		// testaa onko syötetty arvo hyväksyttävä numero
		bool testerBool = int.TryParse(newText, out testerInt);

		if (testerBool) {
			// laittaa syötetyn arvon placeholderiin
			control.placeholderId = int.Parse(newText);
		}

		ButtonChecker ();
	}
	public void CustomSpeed(string newText)
	{
		int testerInt;
		Debug.Log (newText);

		// testaa onko syötetty arvo hyväksyttävä numero
		bool testerBool = int.TryParse(newText, out testerInt);

		if (testerBool) {
			// laittaa syötetyn arvon placeholderiin
			time.customSpeedPlaceholder = int.Parse(newText);
		}

		ButtonChecker ();
	}

	public void InputCustomSpeed(string newText)
	{
		int testerInt;
		Debug.Log (newText);

		// testaa onko syötetty arvo hyväksyttävä numero
		bool testerBool = int.TryParse(newText, out testerInt);

		if (testerBool) {
			// laittaa syötetyn arvon placeholderiin
			time.customSpeedPlaceholder = int.Parse(newText);
		}

		ButtonChecker ();
	}

	void ButtonChecker()
	{
		// Tarkistaa onko kaikki arvot hyväksyttäviä, jotta Lisäys nappi muuttuisi aktiivikseksi
		control.EnableWorkerButton ();
		control.EnableMachineButton ();
		control.EnableProductButton ();
		control.EnableStorageutton ();
	}
}
