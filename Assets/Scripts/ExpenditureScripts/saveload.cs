using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

public class saveload : MonoBehaviour {

	public static string save_data;
	public static float overall_earn;
	public static string app_name;
	public static string new_app_name;

	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/info.dat");
		Finance_Data data = new Finance_Data ();
		data.Overall_Earn = overall_earn;
		data.Save_data = save_data;
		data.Appname = app_name;
		data.Newappname = new_app_name;
		bf.Serialize (file, data);
		file.Close ();
	}

	public static void Load()
	{

		if (File.Exists (Application.persistentDataPath + "/info.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/info.dat", FileMode.Open);
			Finance_Data data = (Finance_Data)bf.Deserialize (file);
			app_name = data.Appname;
			overall_earn = data.Overall_Earn;
			save_data = data.Save_data;
			new_app_name = data.Newappname;
			file.Close ();
			if (app_name == "" || app_name == null)
				app_name = "notebook.apk";
		} else
			saveload.Save ();
	}
}


[Serializable]
class Finance_Data
{
	public float Overall_Earn;
	public string Save_data;
	public string Appname;
	public string Newappname;
}