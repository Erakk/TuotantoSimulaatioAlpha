using UnityEngine;
using System.Collections.Generic;
//using UnityEngine.UI;
//using System.Text;
using System.IO;

public class FileReader : MonoBehaviour {


	private ProgramController control;

	// tapahtumat, joita tapahtuu heti kun tile luodaan
	void Start(){
		// luo polun luodulle "control" muuttujalle
		control = ProgramController.controller;
	}
	/*
	*	0. objectin nim (machine, storage....)
	*	1. objectin x akselin kohta
	*	2. objectin y akselin kohta
	*	- loput saattaa muuttua objectetittain, 
	* 
	*/
	public void ReadFromFile()
	{;
		// hakee tiedoston tietokoneelta variableen
		StreamReader file = new StreamReader (@"C:\Users\i7-6700K\Desktop\Projektit\simteksti.txt");

		// kirjoittaa rivin tiedostosta stringiin
		string text = file.ReadLine();

		// looppia läpi niin kauan kun rivejä riittää
		while (text != null) 
		{
			// merkki, joka jakaa rivin tiedot toisistaan
			char[] delimiter = {':'};
			// jakaa tiedot string arrayin osiin
			string[] fields = text.Split(delimiter);

			// katsoo toimintamallin arrayn ensimmäisen arvon perusteella
			switch (fields[0]) {
			case "Worker":
				/*
				 *  3. workslot
				 * 	4. workspeed
				 * 	5. workerid
				*/
				// tallentaa tiedot arraystä placeholdereihin ja luo objectin
				control.placeHolderSlot = int.Parse(fields[3]);
				control.placeHolderValue = int.Parse(fields[4]);
				control.placeholderId = int.Parse(fields[5]);

				control.InstantiateWorker (int.Parse(fields[1]), int.Parse(fields[2]));
				break;
			case "Product":
				/*
				 *  3. product name
				 * 	4. product ID
				 * 	5-> machine route
				*/
				// tallentaa tiedot arraystä placeholdereihin ja luo objectin

				control.placeHolderName = fields[3];
				control.placeholderId = int.Parse(fields[4]);	

				for (int i = 5; i < fields.Length; i++) {
					control.placeholderList.Add (int.Parse(fields[i]));
				}

				control.InstantiateProduct (int.Parse(fields[1]), int.Parse(fields[2]));
				break;
			case "Storage":
				/*
				 *  3. storage size
				 * 	4. storage id
				 * 	5--> machines
				*/
				// tallentaa tiedot arraystä placeholdereihin ja luo objectin
				control.placeHolderValue = int.Parse(fields[3]); 
				control.placeholderId = int.Parse(fields[4]);

				for (int i = 5; i < fields.Length; i++) {
					control.placeholderList.Add (int.Parse(fields[i]));
				}

				control.InstantiateStorage (int.Parse(fields[1]), int.Parse(fields[2]));
				break;
			case "Machine":
				/*
				 *  3. machine speed
				 * 	4. machineID
				 * 	5. machineName
				*/
				// tallentaa tiedot arraystä placeholdereihin ja luo objectin
				control.placeHolderValue = int.Parse(fields[3]);
				control.placeholderId = int.Parse(fields[4]);
				control.placeHolderName = fields[5];
				control.InstantiateMachine (int.Parse(fields[1]), int.Parse(fields[2]));
				break;
			default:
			break;
			}
			// tyhjentää placeholder listan, muuten seuraava objecti saa myös edellisen objectin kohdat
			control.placeholderList = new List<int>();
			// Lukee seuraavan rivin

			text = file.ReadLine();
		}
		control.MachinesAdded ();
	}	

	// Tarkista mitä eroo on string ja stream ja muilla "readereilla"
}
