using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MinimapScriptNewMode : MonoBehaviour {
	
	public GUIStyle miniMap;
	public GUIStyle enemyIcon;
	public GUIStyle playerIcon;
	public Transform player;
	
	public GameObject track;
	public List<GameObject> IAs;
	public List<Transform> enemies;
	private Transform kart_transform;
	//Offset variables (X and Y) - where you want to place your map on screen.
	public float  mapOffSetX= 762f;
	public float  mapOffSetY= 510f;
	//The width and height of your map as it'll appear on screen,
	public float  mapWidth= 200f;
	public float  mapHeight= 200f;
	//Width and Height of your scene, or the resolution of your terrain.
	public float  sceneWidth = 150f;
	public float  sceneHeight = 150f;
	//The size of your player's and enemy's icon on the map. 
	public float iconSize= 10;
	private float iconHalfSize;
	
	// Use this for initialization
	void Start () {
		
		int j = 0;
		for(int i = 0; i < IAs.Count; i++)
		{
			if	(IAs[i].activeSelf)
			{
				enemies[j] = IAs[i].GetComponent<Transform>();
				j++;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		iconHalfSize = iconSize/2;
		
		
	}
	
	float  GetMapPos ( float pos ,   float mapSize  ,   float sceneSize   ){
		return (float)(pos * mapSize /sceneSize);
	}
	
	void  OnGUI (){
		//Everything about the map.
		//Calculations to set up the correct nombers
		//Important: In the race, the kart uses (x,y) coordinates. 
		// The GUI uses (x,z).
		//And, also, the Z changed of sign
		GUI.BeginGroup( new Rect(mapOffSetX,mapOffSetY,mapWidth,mapHeight), miniMap);
		float pX= GetMapPos(transform.position.x, mapWidth, sceneWidth);
		float pZ= GetMapPos(transform.position.z, mapHeight, sceneHeight);
		float playerMapX= pX - iconHalfSize + mapWidth/2;
		float playerMapZ = ((pZ) - iconHalfSize + mapHeight/1.9f);
		GUI.Box( new Rect(playerMapZ , playerMapX, iconSize, iconSize), "", playerIcon);
		
		float sX = GetMapPos(enemies[0].position.x, mapWidth, sceneWidth);
		float sZ = GetMapPos(enemies[0].position.z, mapHeight, sceneHeight);
		float enemyMapX = sX - iconHalfSize + mapWidth/2;
		float enemyMapZ = ((sZ) - iconHalfSize + mapHeight / 1.9f);
		GUI.Box(new Rect(enemyMapZ, enemyMapX, iconSize, iconSize), "", enemyIcon);
		
		float s1X = GetMapPos(enemies[1].position.x, mapWidth, sceneWidth);
		float s1Z = GetMapPos(enemies[1].position.z, mapHeight, sceneHeight);
		float enemyMap1X = s1X - iconHalfSize + mapWidth/2;
		float enemyMap1Z = ((s1Z) - iconHalfSize + mapHeight / 1.9f);
		GUI.Box(new Rect(enemyMap1Z, enemyMap1X, iconSize, iconSize), "", enemyIcon);
		
		float s2X = GetMapPos(enemies[2].position.x, mapWidth, sceneWidth);
		float s2Z = GetMapPos(enemies[2].position.z, mapHeight, sceneHeight);
		float enemyMap2X = s2X - iconHalfSize + mapWidth/2;
		float enemyMap2Z = ((s2Z) - iconHalfSize + mapHeight / 1.9f);
		GUI.Box(new Rect(enemyMap2Z, enemyMap2X, iconSize, iconSize), "", enemyIcon);
		
		float s3X = GetMapPos(enemies[3].position.x, mapWidth, sceneWidth);
		float s3Z = GetMapPos(enemies[3].position.z, mapHeight, sceneHeight);
		float enemyMap3X = s3X - iconHalfSize + mapWidth/2;
		float enemyMap3Z = ((s3Z) - iconHalfSize + mapHeight / 1.9f);
		GUI.Box(new Rect(enemyMap3Z, enemyMap3X, iconSize, iconSize), "", enemyIcon);
		
		float s4X = GetMapPos(enemies[4].position.x, mapWidth, sceneWidth);
		float s4Z = GetMapPos(enemies[4].position.z, mapHeight, sceneHeight);
		float enemyMap4X = s4X - iconHalfSize + mapWidth/2;
		float enemyMap4Z = ((s4Z) - iconHalfSize + mapHeight / 1.9f);
		GUI.Box(new Rect(enemyMap4Z, enemyMap4X, iconSize, iconSize), "", enemyIcon);

		/*
		float s5X = GetMapPos(enemies[5].position.x, mapWidth, sceneWidth);
		float s5Z = GetMapPos(enemies[5].position.z, mapHeight, sceneHeight);
		float enemyMap5X = s5X - iconHalfSize + mapWidth/2;
		float enemyMap5Z = ((s5Z) - iconHalfSize + mapHeight / 1.9f);
		GUI.Box(new Rect(enemyMap5Z, enemyMap5X, iconSize, iconSize), "", enemyIcon);
		
		float s6X = GetMapPos(enemies[6].position.x, mapWidth, sceneWidth);
		float s6Z = GetMapPos(enemies[6].position.z, mapHeight, sceneHeight);
		float enemyMap6X = s6X - iconHalfSize + mapWidth/2;
		float enemyMap6Z = ((s6Z) - iconHalfSize + mapHeight / 1.9f);
		GUI.Box(new Rect(enemyMap6Z, enemyMap6X, iconSize, iconSize), "", enemyIcon);
		*/
		
		
		GUI.EndGroup();
	}
}
