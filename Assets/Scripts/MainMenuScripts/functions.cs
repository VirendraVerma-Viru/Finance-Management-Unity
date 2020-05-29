using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class functions : MonoBehaviour {

	public Text status;
	public GameObject download_button;
	public string getappname;

	void Start()
	{
		status.text="Checking for any Update";
		download_button.SetActive (false);
		saveload.new_app_name = "";
		saveload.Save ();
		StartCoroutine (GetUpdateData());

	}
	IEnumerator GetUpdateData(){
		WWW itemsData = new WWW ("http://appdata.heliohost.org/notebook/update.php");
		yield return itemsData;
		getappname = itemsData.text;
		if (getappname == "" || getappname == null)
			status.text = "Check your Connection";
		download_button.SetActive (false);
		saveload.Load ();
		//print (getappname + "appname");
		//print (saveload.app_name + "savedname");
		if (saveload.app_name == getappname) {
			status.text="App is Up to date";
			download_button.SetActive (false);
			saveload.new_app_name = "";
			saveload.Save ();
		} else {
			download_button.SetActive (true);
			status.text="Update new Version";
			saveload.new_app_name = getappname;
			saveload.Save ();
		}
	}



}
