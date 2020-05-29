using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class EntryController : MonoBehaviour {

	public InputField Earn_title;
	public InputField Earn_cost;

	public InputField Expence_title;
	public InputField Expence_cost;

	public GameObject addentries;
	public Transform addentries_parent;
	public GameObject earning_entries;
	public Transform earning_entries_parent;
	public GameObject expence_entries;
	public Transform expence_entries_parent;

	public GameObject go_content;
	public GameObject go_space;
	public Text go_title;
	public Text go_cost;

	public Text month_text;

	public Text total_expence;
	public Text total_earn;
	public Text total_result;

	private string temp_title = "";
	private string temp_cost = "";

	private int earn_counter = 0;
	private int expence_counter = 0;

	private float earn_sum=0;
	private float expence_sum = 0;
	private float sum_result = 0;

	public string data;
	private bool earn_trigger=false;
	private bool expence_trigger = false;

	private string[] items;

	public System.DateTime selected_date=System.DateTime.Now;
	// Use this for initialization
	void Start () {
		Refresh ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Refresh(){
		
		temp_title = "";
		temp_cost = "";

		earn_counter = 0;
		expence_counter = 0;

		earn_sum=0;
		expence_sum = 0;
		sum_result = 0;
		data = "";
		month_text.text = selected_date.ToString ("MMMM")+" "+selected_date.ToString("yyyy");
		earn_trigger=false;
		expence_trigger = false;
		//saveload.save_data = "";
		//saveload.Save ();
		saveload.Load ();
		data = saveload.save_data;
		print (data);
		fetchData ();
	}

	public void addEarn(){
		
		temp_title = Earn_title.text;
		temp_cost = Earn_cost.text;

		float t = float.Parse (temp_cost);

		if(temp_title=="")
			temp_title="Extra Earn";
		if(temp_cost=="")
			temp_cost="0";

		earn_sum = earn_sum + t;
		expence_trigger = false;
		earn_trigger = true;
		addEarnEntry ();

	}

	void addEarnEntry()
	{
		

		earn_counter++;
		go_title.text = selected_date.ToString("dd")+". "+temp_title;
		go_cost.text = "+"+temp_cost;
		go_cost.color = Color.green;

		GameObject go = (GameObject)Instantiate (go_content) as GameObject;//instatiate content
		go.transform.SetParent(earning_entries_parent.transform,false);//setting  parrent to earning
		int temp = earning_entries.transform.childCount;//getting all child count of earning entry list

		temp = temp - 3;//for add aur input field ko last me rakhne ke liye
		go.transform.SetSiblingIndex (temp);//ab set kar diya

		//now instatiate another space game object for shifting all the content downwords
		go = Instantiate (go_space);
		go.transform.SetParent(addentries_parent.transform,false);

		go.transform.SetSiblingIndex (3);

		//clear input field
		Earn_title.Select();
		Earn_title.text = "";
		Earn_cost.Select();
		Earn_cost.text = "";

		if (earn_trigger)
		sum_update ();
	}

	public void addExpence(){
		
		temp_title = Expence_title.text;
		temp_cost = Expence_cost.text;
		float t = float.Parse (temp_cost);


		if(temp_title=="")
			temp_title="Extra Expenditure";
		if(temp_cost=="")
			temp_cost="0";

		expence_sum=expence_sum+t;
		expence_trigger = true;
		earn_trigger = false;
		addExpenceEntry ();

	}

	void addExpenceEntry()
	{
		

		expence_counter++;
		go_title.text = selected_date.ToString("dd")+". "+temp_title;
		go_cost.text = "-"+temp_cost;
		go_cost.color = Color.red;

		GameObject go = Instantiate (go_content);//instatiate content
		go.transform.SetParent(expence_entries_parent.transform,false);//setting  parrent to earning
		int temp = expence_entries_parent.transform.childCount;//getting all child count of earning entry list

		temp = temp - 3;//for add aur input field ko last me rakhne ke liye
		go.transform.SetSiblingIndex (temp);//ab set kar diya

		//clear input field
		Expence_title.Select();
		Expence_title.text = "";
		Expence_cost.Select();
		Expence_cost.text = "";


		//now instatiate another space game object for shifting all the content downwords
		go = Instantiate (go_space);
		go.transform.SetParent (addentries_parent.transform,false);
		temp = addentries.transform.childCount;
		temp = temp - 12;
		go.transform.SetSiblingIndex (temp);

		if(expence_trigger)
		sum_update ();
	}

	void sum_update()
	{
		total_earn.text = earn_sum.ToString ();
		total_expence.text = expence_sum.ToString ();
		sum_result =earn_sum-expence_sum;

		if (sum_result > 0) {
			total_result.text = "+"+sum_result.ToString ();
			total_result.color = Color.green;
		} else {
			total_result.text = "-"+sum_result.ToString ();
			total_result.color = Color.red;
		}
		savetofile ();
	}

	void savetofile()
	{
		saveload.Load ();
		data = saveload.save_data;
		if (data == ""||data==null) {
			Initialize_file ();

		} else {
			items = data.Split (';');
			int arraylength = items.Length;

			for (int i = 0; i < arraylength-1; i++) {
				
				System.DateTime dateTime = System.DateTime.Parse (GetDataValue (items [i], "DateTime:"));

				if (dateTime.ToString ("MM") == selected_date.ToString ("MM")) {
					items [i] = items [i].Replace ("|Stop:", "|");

					if (earn_trigger) {
						items [i] = items [i] + "Earn-Name" + earn_counter + ":" + temp_title + "|Earn-Cost" + earn_counter + ":" + temp_cost + "|";
						items [i] = items [i] + "Total_earn:" + earn_counter + "|";
					}
					if (expence_trigger) {
						items [i] = items [i] + "Expence-Name" + expence_counter + ":" + temp_title + "|Expence-Cost" + expence_counter + ":" + temp_cost + "|";
						items [i] = items [i] + "Total_Expence:" + expence_counter + "|";

					}
					items [i] = items [i] + "Stop:";
					merge_and_save_file ();


				} else {
					Initialize_file ();
					//print ("Time Difference is " + (selected_date - dateTime).Days);
					//print ("Selected Date" + selected_date);
				}
			}

		}

	}

	void merge_and_save_file()
	{
		int c = 0;
		data = "";
		print ("ItemsLength:"+items.Length);
		for (int i = 0; i < items.Length-1; i++) {
			data = data + items [i];
			data = data + ";";
		}

		//data = data + ";";
		saveload.save_data = data;
		saveload.Save ();
	}

	void fetchData()
	{
		if (data != null) {
			items = data.Split (';');
			int arraylength = items.Length;

			for (int i = 0; i < arraylength-1; i++) {
				
				System.DateTime dateTime = System.DateTime.Parse (GetDataValue (items [i], "DateTime:"));
				if (dateTime.ToString ("MM") == selected_date.ToString ("MM")) {
					
					int total_earn = Regex.Matches (items [i], "Total_earn:").Count;
					int total_expence = Regex.Matches (items [i], "Total_Expence:").Count;
					//print (total_earn + "E:Ex" + total_expence);
					for (int j = 0; j < total_earn; j++) {
						
						temp_title = GetDataValue (items [i], "Earn-Name" + (j + 1) + ":");
						temp_cost = GetDataValue (items [i], "Earn-Cost" + (j + 1) + ":");

						float t = float.Parse (temp_cost);

						if (temp_title == "")
							temp_title = "Extra Earn";
						if (temp_cost == "")
							temp_cost = "0";

						earn_sum = earn_sum + t;

						addEarnEntry ();
					}

					for (int j = 0; j < total_expence; j++) {
						temp_title = GetDataValue (items [i], "Expence-Name" + (j + 1) + ":");
						temp_cost = GetDataValue (items [i], "Expence-Cost" + (j + 1) + ":");

						float t = float.Parse (temp_cost);

						if (temp_title == "")
							temp_title = "Extra Expence";
						if (temp_cost == "")
							temp_cost = "0";

						expence_sum = expence_sum + t;

						addExpenceEntry ();
					}


				} else
					data = data + ";";
			}

		}
		Update_result ();
	}

	string GetDataValue(string data,string index){
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if (value.Contains ("|"))
			value = value.Remove (value.IndexOf ("|"));
		return value;
	}

	public void MainMenuButton()
	{
		SceneManager.LoadScene (0);
	}

	void Initialize_file()
	{
		data=data+"DateTime:"+selected_date+"|";
		if (earn_counter > 0) {
			data = data + "Earn-Name" + earn_counter + ":" + temp_title + "|Earn-Cost" + earn_counter + ":" + temp_cost + "|";
			data=data+"Total_earn:"+earn_counter+"|";
		}
		if (expence_counter > 0) {
			data = data + "Expence-Name" + expence_counter + ":" + temp_title + "|Expence-Cost" + expence_counter + ":" + temp_cost + "|";
			data = data + "Total_Expence:" + expence_counter +"|";
		}
		data = data + "Stop:;";
		saveload.save_data = data;
		saveload.Save ();
	}

	void Update_result()
	{
		total_earn.text = earn_sum.ToString ();
		total_expence.text = expence_sum.ToString ();
		sum_result =earn_sum-expence_sum;

		if (sum_result > 0) {
			total_result.text = "+"+sum_result.ToString ();
			total_result.color = Color.green;
		} else {
			total_result.text = "-"+sum_result.ToString ();
			total_result.color = Color.red;
		}
	}
}
