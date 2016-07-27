using UnityEngine;
using System.Collections.Generic;

public class Product : MonoBehaviour {

	// näyttää mistä kohdasta tuote lähtee liikkumaan
	public int tileX;
	public int tileY;

	// kertoo tuotteen "yleisnimen"
	public string ProductName;
	// kertoo tuotteen uniikin numeron
	public int ProductNumber;
	// kertoo reitin mitä koneita kyseinen tuote käy läpi
	// merkitää koneidet id:t järjestykseen
	public List<int> route;

	// pääsy "ProgramControlleriin"
	private ProgramController control;

	// strnigi kertomaan mitä tuote tekee seuraavaksi
	private string workPhase;

	// onko tuotetta viellä laitettu seuraavan koneen listalle
	private bool addedToMachine = false;
	private bool machineDone = true;

	// tekee ajastimen kauanko tuote on viellä työkoneella
	private float workTimeLeft;

	// variable seuraavan työstökoneen ID:seen
	private int nextMachineId = 0;

	// tallennetaan varasto, johon objectia kuljetetaan
	public int selectedStorage = 0;

	// tapahtumat, joita tapahtuu heti kun tile luodaan
	void Start(){
		// luo polun luodulle "control" muuttujalle
		control = ProgramController.controller;

		// tehtävä, jota tuote alkaa tekemänä esimmäisenä
		workPhase = "moveToMachine";
	}

	// Update päivittyy jokaisella framella. Liikuttaa tuotetta koneelta koneelle
	void Update()
	{
		// skippaa päivitys osuudet jos ohjelma on pausella
		if (control.programSpeed == 0) {
			return;
		}

		// Tarkistaa, että reitillä on viellä koneita käytävänä
		if (route.Count != 0) {
			// Haetaan seuraavan koneen scripti variableen
			if (machineDone) {
				// tallentaa seuraavan koneen ID:n koneen hakemista varten
				nextMachineId = route [0];
				machineDone = false;
			}
			// if:fi, joka varmistaa, että route "id" on oikea
			// NOTE: en ole varma tarvitaanko
			if (nextMachineId == 0) 
			{
				return;
			}
			GameObject nextMachineObject = GameObject.Find ("Machine: " + nextMachineId);
			Machine nextMachine = nextMachineObject.GetComponent<Machine> ();

			switch (workPhase) {
			// asiat, joita tapahtuu, kun tuote on liikkeellä koneiden välillä
			case "moveToMachine":
				MoveToMachine (nextMachine, nextMachineObject);
				break;
			// asiat, joita tapahtuu, kun tuote on siirtymässä varastoon
			case "moveToStorage":
				MoveToStorage (nextMachine);
				break;
			// asiat, joita tapahtuu, kun tuote on varastoituna
			case "storage":
				UseStorage(nextMachine);
				break;
			// asiat, joita tapahtuu, kun tuotetta työstetään
			case "work":
				UseMachine(nextMachine);
				break;
			default:
				break;
			}
		} 
		else 
		{
			// työ valmis ja tuote tuhoukseen
			Destroy(gameObject);

		}

	}

	// Muuttaa "tilemap" koordinaatin "maailman" kordinaatistoon
	Vector3 TileCoordToWorldCoord(int x, int y)
	{
		return new Vector3(x, -0.5f, y);
	}

