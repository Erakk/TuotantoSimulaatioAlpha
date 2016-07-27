using UnityEngine;
using System.Collections.Generic;

public class Storage : MonoBehaviour {
	
	// näyttää missä kohdassa karttaa varasto on
	public int tileX;
	public int tileY;
	// kertoo varaston koon
	public int StorageSize {get;set;}
	// Pitää lukua montako tuotetta varastossa on
	public int ItemsInStorage;
	// kertoo varaston numeron
	public int StorageId {get;set;}
	// kertoo mitkä koneet voivat varastoa käyttää
	public List<int> machines;
}
