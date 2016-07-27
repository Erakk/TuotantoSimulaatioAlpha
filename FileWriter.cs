using UnityEngine;
using System.Collections;
using System.IO;

public class FileWriter : MonoBehaviour {

	public void SaveToFile()
	{
		// telee tiedostopolun, johon voidaan kirjoittaa
		StreamWriter file = new StreamWriter (@"C:\Users\i7-6700K\Desktop\Projektit\simtekstiW.txt");

		// Haetan kaikki halutut objectit erillisiin arrayhin ( erillisiin, koska eri objetit halutaan kirjoittaa eri tavoin)
		GameObject[] machines = GameObject.FindGameObjectsWithTag ("Machine");
		GameObject[] storages = GameObject.FindGameObjectsWithTag ("Storage");
		GameObject[] workers = GameObject.FindGameObjectsWithTag ("Worker");
		GameObject[] products = GameObject.FindGameObjectsWithTag ("Product");
		// variable, jonka tiedot kirjotetaan tiedostoon
		string output;

		// käydään läpi jokainen kone
		foreach (var machine in machines) {
			// haetaan objectin scripti
			Machine machineScr = machine.GetComponent<Machine> ();
			// kirjoitetaan scriptistä halutut tiedot variableem
			output = "Machine" + ":" + machineScr.tileX.ToString() + ":" + machineScr.tileY.ToString()  + ":" + machineScr.MachineSpeed.ToString()  + ":" + machineScr.MachineId.ToString()  + ":" + machineScr.MachineName ;
			// kirjoitetaan tiedot tiedostoon
			file.WriteLine (output);
		}
		// käydään läpi jokainen kone
		foreach (var storage in storages) {
			Storage storageScr = storage.GetComponent<Storage> ();
			output = "Storage" + ":" + storageScr.tileX.ToString () + ":" + storageScr.tileY.ToString () + ":" + storageScr.StorageSize.ToString () + ":" + storageScr.StorageId.ToString ();
			// käydään varastossa olevan listan kaikki kohdat läpi, ja kirjoitetaan ne "output" variablen perään
			for (int i = 0; i < storageScr.machines.Count; i++) {
				output = output + ":" + storageScr.machines[i].ToString();
			}
			file.WriteLine (output);
		}
		// käydään läpi jokainen kone
		foreach (var worker in workers) {
			Worker workerScr = worker.GetComponent<Worker> ();
			output = "Worker" + ":" + workerScr.tileX.ToString() + ":" + workerScr.tileY.ToString()  + ":" + workerScr.WorkSlotId.ToString()  + ":" + workerScr.WorkSpeed.ToString()  + ":" + workerScr.WorkerId.ToString() ;
			file.WriteLine (output);
		}
		// käydään läpi jokainen kone
		foreach (var product in products) {
			Product productScr = product.GetComponent<Product> ();
			output = "Product" + ":" + productScr.tileX.ToString() + ":" + productScr.tileY.ToString()  + ":" + productScr.ProductName + ":" + productScr.ProductNumber.ToString();
			for (int i = 0; i < productScr.route.Count; i++) {
				output = output + ":" + productScr.route[i].ToString();
			}
			file.WriteLine (output);
		}

		file.Close ();
	}
}
