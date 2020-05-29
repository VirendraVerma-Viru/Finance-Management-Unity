using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkforupdatefunction : MonoBehaviour {

	public void Download_button()
	{
		saveload.Load ();
		if (saveload.new_app_name != ""||saveload.new_app_name!=null) {
			string getappname = saveload.new_app_name;
			Application.OpenURL ("http://appdata.heliohost.org/notebook/app/" + getappname);
		}
	}

	public void back_button()
	{
		SceneManager.LoadScene (0);
	}
}
