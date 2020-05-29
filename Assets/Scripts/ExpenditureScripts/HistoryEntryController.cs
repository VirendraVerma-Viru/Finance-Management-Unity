using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryEntryController : MonoBehaviour {

	public EntryController entry_controller;
	string data;
	string[] items;

	public void PreviousMonth()
	{
		entry_controller.selected_date = entry_controller.selected_date.AddMonths (-1);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("ExtraContent");
		foreach(GameObject enemy in enemies)
			GameObject.Destroy(enemy);
		entry_controller.Refresh ();
	}

	public void NextMonth()
	{
		entry_controller.selected_date = entry_controller.selected_date.AddMonths (1);


		GameObject[] enemies = GameObject.FindGameObjectsWithTag("ExtraContent");
		foreach(GameObject enemy in enemies)
			GameObject.Destroy(enemy);
		entry_controller.Refresh ();
	}

	public void ClearMonthDataButton()
	{
		
		saveload.Load ();
		data = saveload.save_data;
		if (data == ""||data==null) {
			print ("its null");
		} else {
			items = data.Split (';');
			int arraylength = items.Length;

			for (int i = 0; i < arraylength-1; i++) {

				System.DateTime dateTime = System.DateTime.Parse (GetDataValue (items [i], "DateTime:"));

				if (dateTime.ToString ("MM") == entry_controller.selected_date.ToString ("MM")) {
					items [i] = "";


					merge_and_save_file ();


				} else {
					
					//print ("Time Difference is " + (selected_date - dateTime).Days);
					print ("Selected Date" + entry_controller.selected_date);
				}
			}

		}
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("ExtraContent");
		foreach(GameObject enemy in enemies)
			GameObject.Destroy(enemy);
		entry_controller.Refresh ();
	}

	void merge_and_save_file()
	{
		int c = 0;
		data = "";
		print ("ItemsLength:"+items.Length);
		for (int i = 0; i < items.Length-1; i++) {
			data = data + items [i];
			if(items[i]!="")
			data=data+";";
		}

		//data = data + ";";
		saveload.save_data = data;
		saveload.Save ();
	}

	public void Refresh()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("ExtraContent");
		foreach(GameObject enemy in enemies)
			GameObject.Destroy(enemy);
		entry_controller.Refresh ();
	}

	string GetDataValue(string data,string index){
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if (value.Contains ("|"))
			value = value.Remove (value.IndexOf ("|"));
		return value;
	}

	public void ClearAll()
	{
		saveload.save_data = "";
		saveload.Save ();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("ExtraContent");
		foreach(GameObject enemy in enemies)
			GameObject.Destroy(enemy);
		entry_controller.Refresh ();
	}
}
