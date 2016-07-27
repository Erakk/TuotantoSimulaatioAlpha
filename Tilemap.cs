using UnityEngine;
using System.Collections.Generic;

public class Tilemap : MonoBehaviour {

	// tekee staattisen muuttujan tästä scirptistä
	// helpompi käyttää muualla
	public static Tilemap map;

	// muokattavat x ja y coordinaatit
	// määrittelee alueen koon
	public int x;
	public int y;

	// tuodaat editorista perus "tile" alku alueen rakentamista varten
	public GameObject tile;

	void Awake(){
		map = this;
	}

	void Start()
	{
		GenerateArea ();
	}


	// luo alkualueen x:n ja y:n mukaan
	void GenerateArea(){
		// luo määriteltyjen y ja x kordinaattien mukaan oiekan kokoisen kartan
		// käyttäen kahta "for" lauseketta (toinen y akselille ja toinen x akselille)
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				
				// laittaa jokaisen "tilen" kohdalle ennlata määritetyn objectin
				GameObject go = (GameObject)Instantiate(tile, new Vector3(i, -0.5f, j), Quaternion.identity);

				// hakee luodun objectin scriptin, johon tekee perus määritykset
				ClickableTile ct = go.GetComponent<ClickableTile>();
				ct.tileX = i;
				ct.tileY = j;

				// nimeää tilen käyttöliittymän selventämiseksi
				go.name = "tile_" + i + "_" + j;
				// laittaa luodun objectin määriteltyyn kansioon käyttöliittymän selventämiseksi
				go.transform.SetParent(this.transform);
				// laittaa objectille "tag":in myöhäisempää käyttöä varten
				go.tag = "Tile";
			}
		}
	}
}
