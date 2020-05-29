using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

public class savingdataondevice : MonoBehaviour {

	public static int id;
	public static string username;
	public static int highscore;
	public static float intervalSpeed;
	public static float cubespeed;


	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();
		data.ID = id;
		data.Username = username;
		data.Highscore = highscore;
		data.IntervalSpeed = intervalSpeed;
		data.CubeSpeed = cubespeed;
		bf.Serialize (file, data);
		file.Close ();
	}

	public static void Load()
	{

		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			id = data.ID;
			username = data.Username;
			highscore = data.Highscore;
			intervalSpeed = data.IntervalSpeed;
			cubespeed = data.CubeSpeed;

		} else
			print ("file dosent exist");
	}
}




[Serializable]
class PlayerData
{
	public int ID;
	public string Username;
	public int Highscore;
	public float IntervalSpeed;
	public float CubeSpeed;
}