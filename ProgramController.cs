using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ProgramController : MonoBehaviour {

	// tekee staattisen muuttujan tästä scirptistä
	// helpompi käyttää muualla
	public static ProgramController controller;

	// luo perus objecteille objectit scripteihin
	public GameObject worker;
	public GameObject machine;
	public GameObject product;
	public GameObject storage;

	//Nappi, jota käytetään konelistassa
	public GameObject MachineListButton;

	// Kone lista. 
	public GameObject MachineList;
	public GameObject SelectedMachinesList;

	// luodaan scripteihin helpompi mahdollisuus käyttää option ruutuja
	public GameObject workerOptionScreen;
	public GameObject machineOptionScreen;
	public GameObject productOptionScreen;
	public GameObject storageOptionScreen;

	// jokaiselle ruudulle tarkastus, onko juuri tämä ruutu päällä. 
	// Note: olisi mahdollista tehdä integerilläkin, mutta se toisi ylimääräisen switch operaattorin
	// saattaisi myös olla mahdollista käyttää päällä olevia GameObjecteja tähän tarjkoitukseen, mutta en nopeasti keksinyt mieten sen saisi toimimaan
	public bool workerScreenOn;
	public bool machineScreenOn;
	public bool productScreenOn;
	public bool storageScreenOn;

	//Placeholderit jotka myöhemmin kopioidaan haluttuihin scripteihin. Tuskin paras tapa tehdä.
	public int placeHolderValue;
	public int placeholderId;
	public string placeHolderName = "0";
	public int placeHolderSlot;
	public List<int> placeholderList;

	// numeraalinen arvo eri objecteille. Näitä numeroita käytetään "clickableTile" scriptssä määrittämään minkälaista objectia ollaan luomassa
	public int SelectedObject = 0;

	// käytetään Machine listassa katsomassa mitä ominaisuutta halutaan käyttää
	public string SelectedNeedMachine;

	public int programSpeed = 0;

	// tapahtumat, joita tapahtuu ohjelman käynnistyessä
	void Awake(){
		controller = this;
	}
	void Start(){
		placeholderList = new List<int>();
	}	
	// luo tehtävät, joita "Add Machine" nappi tekee
	public void AddMachineButton(){
		TurnAllOptionsInActive ();

		// laitetaan pelkästään tämän valikon Options ruutu päälle
		machineOptionScreen.SetActive (true);
		machineScreenOn = true;

	}
	// luo tehtävät, joita "Add Person" nappi tekee
	public void AddWorkerButton(){
		TurnAllOptionsInActive ();

		// laitetaan pelkästään tämän valikon Options ruutu päälle
		workerOptionScreen.SetActive (true);
		workerScreenOn = true;
	}
	// luo tehtävät, joita "Add Storage" nappi tekee
	public void AddStorageButton(){
		TurnAllOptionsInActive ();

		// laitetaan pelkästään tämän valikon Options ruutu päälle
		storageOptionScreen.SetActive (true);
		storageScreenOn = true;
	}
	// luo tehtävät, joita "Add Product" nappi tekee
	public void AddProductButton(){
		TurnAllOptionsInActive ();

		// laitetaan pelkästään tämän valikon Options ruutu päälle
		productOptionScreen.SetActive (true);
		productScreenOn = true;
	}
	// Alla "Add" nappien ominaisuuksien hyväksymis napit
	public void AddMachineInstantiate(){
		ClearInputFieldTexts ();
		SelectedObject = 1;

		TurnAllOptionsInActive ();

	}
	public void AddWorkerInstantiate(){
		ClearInputFieldTexts ();
 		// Laittaa päälle koneen valinta list. Työn tekijä liitetään kyseiselle koneelle

		MachineList.SetActive(true);

		// Määrittää valinnan "tuotteeseen", jotta konelistan nappit tekee oikeat toiminnot
		SelectedNeedMachine = "worker";
		TurnAllOptionsInActive ();
		AllowAllAddMachineButtons ();

	}
	public void AddStorageInstantiate(){
		ClearInputFieldTexts ();
		// Laittaa päälle koneen valinta listan ja reittilistan jonka tuote tulee kulkemaan
		MachineList.SetActive(true);
		SelectedMachinesList.SetActive(true);

		// Määrittää valinnan "tuotteeseen", jotta konelistan nappit tekee oikeat toiminnot
		SelectedNeedMachine = "storage";
		TurnAllOptionsInActive ();
		AddedMachineListResetButton ();
		AllowAllAddMachineButtons ();

	}
	public void AddProductInstantiate(){
		ClearInputFieldTexts ();
		// Laittaa päälle koneen valinta listan ja reittilistan jonka tuote tulee kulkemaan
		MachineList.SetActive(true);
		SelectedMachinesList.SetActive(true);

		// Määrittää valinnan "tuotteeseen", jotta konelistan nappit tekee oikeat toiminnot
		SelectedNeedMachine = "product";
		TurnAllOptionsInActive ();
		AddedMachineListResetButton ();
		AllowAllAddMachineButtons ();

	}

	// laitetaan kaikki option ruudut pois päältä. Tämä scipti ajetaan aina objecti nappia painettaessa
	void TurnAllOptionsInActive()
	{
		

		workerOptionScreen.SetActive (false);
		machineOptionScreen.SetActive (false);
		productOptionScreen.SetActive (false);
		storageOptionScreen.SetActive (false);

		workerScreenOn = false;
		machineScreenOn = false;
		productScreenOn = false;
		storageScreenOn = false;
	}

	// Tarkistaa, että ID sarake on täytetty objectia luodessa
	bool IdChecker()
	{
		if (placeholderId > 0) {
			return true;
		}
		return false;
	}

	// Tarkistaa, että nimi on täytetty objectia luodessa
	bool NameChecker()
	{
		if (placeHolderName == "") {
			placeHolderName = "0";
		}

		if (placeHolderName != "0") {
			return true;
		}
		return false;
	}

	// Tarkistaa, että tarvittava arvo on täytetty objectia luodessa
	bool ValueChecker()
	{
		if (placeHolderValue > 0) {
			return true;
		}
		return false;
	}

	// laittaa työtekijä napin aktiiviseksi, mikäli arvo ei ole tyhjä
	public void EnableWorkerButton(){
		if (!workerScreenOn) {
			return;
		}

		if (ValueChecker() && IdChecker()) 
		{
			// Etsii sovelluksesta "ButtonWorker" nimisen objectin--> hakee siltä komonentin "Button", josta laittaa napin käytettäväksi
			GameObject.Find ("ButtonWorker").GetComponent<Button> ().interactable = true;
		} 
		else 
		{
			GameObject.Find ("ButtonWorker").GetComponent<Button> ().interactable = false;
		}
	}

	// laittaa tuote napin aktiiviseksi, mikäli arvo ei ole tyhjä
	public void EnableProductButton(){
		if (!productScreenOn) {
			return;
		}

		if (IdChecker() && NameChecker()) 
		{
			// Etsii sovelluksesta "ButtonProduct" nimisen objectin--> hakee siltä komonentin "Button", josta laittaa napin käytettäväksi
			GameObject.Find ("ButtonProduct").GetComponent<Button> ().interactable = true;
		} 
		else 
		{
			GameObject.Find ("ButtonProduct").GetComponent<Button> ().interactable = false;
		}
	}
	// laittaa varasto napin aktiiviseksi, mikäli arvo ei ole tyhjä
	public void EnableStorageutton(){
		if (!storageScreenOn) {
			return;
		}

		if (ValueChecker() && IdChecker()) 
		{
			// Etsii sovelluksesta "ButtonStorage" nimisen objectin--> hakee siltä komonentin "Button", josta laittaa napin käytettäväksi
			GameObject.Find ("ButtonStorage").GetComponent<Button> ().interactable = true;
		} 
		else 
		{
			GameObject.Find ("ButtonStorage").GetComponent<Button> ().interactable = false;
		}
	}
	// laittaa työkone napin aktiiviseksi, mikäli arvo ei ole tyhjä
	public void EnableMachineButton(){
		if (!machineScreenOn) {
			return;
		}

		if (ValueChecker() && IdChecker() && NameChecker()) 
		{
			// Etsii sovelluksesta "ButtonMachine" nimisen objectin--> hakee siltä komonentin "Button", josta laittaa napin käytettäväksi
			GameObject.Find ("ButtonMachine").GetComponent<Button> ().interactable = true;
		} 
		else 
		{
			GameObject.Find ("ButtonMachine").GetComponent<Button> ().interactable = false;
		}
	}

	//Tarkistaa onko koneita viellä lisätty. 
	//Tämä vaikuttaa työntekijöiden ja tuotteiden lisäämiseen, sillä ne tarvitsevat koneen johon ne liitetään
	public void MachinesAdded()
	{
		// Etsii kaikki "machine" tagin omaavat objetit luo integerin niiden määrän perusteella
		int machines =  GameObject.FindGameObjectsWithTag ("Machine").Length;
		if (machines > 0) {
			GameObject.Find ("Add_Worker").GetComponent<Button> ().interactable = true;
			GameObject.Find ("Add_Product").GetComponent<Button> ().interactable = true;
			GameObject.Find ("Add_Storage").GetComponent<Button> ().interactable = true;
			return;
		}	
		GameObject.Find ("Add_Worker").GetComponent<Button> ().interactable = false;
		GameObject.Find ("Add_Product").GetComponent<Button> ().interactable = false;
		GameObject.Find ("Add_Storage").GetComponent<Button> ().interactable = false;
		return;
	}


	public void AddMachineToList()
	{
		// variablet joita tullaan äyttämään useassa eri switchin kohdassa
		string buttonName = EventSystem.current.currentSelectedGameObject.name;
		GameObject go = GameObject.Find (buttonName);
		Machine id = go.GetComponent<Machine> ();
		placeHolderSlot =  id.MachineId;
		switch (SelectedNeedMachine) {
		case "worker":			

			SelectedObject = 2;
			Debug.Log(SelectedObject);
			MachineList.SetActive(false);
			break;
		case "product":
			// Laittaa tuotteen placeholder listaan (lista kopioidaan myöhemmin tuotteen reitti listaan)
			placeholderList.Add (placeHolderSlot);
			// Päivittää UI listan, jossa näkyy jo valitut koneet
			UpdateAddedMachinesList ();
			break;
		case "storage":
			// Laittaa tuotteen placeholder listaan (lista kopioidaan myöhemmin varaston hyväksyty listaan)
			placeholderList.Add (placeHolderSlot);
			// Päivittää UI listan, jossa näkyy jo valitut koneet
			UpdateAddedMachinesList ();
			break;
		default:
			break;
		}

	}
	// Päivittää kone listaa aina kun tuotteen reitille valitaan uusi kone
	public void UpdateAddedMachinesList()
	{
		GameObject backGround = GameObject.Find ("ImageBG");
		// tekee tekstiobjectin objectin listalle
		CreateNewTextObject (backGround, 
							"Machine: " ,
							placeholderList.Count, 
							placeholderList[placeholderList.Count - 1], 
							15,
							0,
							"MachineListText");
		// laittaa kaikki koneiden lisäys napit aktiivisiksi
		AllowAllAddMachineButtons ();
		// estää saman napin painamisen uudestaan
		string buttonName = EventSystem.current.currentSelectedGameObject.name;
		GameObject.Find (buttonName).GetComponent<Button> ().interactable = false;
	}

	// Nappi jolla mennään kone listauksesta eteenpäin
	// TODO: estä napin käyttö, jos lista on tyhjä
	public void AddedMachineListContinueButton()
	{
		
		// valitsee objectin, joka on ollut valittuna
		switch (SelectedNeedMachine) {
		case "product":
			SelectedObject = 4;
			break;
		case "storage":
			SelectedObject = 3;
			break;
		default:
			break;
		}
		// laittaa listaukset pois ruudulta
		MachineList.SetActive(false);
		SelectedMachinesList.SetActive(false);
	}

	public void AddedMachineListResetButton()
	{
		placeholderList = null;
		placeholderList = new List<int>();

		// hakee kaikki objectit "machinelisttext" tagilla ja poistaa ne
		GameObject[] MachineListTexts = GameObject.FindGameObjectsWithTag ("MachineListText");
		foreach (GameObject text in MachineListTexts) {
			Destroy (text);
		}
	}

	// Tyhjentää inputfieldit tulevia käyttöjä varten
	void ClearInputFieldTexts()
	{
		GameObject[] texts = GameObject.FindGameObjectsWithTag ("InputFieldText");
		foreach (GameObject text in texts) {
			text.GetComponent<InputField> ().text = "";
		}
	}

	public void CreateNewTextObject(GameObject folder, string nameStart, int listPlace, int id, int gap, int startGap, string tag)
	{
		// tekee uuden perus objectin editoriin
		GameObject machineText = new GameObject (listPlace + ". " + nameStart);
		// Lisätää objectiin ne ominaisuudet jota halutaan
		machineText.AddComponent<CanvasRenderer> ();
		//Laittaa objectin oikean taustan päälle
		machineText.transform.SetParent(folder.transform);
		machineText.tag = tag;
		//Laitetaan objectille componentti, josta voidaan säätää objecti haluttuun kohtaan
		RectTransform machinePos = machineText.AddComponent<RectTransform> ();
		// ankkuroidaan teksti siten, että se pysyy aina tälle määritetyn taustan alueella
		machinePos.anchorMin = Vector2.zero;
		machinePos.anchorMax = Vector2.one;
		machinePos.anchoredPosition = Vector2.zero;
		machinePos.sizeDelta = Vector2.zero;
		machinePos.transform.localPosition = new Vector3 (0,(- startGap + ((listPlace - 1) * - gap)),0);
		// lisätään interfaceen teksti ja muokataan sitä
		Text uiText = machineText.AddComponent<Text> ();
		// Mitä tekstissä lukee
		uiText.text =  listPlace + ". " + nameStart + " " + id;
		// Millä fontilla teksti kirjoitetaan
		Font ArialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
		uiText.font = ArialFont;
		// tekstin väri
		uiText.color = Color.black;
	}

	// laittaa kaikki koneiden lisäys napit aktiivisiksi
	void AllowAllAddMachineButtons()
	{
		// halee kaikki napit joissa on määritetty "tag"
		GameObject[] addMachineButtons = GameObject.FindGameObjectsWithTag ("AddMachineButton");
		foreach (GameObject button in addMachineButtons) {
			// laittaa kaikki määritetyt napit aktiivisiksi
			button.GetComponent<Button> ().interactable = true;
		}
	}
	public void CloseProgram()
	{
		Application.Quit ();
	}
	public void RestartProgram()
	{
		Application.LoadLevel ("Main");
	}

	public void InstantiateWorker( int tileX, int tileY)
	{
		// laittaa halutun ikonin tämän "tilen" päälle
		GameObject go = (GameObject)Instantiate (worker, new Vector3 (tileX, -0.5f, tileY), Quaternion.identity);
		// Haekee objectille scriptin
		Worker workerScript = go.GetComponent<Worker> ();
		// Laittaa lisätylle työntekijälle tekovaiheessa määritetyn nopeuden
//		Debug.Log(workerScript.WorkSpeed);
		Debug.Log (placeHolderValue);
		workerScript.WorkSpeed = placeHolderValue;
		workerScript.WorkerId = placeholderId;
		workerScript.WorkSlotId = placeHolderSlot;

		workerScript.tileX = tileX;
		workerScript.tileY = tileY;


		//Nimeä tämä objecti
		go.name = "Worker: " + workerScript.WorkerId;

		// Laittaa objectin editorissa oikean kansioon. Lähinnä siisteys syistä.
		GameObject parent = GameObject.Find ("Workers");
		go.transform.parent = parent.transform;
	}
	public void InstantiateMachine( int tileX, int tileY)
	{
		// Käytetään muuttujana laittamaan lisätyt objetit oikeisiin kansioihin
		GameObject parent;

		//Käytetään muuttujana luomaan objecti tämän alle
		GameObject go;

		// laittaa halutun ikonin tämän "tilen" päälle
		go = (GameObject)Instantiate (machine, new Vector3 (tileX, -0.5f, tileY), Quaternion.identity);
		// Hakee objectille scriptin
		Machine machineScript = go.GetComponent<Machine> ();
		// laittaa koneelle määritetyt arvot
		machineScript.MachineSpeed = placeHolderValue;
		machineScript.MachineId = placeholderId;
		machineScript.MachineName = placeHolderName;

		// kopioidaan tämän tile koordinaatit objectille
		machineScript.tileX = tileX;
		machineScript.tileY = tileY;


		//Nimeä tämä objecti
		go.name = "Machine: " + machineScript.MachineId;

		// Laittaa objectin editorissa oikean kansioon. Lähinnä siisteys syistä.
		parent = GameObject.Find ("Machines");
		go.transform.parent = parent.transform;


		// NOTE: alla oleva koodinpätkä break; asti antaa varoitusta. Ei pitäisi olla vaikutusta kuitenkaan toimintaan. Katso myöhemmin onko hyvä juttu
		// Warning tulee siitä, että kansio pakottaa "buttonin" olemaan tietyssä kohdassa, jonka takia se ei ilmesty kohtaa johon se varsinaisesti kutsutaan

		//Tekee napin scrollbaariin, jota käytetään koneiden valitsemiseen
		GameObject button = (GameObject)Instantiate (MachineListButton, new Vector3 (0, 0, 0), Quaternion.identity);

		// Lisätään napillekkin scripti. Tästä scriptiä käytetään silloin, kun työntekijälle tai tuotteelle määritetään reittiä
		Machine machineButton = button.AddComponent<Machine> ();

		machineButton.MachineId = placeholderId;

		// nimeää objectin ja antaa tekstin itse napille
		button.name = machineScript.MachineName + " " + machineScript.MachineId;
		button.GetComponentInChildren<Text> ().text = machineScript.MachineName + " " + machineScript.MachineId;



		//Laittaa napin oikeaan kansioon
		// Note: en tykkää "setactive" koodinpätkistä. Unity ei suostu laittamaan objecteja kansioihin, jotka oven "inactive", josta syystä laitan nämä hetkellisesti päälle
		MachineList.SetActive (true);

		GameObject parentMachineButton = GameObject.Find ("MachineListFitter");
		button.transform.parent = parentMachineButton.transform;

		// Lisätään nappiin "onClick" ominaisuus.
		// NOTE: muissa napeissa tehnyt tämän vaiheen suoraa editorista

		// Mahdolisesti toinen tapa tehdä tämä olisi "button.GetComponent<Button> ().onClick.AddListener(control.AddMachineToList ());"
		button.GetComponent<Button> ().onClick.AddListener (() => AddMachineToList ());

		MachineList.SetActive(false);
	}
	public void InstantiateStorage( int tileX, int tileY)
	{
		// Käytetään muuttujana laittamaan lisätyt objetit oikeisiin kansioihin
		GameObject parent;

		//Käytetään muuttujana luomaan objecti tämän alle
		GameObject go;
		// laittaa halutun ikonin tämän "tilen" päälle
		go = (GameObject)Instantiate (storage, new Vector3 (tileX, -0.5f, tileY), Quaternion.identity);
		// Hakee objectille scriptin
		Storage storageScript = go.GetComponent<Storage> ();
		// laittaa varastoon määritetyt arvot
		storageScript.StorageSize = placeHolderValue;
		storageScript.StorageId = placeholderId;
		storageScript.machines = new List<int> (placeholderList);

		// kopioidaan tämän tile koordinaatit objectille
		storageScript.tileX = tileX;
		storageScript.tileY = tileY;

		//Nimeä tämä objecti
		go.name = "Storage: " + storageScript.StorageId ;

		// Laittaa objectin editorissa oikean kansioon. Lähinnä siisteys syistä.
		parent = GameObject.Find ("Storages");
		go.transform.parent = parent.transform;
	}
	public void InstantiateProduct(int tileX, int tileY)				
	{
		// Käytetään muuttujana laittamaan lisätyt objetit oikeisiin kansioihin
		GameObject parent;

		//Käytetään muuttujana luomaan objecti tämän alle
		GameObject go;
		// laittaa halutun ikonin tämän "tilen" päälle
		go = (GameObject)Instantiate (product, new Vector3 (tileX, -0.5f, tileY), Quaternion.identity);
		// Hakee objectille scriptin
		Product productScript = go.GetComponent<Product> ();
		// laittaa tuotteelle määritetyt arvot
		productScript.ProductNumber = placeholderId;

		productScript.ProductName = placeHolderName;	
		productScript.route = new List<int> (placeholderList);

		// kopioidaan tämän tile koordinaatit objectille
		productScript.tileX = tileX;
		productScript.tileY = tileY;;

		//Nimeä tämä objecti
		go.name = "Product: " + productScript.ProductName + " " + productScript.ProductNumber;

		// Laittaa objectin editorissa oikean kansioon. Lähinnä siisteys syistä.
		parent = GameObject.Find ("Products");
		go.transform.parent = parent.transform;
	}

}
