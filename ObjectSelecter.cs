using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectSelecter : MonoBehaviour {
	public static ObjectSelecter selecter;

	// UI ruudut, joissa on tiedot valitusta objectista
	public GameObject MachineScreen;
	public GameObject StorageScreen;

	// säilyttää valitun objectin idn
	private int selectedObjectId;
	// Use this for initialization

	// pääsy "ProgramControlleriin"
	private ProgramController control;

	void Start () {
		selecter = this;
		// luo polun luodulle "control" muuttujalle
		control = ProgramController.controller;
	}
	
	// Update is called once per frame
	void Update () {
		// Tarkistaa onko hiiri mahdollisesti UI elementin päällä
		// mikäli on, tämä estää tuotteiden lisäämisen kartalle
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}
		// tekee variable tallentamaan hiiren osoituksia
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hitInfo;

		// tarkistaa onko hiiri osunut objectiin
		if (Physics.Raycast(ray, out hitInfo)) 
		{
			// tallentaa variableen onjectin, johon hiiren soitus osui
			GameObject ourHitObject = hitInfo.collider.transform.gameObject;

			// tekee, mikäli hiiri osoittu konetta
			if (ourHitObject.GetComponent<Machine>() != null) 
			{
				// Mikäli Koneen objectista painetaan, avautuu kyseisen koneen valikkoruutu
				if (Input.GetMouseButtonDown(0)) {
					Machine machine = ourHitObject.GetComponent<Machine> ();
					MachineScreen.SetActive (true);

					// poistetaan kaikki vanhat tekstit valikosta (vaihtoehtona ois verrata tekstejä keskenäänja lisätä vain uudet, mutta todenäksesti ois enemmän koodia)
					GameObject[] objectTexts = GameObject.FindGameObjectsWithTag ("objectsInMachine");
					foreach (GameObject text in objectTexts) {
						Destroy (text);
					}
					selectedObjectId = machine.MachineId;

					// päivittää valikko ruudun (machineScreen) oikeilla teksteillä
					UpdateMachineWorkers ();
					UpdateMachineStorages ();
					UpdateMachineProductQueue (machine);
				}
			}
			// tekee, mikäli hiiri osoitti varastoa
			else if (ourHitObject.GetComponent<Storage>() != null) 
			{
				
				// Mikäli Varaston objectista painetaan, avautuu kyseisen varaston valikkoruutu
				if (Input.GetMouseButtonDown(0)) {
					Storage storage = ourHitObject.GetComponent<Storage> ();
					StorageScreen.SetActive (true);
					// poistetaan kaikki vanhat tekstit valikosta (vaihtoehtona ois verrata tekstejä keskenäänja lisätä vain uudet, mutta todenäksesti ois enemmän koodia)
					GameObject[] objectTexts = GameObject.FindGameObjectsWithTag ("objectsInStorage");
					foreach (GameObject text in objectTexts) {
						Destroy (text);
					}
					selectedObjectId = storage.StorageId;
					UpdateProductsInStorage ();
				}

			} 
			// tekee, mikäli hiiri osoitti jotain muuta (esim työntekijä tai tuote)
			else 
			{
				return;
			}
		}
	}

	// sulkee avatun kone/varasto ruudun
	public void CloseButton()
	{
		MachineScreen.SetActive (false);
		StorageScreen.SetActive (false);
	}

	// Päivittää työntekijä listan koneen valikko ruutuun
	void UpdateMachineWorkers ()
	{
		// variable, joka laskee montako työntekijää koneella on
		int workerCount = 0;
		GameObject folder = GameObject.Find ("WorkersForMachine");
		// varmistetaan, että koneella on ainakin yksi työntekijä, muuten lähtetään funktiosta
		if (GameObject.FindGameObjectsWithTag ("Worker").Length == 0) {
			return;
		}
		GameObject[] workers = GameObject.FindGameObjectsWithTag ("Worker");
		foreach (GameObject worker in workers) {
			// haetaan jokaisen työntekijän scripti, jota verrataan valittuun koneeseen
			Debug.Log(worker.name);
			Worker workerScript = worker.GetComponent<Worker> ();
			if (workerScript.WorkSlotId == selectedObjectId) {
				workerCount++;
				// Lisätään teksti objecti ohjelmaan valituilla asetuksilla
				control.CreateNewTextObject (folder, 
											"Worker: ",
											workerCount, 
											workerScript.WorkerId, 
											15,
											50,
											"objectsInMachine");
			}
		}
	}

	void UpdateMachineStorages()
	{
		// variable, joka laskee montako varastoa koneelle on määritetty
		int storageCount = 0;
		GameObject folder = GameObject.Find ("StoragesForMachine");
		// varmistetaan, että koneella on ainakin yksi varasto, muuten lähtetään funktiosta
		if (GameObject.FindGameObjectsWithTag ("Storage").Length == 0) {
			return;
		}
		// hakee kaikki objectit "storage" tagilla
		GameObject[] storages = GameObject.FindGameObjectsWithTag ("Storage");
		foreach (GameObject storage in storages) {
			// haetaan objectien scriptit
			Storage storageScript = storage.GetComponent<Storage> ();
			// käydään jokaisen haetun skriptin hyväksyty laitteet luettelot läpi
			foreach (int listMachine in storageScript.machines) {
				// mikäli kyseinen kone on varaston hyväksytyllä luettelolla, lisätään se listaan
				if (listMachine == selectedObjectId) {;
					storageCount++;
					control.CreateNewTextObject (folder, 
												"Storage: ",
												storageCount, 
												storageScript.StorageId, 
												15,
												50,
												"objectsInMachine");
				}
			}
		}
	}

	void UpdateMachineProductQueue(Machine machine)
	{

		GameObject folder = GameObject.Find ("ProductsForMachine");
		// Hakee kaikki tuotteet, jotka on listalla menossa koneeseen
		for (int i = 0; i < machine.nextProducts.Count; i++) {
			// tulostaa listan näytölle
			control.CreateNewTextObject (folder, 
										"Product: ",
										i + 1, 
										machine.nextProducts[i], 
										15,
										50,
										"objectsInMachine");
		}

	}
	void UpdateProductsInStorage()
	{
		// variable, johon lasketaan montako tuotetta varastossa on
		int productCount = 0;
		// tekee kansiosta, johon uudet objectit tehdään, variablen
		GameObject folder = GameObject.Find ("ProductsInStorage");
		//hakee kaikki tuotteet halutulla tagilla
		GameObject[] products = GameObject.FindGameObjectsWithTag ("Product");
		foreach (GameObject product in products) {
			//Hakee tuotteilta scriptin käyttöön
			Product productScript = product.GetComponent<Product> ();
			// vertaa tuotteen tämän hetkistä varastoa kyseessä olevaan varastoon
			if (productScript.selectedStorage == selectedObjectId) {
				productCount++;
				// tulostaa tuotteet ruudulle, jotka ovat valitussa varastossa
				control.CreateNewTextObject (folder, 
											"Product: ",
											productCount, 
											productScript.ProductNumber, 
											15,
											50,
											"objectsInStorage");
			}
		}
	}

}
