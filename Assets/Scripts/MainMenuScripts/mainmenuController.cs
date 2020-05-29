using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenuController : MonoBehaviour {

	public GameObject MainMenu;
	public GameObject OptionMenu;
	string getappname;
	public Text checkforupdatetext;
	void start()
	{
		StartCoroutine (GetUpdateData());
	}

	IEnumerator GetUpdateData(){
		WWW itemsData = new WWW ("http://appdata.heliohost.org/notebook/update.php");
		yield return itemsData;
		getappname = itemsData.text;

		saveload.Load ();
		//print (getappname + "appname");
		//print (saveload.app_name + "savedname");
		if (saveload.app_name == getappname) {
			checkforupdatetext.color = Color.black;

		} else {
			checkforupdatetext.color = Color.green;


		}
	}
	public void ExpenditureButton()
	{
		SceneManager.LoadScene (1);
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void Options()
	{
		MainMenu.SetActive (false);
		OptionMenu.SetActive (true);
	}

	public void BackButton()
	{
		OptionMenu.SetActive (false);
		MainMenu.SetActive (true);
	}

	public void ClearAllHistory()
	{
		saveload.save_data = "";
		saveload.Save ();

	}

	public void check_for_update()
	{
		SceneManager.LoadScene (2);
	}
}
