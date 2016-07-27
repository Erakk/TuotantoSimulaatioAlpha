using UnityEngine;
using System.Collections.Generic;

public class Machine : MonoBehaviour {

	// näyttää missä kohdassa karttaa kone on
	public int tileX;
	public int tileY;

	//tarkistaa onko kone jo käytössä
	public bool MachineInUse {get;set;}
	// kertoo koneen nimen
	public string MachineName {get;set;}
	// kertoo koneen numeron
	public int MachineId;
	// kertoo kuinka kauan tuotetta työstetään tällä koneella
	// (tunteina)
	public float MachineSpeed {get;set;}
	// kertoo mitä tuotteita tällä koneella voi työstää
	// TODO
	// johonkin templateen nappi, josta lisätään tähän listaan tuotteita
	public List<int> nextProducts;

	public float totalWorkerSpeed;

	void Start()
	{
		nextProducts = new List<int>();
	}
}

/* esimerkki listaan lisäämiseen

	public void ButtonOne(){
		products.add(new product("test product"));
	}
*/
