using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour {

	public string[] items;

	// Use this for initialization
	void Start () {
		StartCoroutine (GetData());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator GetData(){
		WWW itemsData = new WWW ("https://appsdatav.000webhostapp.com/FinanceManagement/testprint.php");
		yield return itemsData;
		print (itemsData.text);
		string itemsDataString = itemsData.text;
		print (itemsDataString);
		items = itemsDataString.Split (';');
		print (GetDataValue (items [0], "Name:"));
	}

	public void CreateUser(string username,string password,string email){
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		form.AddField ("passwordPost", password);
		form.AddField ("emailPost", email);


		WWW www = new WWW ("url", form);
	}

	string GetDataValue(string data,string index){
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if (value.Contains ("|"))
			value = value.Remove (value.IndexOf ("|"));
		return value;
	}
}
