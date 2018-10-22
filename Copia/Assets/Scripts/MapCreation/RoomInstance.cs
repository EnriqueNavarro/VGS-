using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour {
	public Texture2D tex;
	[HideInInspector]
	public Vector2 gridPos;
	public int type; // 0: normal, 1: enter, 2:location, 3:boss
	[HideInInspector]
	public bool doorTop, doorBot, doorLeft, doorRight;
	[SerializeField]
	GameObject doorU, doorD, doorL, doorR, doorWall;
	[SerializeField]
	ColorToGameObject[] mappings;
	float tileSize = 16;
	Vector3 roomSizeInTiles = new Vector3(9,0,17);
	public void Setup(Texture2D _tex, Vector3 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight){
		tex = _tex;
		gridPos = _gridPos;
		type = _type;
		doorTop = _doorTop;
		doorBot = _doorBot;
		doorLeft = _doorLeft;
		doorRight = _doorRight;
		MakeDoors();
		GenerateRoomTiles();
	}
	void MakeDoors(){
		//top door, get position then spawn
		Vector3 spawnPos = transform.position + Vector3.forward*(roomSizeInTiles.z/4 * tileSize) - Vector3.forward*(tileSize/4);
		PlaceDoor(spawnPos, doorTop, doorU);
		//bottom door
		spawnPos = transform.position + Vector3.back*(roomSizeInTiles.z/4 * tileSize) - Vector3.back*(tileSize/4);
		PlaceDoor(spawnPos, doorBot, doorD);
		//right door
		spawnPos = transform.position + Vector3.right*(roomSizeInTiles.x * tileSize) - Vector3.right*(tileSize);
		PlaceDoor(spawnPos, doorRight, doorR);
		//left door
		spawnPos = transform.position + Vector3.left*(roomSizeInTiles.x * tileSize) - Vector3.left*(tileSize);
		PlaceDoor(spawnPos, doorLeft, doorL);
	}
	void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn){
		// check whether its a door or wall, then spawn
		if (door){
			Instantiate(doorSpawn, spawnPos, Quaternion.Euler(90,0,0)).transform.parent = transform;
		}else{
			Instantiate(doorWall, spawnPos, Quaternion.Euler(90,0,0)).transform.parent = transform;
		}
	}
	void GenerateRoomTiles(){
		//loop through every pixel of the texture
		for(int x = 0; x < tex.width; x++){
			for (int z = 0; z < tex.height; z++){
				GenerateTile(x,z);
			}
		}
	}
	void GenerateTile(int x, int z){
		Color pixelColor = tex.GetPixel(x,z);
		//skip clear spaces in texture
		if (pixelColor.a == 0){
			return;
		}
		//find the color to math the pixel
		foreach (ColorToGameObject mapping in mappings){
			if (mapping.color.Equals(pixelColor)){
				Vector3 spawnPos = positionFromTileGrid(x,z);
				Instantiate(mapping.prefab, spawnPos, Quaternion.Euler(90,0,0)).transform.parent = this.transform;
			}else{
				//forgot to remove the old print for the tutorial lol so I'll leave it here too
				//print(mapping.color + ", " + pixelColor);
			}
		}
	}
	Vector3 positionFromTileGrid(int x, int z){
		Vector3 ret;
		//find difference between the corner of the texture and the center of this object
		Vector3 offset = new Vector3((-roomSizeInTiles.x + 1)*tileSize, 0, (roomSizeInTiles.z/4)*tileSize - (tileSize/4));
		//find scaled up position at the offset
		ret = new Vector3(tileSize * (float) x, 0, -tileSize * (float) z) + offset + transform.position;
		return ret;
	}
}
