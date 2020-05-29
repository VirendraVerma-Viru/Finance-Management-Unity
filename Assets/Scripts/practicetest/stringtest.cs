using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class stringtest : MonoBehaviour {

	string a="hello dogdog";

	// Use this for initialization
	void Start () {
		print (System.DateTime.Now);
		print(System.DateTime.Now.ToString ("MM"));
		print (System.DateTime.Now.AddMonths (1));
		//test ();
		int count = Regex.Matches(a, "dog").Count;
		//print (a.Replace("dog","cat"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void test(){
		
		string s="Hello";
		string a="World";
		string b = "";
		for (int i = 0; i < 99999; i++) {
			b=b+""+i.ToString();
		}
		string str = s + "|" + a+b;
		print (str);
	}

}