	void MoveToMachine(Machine nextMachine, GameObject nextMachineObject)
	{
		// tarkistetaan, onko konetta viellä lisätty seuraavan koneen listalle
		if (!addedToMachine) {

			// mikäli kone ei ollut listalla, lisätään se ja estetään uudestaan lisäys
			nextMachine.nextProducts.Add (ProductNumber);
			addedToMachine = true;
		}
		// haetaan seuraavan koneen kordinaatit tähän funktioon 
		// NOTE: nämä variable voisi "skipata", mutta jätän ainakin toistaseks selkeyden takia
		int machineX = nextMachine.tileX;
		int maxhineY = nextMachine.tileY;
		// katsotaan onko tuote päässyt viellä koneelle
		if (transform.position != nextMachineObject.transform.position ) {
			// siirrytään seuraavan kotaan "animoiden"
			transform.position = Vector3.MoveTowards (transform.position, TileCoordToWorldCoord(machineX, maxhineY), control.programSpeed * Time.deltaTime);
		} 
		else 
		{


			// Tarkistaa onko tämä tuote seuraavana listalla
			// mikäli on, aletaan tuotetta työstämään
			if (nextMachine.nextProducts[0] == ProductNumber ) 
			{
				workPhase = "work";
			} 
			// mikäli ei ole, tuote siirretään varastoon
			else 
			{
				workPhase = "moveToStorage";
			}

		}
	}
	void MoveToStorage(Machine nextMachine)
	{
		// lähtee takasin toihin, mikäli laite tyhjenee kuljetuksen aikana
		if (nextMachine.nextProducts[0] == ProductNumber ) 
		{
			workPhase = "moveToMachine";
			selectedStorage = 0;
			return;
		} 
		//NOTE: menettää ensimmäisen framen liikkeestä. Ongelman voisi poistaa laittamalla liikkeen esim omaan functioon ja lisäämällä function "elseen" ja selectedStorage määrityksen perään, mutta en koe tarpeelliseksi

		if (selectedStorage == 0) {
			// Haetaan kaikki varastot "tagin" perusteella
			GameObject[] Storages = GameObject.FindGameObjectsWithTag ("Storage");
			foreach (GameObject storage in Storages) {
				// Haetaan jokaisen varston nimikko scripti
				Storage storageScript = storage.GetComponent<Storage> ();
				// haetaan jokaisesta "storage" sriptistä kone listaus
				foreach (int id in storageScript.machines) {
					// tarkistetaan, että varasto on sopivalle koneelle, sekä varastossa on tilaa
					if (id == nextMachineId && storageScript.StorageSize > storageScript.ItemsInStorage) {
						// tallennetaan varaston ID talteen, jotta "foreach" looppeja ei tarvitse käydä läpi joka framella
						selectedStorage = storageScript.StorageId;
					}
				}
			}
		}
		// mikäli haluttu varasto on haettu, liikutaan tänne
		else {
			// haetaan varaston objecti ja scripti
			GameObject nextStorageObject = GameObject.Find ("Storage: " + selectedStorage);
			Storage nextStorage = nextStorageObject.GetComponent<Storage> ();

			// tehdään variable x/y koordinaateista
			int storageX = nextStorage.tileX;
			int storageY = nextStorage.tileY;

			// liikutaan, mikäli ei olla viellä perillä
			if (transform.position != nextStorageObject.transform.position ) {
				// siirrytään seuraavan kotaan "animoiden"
				transform.position = Vector3.MoveTowards (transform.position, TileCoordToWorldCoord(storageX, storageY), control.programSpeed * Time.deltaTime);
			} 
			// vaihdetaan työkuva, mikäli ollaan perillä
			else 
			{
				workPhase = "storage";
			}
		}
	}
	void UseMachine(Machine nextMachine)
	{
		// tarkistaa onko konetta viellä poistettu tuotteen listalta
		// NOTE: samaa booleania käytetään "MoveToMachine" kohdassa
		if (addedToMachine) {
			// laittaa ajan kauanko kone työstää kyseistä tuotetta
			// TODO laita oikea aika
			workTimeLeft = nextMachine.MachineSpeed; 

			addedToMachine = false;
		}
		// vähentää aikaa joka framella ( aika vähenee aina suhteess ohjelman nopeuteen)
		workTimeLeft -= Time.deltaTime * control.programSpeed;
		// tarkistaa onko aikaa viellä jäljellä
		if (workTimeLeft <= 0) {
			workPhase = "moveToMachine";
			machineDone = true;
			// Poistaa ensimmäisen koneen listalta, jotta tuote vois alkaa liikkumaan seuraavaa konetta kohti
			route.RemoveAt (0);
			// Poistaa koneen listalta ensimmäisen tuotteen, jotta uuden tuotteen voi tuoda sisälle
			nextMachine.nextProducts.RemoveAt (0);
		}
	}
	void UseStorage(Machine nextMachine)
	{
		// tarkistaa, onko tuote seuraavana seuraavan koneen listalla
		// mikäli on, siirretään tuotetta sinne
		if (nextMachine.nextProducts[0] == ProductNumber ) 
		{
			workPhase = "moveToMachine";
			selectedStorage = 0;
		} 
	}
}
