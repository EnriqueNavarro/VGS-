using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
	Vector3 worldSize = new Vector3(4,0,4);
	Room[,] rooms;
	List<Vector3> takenPositions = new List<Vector3>();
	int gridSizeX, gridSizeZ; 
	public int numberOfRooms = 20;
	public GameObject roomWhiteObj;
	public Transform mapRoot;
	void Start () {
		if (numberOfRooms >= (worldSize.x * 2) * (worldSize.z * 2)){ // make sure we dont try to make more rooms than can fit in our grid
			numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.z * 2));
		}
		gridSizeX = Mathf.RoundToInt(worldSize.x); //note: these are half-extents
		gridSizeZ = Mathf.RoundToInt(worldSize.z);
		CreateRooms(); //lays out the actual map
		SetRoomDoors(); //assigns the doors where rooms would connect
		DrawMap(); //instantiates objects to make up a map
		GetComponent<SheetAssigner>().Assign(rooms); //passes room info to another script which handles generatating the level geometry
	}
	void CreateRooms(){
		//setup
		rooms = new Room[gridSizeX * 2,gridSizeZ * 2];
		rooms[gridSizeX,gridSizeZ] = new Room(Vector3.zero, 1);
		takenPositions.Insert(0,Vector3.zero);
		Vector3 checkPos = Vector3.zero;
		//magic numbers
		float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
		//add rooms
		for (int i =0; i < numberOfRooms -1; i++){
			float randomPerc = ((float) i) / (((float)numberOfRooms - 1));
			randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
			//grab new position
			checkPos = NewPosition();
			//test new position
			if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare){
				int iterations = 0;
				do{
					checkPos = SelectiveNewPosition();
					iterations++;
				}while(NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
				if (iterations >= 50)
					print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
			}
			//finalize position
			rooms[(int) checkPos.x + gridSizeX, (int) checkPos.z + gridSizeZ] = new Room(checkPos,0);
			takenPositions.Insert(0,checkPos);
		}	
	}
	Vector3 NewPosition(){
		int x = 0, z = 0;
		Vector3 checkingPos = Vector3.zero;
		do{
			int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
			x = (int) takenPositions[index].x;//capture its x, y position
			z = (int) takenPositions[index].z;
			bool UpDown = (Random.value < 0.5f);//randomly pick wether to look on hor or vert axis
			bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
			if (UpDown){ //find the position bnased on the above bools
				if (positive){
					z += 1;
				}else{
					z -= 1;
				}
			}else{
				if (positive){
					x += 1;
				}else{
					x -= 1;
				}
			}
			checkingPos = new Vector3(x,0,z);
		}while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || z >= gridSizeZ || z < -gridSizeZ); //make sure the position is valid
		return checkingPos;
	}
	Vector3 SelectiveNewPosition(){ // method differs from the above in the two commented ways
		int index = 0, inc = 0;
		int x =0, z =0;
		Vector3 checkingPos = Vector3.zero;
		do{
			inc = 0;
			do{ 
				//instead of getting a room to find an adject empty space, we start with one that only 
				//as one neighbor. This will make it more likely that it returns a room that branches out
				index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
				inc ++;
			}while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
			x = (int) takenPositions[index].x;
			z = (int) takenPositions[index].z;
			bool UpDown = (Random.value < 0.5f);
			bool positive = (Random.value < 0.5f);
			if (UpDown){
				if (positive){
					z += 1;
				}else{
					z -= 1;
				}
			}else{
				if (positive){
					x += 1;
				}else{
					x -= 1;
				}
			}
			checkingPos = new Vector3(x,0,z);
		}while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || z >= gridSizeZ || z < -gridSizeZ);
		if (inc >= 100){ // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
			print("Error: could not find position with only one neighbor");
		}
		return checkingPos;
	}
	int NumberOfNeighbors(Vector3 checkingPos, List<Vector3> usedPositions){
		int ret = 0; // start at zero, add 1 for each side there is already a room
		if (usedPositions.Contains(checkingPos + Vector3.right)){ //using Vector.[direction] as short hands, for simplicity
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector3.left)){
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector3.forward)){
			ret++;
		}
		if (usedPositions.Contains(checkingPos + Vector3.back)){
			ret++;
		}
		return ret;
	}
	void DrawMap(){
		foreach (Room room in rooms){
			if (room == null){
				continue; //skip where there is no room
			}
			Vector3 drawPos = room.gridPos;
			drawPos.x *= 16;//aspect ratio of map sprite
			drawPos.z *= 8;
			//create map obj and assign its variables
			MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.Euler(90,0,0)).GetComponent<MapSpriteSelector>();
			mapper.type = room.type;
			mapper.up = room.doorTop;
			mapper.down = room.doorBot;
			mapper.right = room.doorRight;
			mapper.left = room.doorLeft;
			mapper.gameObject.transform.parent = mapRoot;
		}
	}
	void SetRoomDoors(){
		for (int x = 0; x < ((gridSizeX * 2)); x++){
			for (int z = 0; z < ((gridSizeZ * 2)); z++){
				if (rooms[x,z] == null){
					continue;
				}
				//Vector3 gridPosition = new Vector3(x,0,z);
				if (z - 1 < 0){ //check above
					rooms[x,z].doorBot = false;
				}else{
					rooms[x,z].doorBot = (rooms[x,z-1] != null);
				}
				if (z + 1 >= gridSizeZ * 2){ //check bellow
					rooms[x,z].doorTop = false;
				}else{
					rooms[x,z].doorTop = (rooms[x,z+1] != null);
				}
				if (x - 1 < 0){ //check left
					rooms[x,z].doorLeft = false;
				}else{
					rooms[x,z].doorLeft = (rooms[x - 1,z] != null);
				}
				if (x + 1 >= gridSizeX * 2){ //check right
					rooms[x,z].doorRight = false;
				}else{
					rooms[x,z].doorRight = (rooms[x+1,z] != null);
				}
			}
		}
	}
}
