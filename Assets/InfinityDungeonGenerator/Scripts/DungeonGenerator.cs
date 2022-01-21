using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {
	public GameObject[] walls;
	//Ground
	public GameObject[] ground;
	
	//
	public GameObject[] environmentToWall;
	public GameObject[] enemies;
	public GameObject[] lamps;
	public GameObject[] columns;
	public GameObject[] furniture;
	public GameObject[] roofs;
	public GameObject player ;
	public int transformY  = 0;
	public int groundTransZ = 0;
	public int groundTransX = 0;
	private bool canCreateDungeon = true;
	public List<Vector3> tunelVector3Saver;
	// Use this for initialization
	private int entranceDoorPlaceI = -1;
	private int entranceDoorPlaceJ = -1;
	private int entranceDoorPlace = -1;
	private int pastRoomSizeI = 0;
	private int pastRoomSizeJ = 0;
	public List<GameObject> objectsToClean;
	
	public List<GameObject> objectsToCleanDungeon;//normal not infinity dungeon
	
	private List<Tunnel> tunrelsToClean = new List<Tunnel>();
	private List<Tunnel> environmentToClean = new List<Tunnel>();
//	public Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
	private int generatedDungeonNumber = 0;
	
	public int roomSizeIlow = 4;
	public int roomSizeImax = 10;
	public int roomSizeJlow = 4;
	public int roomSizeJmax = 10;
	public int maxDungeonSize = 15;
	
	public int enemyRange = 10; // 1/10 chance to spawn enemy for available floor position. if enemy Range is for example equals 2, than there are 1/2 chance that floor has an enemy  

	public int furnitureRange = 2;
	public int lampsRange = 2;
	public int wallEnvRange = 2;
	public int columnRange = 2;


	public bool hasRoof = false;
	public float playerSpawnPosY = 1;
	public float playerSpawnPosX = 1;
	public float playerSpawnPosZ = 1;

	public float sizeOfFloat = 4;
	public int sizeOfInt = 4;
	public void cleanDungeons()
	{
		if (objectsToCleanDungeon.Count > 0)
		{
			for (int i = 0; i < objectsToCleanDungeon.Count; i++)
			{
				DestroyImmediate(objectsToCleanDungeon[i]);
			}

			generatedDungeonNumber = 0;
			tunelVector3Saver.Clear();
//			objectsToCleanDungeon.RemoveRange(0,objectsToCleanDungeon.Count); 
			objectsToCleanDungeon.Clear();
			groundTransX = 0;
			groundTransZ = 0;
		}
	}	

	// Update is called once per frame
	
	
	bool SetRangeChance(int range){
		return Random.Range(0,range) == 0;	
	}

	bool isDoorPlaceXorZ(){
		return Random.Range(0,2)==0;
	}

	public void GenBrokenRectangleDungeon(){
	int roomSize = Random.Range(roomSizeIlow,roomSizeImax);
	int doorPlace =  Random.Range(0,roomSize);
	if(doorPlace==roomSize){
		doorPlace=doorPlace-1;
	}
	int doorPlaceX  =  Random.Range(0,roomSize);	
	if(doorPlaceX==roomSize){
		doorPlaceX=doorPlaceX-1;
	}
	int oldDoorPlace = 0;
	int groundTransforamZ  = groundTransZ;
	int grTrZ  = groundTransX;
	int dungeonSizeRight = Random.Range(5,maxDungeonSize);
	int dungeonSize = Random.Range(5,maxDungeonSize);
	int dungeonSizeCounter  = 0;
	while(dungeonSizeCounter<dungeonSize){
		for(int i = 0; i<roomSize;i++){
			for(int j = 0; j<roomSize;j++){
				if (ground.Length > 0)
				{
					GameObject toClean = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(0 + (j * sizeOfInt) + grTrZ, transformY, groundTransforamZ + (i * sizeOfInt)),
						Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean);
				}

				if (hasRoof)
					{
						GameObject toClean31 = Instantiate(roofs[Random.Range(0,roofs.Length)],new Vector3(0+(j*sizeOfInt+grTrZ),transformY+sizeOfInt,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean31);
					}
					if(dungeonSizeCounter>=2&&SetRangeChance(enemyRange)&&enemies.Length>0){
						GameObject toClean11 =	Instantiate(enemies[Random.Range(0,enemies.Length)],new Vector3(0+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean11);
					}
					
					if(j==0){ 	
						GameObject toClean1 = Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(-2+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
						objectsToCleanDungeon.Add(toClean1);
					} 
					
					if(j==roomSize-1&&i!=doorPlaceX){ 	
						GameObject toClean1 =Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(2+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
						objectsToCleanDungeon.Add(toClean1);
					}
					
					if(dungeonSizeCounter!=0){
						if(i==0&&j!=oldDoorPlace){
							GameObject toClean1 =Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(0+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean1);
						}
					}
					else{
						if(i==0){
							GameObject toClean1 =Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(0+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean1);
						}
					}
									
					if(i==roomSize-1&&j!=doorPlace){
						GameObject toClean1 =Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(0+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
						objectsToCleanDungeon.Add(toClean1);
					}	
					
					//environmentToWalls
					if(SetRangeChance(wallEnvRange)&&environmentToWall.Length>0){
						if(j==0&&i!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-1.4f+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(j==roomSize-1&&i!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(1.4f+(j*sizeOfFloat)+grTrZ,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,transformY,-1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSize-1&&j!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,transformY,1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
					if(SetRangeChance(lampsRange)&&lamps.Length>0){
						if(j==0&&i!=oldDoorPlace&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-1.9f+(j*sizeOfInt)+grTrZ,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(j==roomSize-1&&i!=doorPlace&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(1.9f+(j*sizeOfInt)+grTrZ,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,-90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlace&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,1.7f+transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,0, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSize-1&&j!=doorPlace&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,1.7f+transformY,1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
					
					//Columns
					if(SetRangeChance(columnRange)&&columns.Length>0){
						if(j==0&&i!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						
						else if(j==roomSize-1&&i!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(2f+(j*sizeOfFloat)+grTrZ,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+grTrZ,transformY,-2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSize-1&&j!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+grTrZ,transformY,2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//furniture
					if(SetRangeChance(furnitureRange)&&furniture.Length>0){
						if(j!=0&&i!=0&&i!=roomSize-1&&j!=roomSize){
							GameObject toClean2 = Instantiate(furniture[Random.Range(0,furniture.Length)],new Vector3((j*sizeOfFloat)+grTrZ,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.identity);
							objectsToCleanDungeon.Add(toClean2);
						}
					}
			}
		}
		
		if(dungeonSizeCounter==0||dungeonSizeCounter==dungeonSize-1){
			int tunelSizeX =  Random.Range(1,10);
			for(int m = 0; m<tunelSizeX;m++){
				if (ground.Length > 0)
				{
					GameObject toClean1 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3((roomSize * sizeOfInt) + (m * sizeOfInt) + grTrZ, transformY,
							(doorPlaceX * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean1);
				}

				if (hasRoof)
				{
					GameObject toClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3((roomSize * sizeOfInt) + (m * sizeOfInt)+grTrZ, transformY+sizeOfInt, (doorPlaceX * sizeOfInt) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean31);
				}

				GameObject toClean2 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSize*sizeOfInt)+(m*sizeOfInt)+grTrZ,transformY,-2+(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
				GameObject toClean3 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSize*sizeOfInt)+(m*sizeOfInt)+grTrZ,transformY,2+(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
				objectsToCleanDungeon.Add(toClean2);
				objectsToCleanDungeon.Add(toClean3);
			}
			var groundTransforamX = tunelSizeX*sizeOfInt+grTrZ;
			int dungeonSizeCounterRight = 0;
				
			while(dungeonSizeCounterRight<dungeonSizeRight){
					for(int s = 0; s<roomSize;s++){
					for(int v = 0; v<roomSize;v++){
						if (ground.Length > 0)
						{
							GameObject toClean1 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3((roomSize * sizeOfInt) + (groundTransforamX) + (v * sizeOfInt), transformY,
									groundTransforamZ + (s * sizeOfInt)), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toClean1);
						}

						if (hasRoof)
							{
								GameObject toClean31= Instantiate(roofs[Random.Range(0,roofs.Length)],new Vector3((roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY+sizeOfInt,groundTransforamZ+(s*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
								objectsToCleanDungeon.Add(toClean31);
							}

							if(dungeonSizeCounter>=2&&SetRangeChance(enemyRange)&&enemies.Length>0){
								GameObject toClean11 =	Instantiate(enemies[Random.Range(0,enemies.Length)],new Vector3((roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ+(s*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
								objectsToCleanDungeon.Add(toClean11);
							}
							
							if(v==0&&s!=doorPlaceX){ 	
								GameObject toClean2= Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ+(s*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClean2);
							} 
							
							
							if(v==roomSize-1&&s!=doorPlaceX){ 	
								GameObject toClean2=	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ+(s*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClean2);
							}
							
							if(dungeonSizeCounterRight!=0){
								if(s==0){
									GameObject toClean2=	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ-2+(s*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
									objectsToCleanDungeon.Add(toClean2);
								}
							}
							else{
								if(s==0){
									GameObject toClean2=	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ-2+(s*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
									objectsToCleanDungeon.Add(toClean2);
								}
							}
											
							if(s==roomSize-1){
								GameObject toClean2=	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ+2+(s*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClean2);
							}	
							
									//environmentToWalls
					if(SetRangeChance(wallEnvRange)&&environmentToWall.Length>0){
						if(v==0&&s!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-1.4f+(roomSize*sizeOfInt)+(v*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(s*sizeOfInt)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(v==roomSize-1&&s!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(1.4f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==0&&v!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,-1.4f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==roomSize-1&&v!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,1.4f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
					if(SetRangeChance(lampsRange)&&lamps.Length>0){
						if(v==0&&s!=oldDoorPlace&&s%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-1.9f+(roomSize*sizeOfInt)+(v*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(v==roomSize-1&&s!=doorPlace&&s%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(1.9f+(roomSize*sizeOfInt)+(v*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,-90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==0&&v!=oldDoorPlace&&v%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,1.7f+transformY,-1.9f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,0, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==roomSize-1&&v!=doorPlace&&v%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,1.7f+transformY,1.9f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//Columns
					if(SetRangeChance(columnRange)&&columns.Length>0){
						if(v==0&&s!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						
						else if(v==roomSize-1&&s!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==0&&v!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,-2f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==roomSize-1&&v!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,2f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//furniture
					if(SetRangeChance(furnitureRange)&&furniture.Length>0){
						if(v!=0&&s!=0&&s!=roomSize-1&&v!=roomSize-1){
							GameObject toClean2 = Instantiate(furniture[Random.Range(0,furniture.Length)],new Vector3((roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(s*sizeOfFloat)) , Quaternion.identity);
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					}
				}
				
					int tunelSizeXTun =  Random.Range(1,10);
				
				if(dungeonSizeCounterRight!=dungeonSizeRight-1){
					for(int p = 0; p<tunelSizeXTun;p++){
						if (ground.Length > 0)
						{
							GameObject toClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3((roomSize * sizeOfInt) * 2 + (p * sizeOfInt) + groundTransforamX,
									transformY, (doorPlaceX * sizeOfInt) + groundTransforamZ),
								Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toClean3);
						}

						if (hasRoof)
						{
							GameObject toClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
								new Vector3((roomSize * sizeOfInt) * 2 + (p * sizeOfInt) + groundTransforamX, transformY+sizeOfInt,
									(doorPlaceX * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toClean31);
						}

						GameObject toCleansizeOfInt=Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSize*sizeOfInt)*2+(p*sizeOfInt)+groundTransforamX,transformY,-2+(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						GameObject toClean5=Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSize*sizeOfInt)*2+(p*sizeOfInt)+groundTransforamX,transformY,2+(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						
						objectsToCleanDungeon.Add(toCleansizeOfInt);
						objectsToCleanDungeon.Add(toClean5);
					}
					
					groundTransforamX=(roomSize*sizeOfInt)+(tunelSizeXTun*sizeOfInt)+groundTransforamX;
				}
				else{
					GameObject toClean3=Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(roomSize*sizeOfInt)*2+groundTransforamX,transformY,(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
					objectsToCleanDungeon.Add(toClean3);
				}
				
				dungeonSizeCounterRight=dungeonSizeCounterRight+1;
			}	
		}
		int tunelSize =  Random.Range(1,6);
		if(dungeonSizeCounter!=dungeonSize-1){
			
			for(int n = 0; n<tunelSize;n++){
				if (ground.Length > 0)
				{
					GameObject toClean6 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(doorPlace * sizeOfInt + grTrZ, transformY,
							(roomSize * sizeOfInt) + (n * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean6);
				}

				if (hasRoof)
				{
					GameObject toClean61 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3(doorPlace * sizeOfInt+grTrZ, transformY+sizeOfInt, (roomSize * sizeOfInt) + (n * sizeOfInt) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean61);
				}

				GameObject toClean7=Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(doorPlace*sizeOfInt)+grTrZ,transformY,(roomSize*sizeOfInt)+(n*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				GameObject toClean8=Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(doorPlace*sizeOfInt)+grTrZ,transformY,(roomSize*sizeOfInt)+(n*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				
				objectsToCleanDungeon.Add(toClean7);
				objectsToCleanDungeon.Add(toClean8);
			}
		}
		else{
			GameObject toClean6=Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(doorPlace*sizeOfInt+grTrZ,transformY,-2+(roomSize*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
			objectsToCleanDungeon.Add(toClean6);
		}
		
		groundTransforamZ=(roomSize*sizeOfInt)+(tunelSize*sizeOfInt)+groundTransforamZ;
		oldDoorPlace=doorPlace;
		roomSize = Random.Range(sizeOfInt,9);			
		while(roomSize<=oldDoorPlace){
			roomSize = Random.Range(sizeOfInt,9);
		}		
		doorPlace =  Random.Range(0,roomSize);	
		if(doorPlace==roomSize){
			doorPlace=doorPlace-1;
		}
		
		dungeonSizeCounter=dungeonSizeCounter+1;
	}
	player.transform.position=new Vector3(playerSpawnPosX,playerSpawnPosY,playerSpawnPosZ);	
}

	public void GenProgressionDungeon(int groundTransforamX, int groundTransforamZ, int entranceDoorPlaceI, int entranceDoorPlaceJ, int entranceDoorPlace, int pastRSizeI, int pastRSizeJ, int lockedSide)
	{
		if (generatedDungeonNumber==1)
		{
			lockedSide = entranceDoorPlace;
		}
		
		
		int roomSizeI = roomSizeIlow+generatedDungeonNumber<roomSizeImax?roomSizeIlow+generatedDungeonNumber:roomSizeImax;
		
		int roomSizeJ = roomSizeJlow+generatedDungeonNumber<roomSizeJmax?roomSizeJlow+generatedDungeonNumber:roomSizeJmax;
		int doorPlaceI =  Random.Range(0,roomSizeJ);
		if(doorPlaceI==roomSizeI){
			doorPlaceI=doorPlaceI-1;
		}	
		int dootPlaceJ =  Random.Range(0,roomSizeI);
		if(dootPlaceJ==roomSizeJ){
			dootPlaceJ=dootPlaceJ-1;
		}
		
		if (entranceDoorPlace == 2)	
		{
			if (entranceDoorPlaceJ >= roomSizeI && entranceDoorPlaceJ>=0)
			{
				groundTransforamZ =sizeOfInt+ groundTransforamZ + (entranceDoorPlaceJ-roomSizeI) * sizeOfInt;
				entranceDoorPlaceJ = roomSizeI-1;
			}

			if (pastRSizeJ > roomSizeJ||pastRSizeJ < roomSizeJ)
			{
				groundTransforamX = groundTransforamX - (roomSizeJ-pastRSizeJ) * sizeOfInt; //+++++++++++
			}
			
		}		
		else if (entranceDoorPlace == 3)
		{
			if (pastRSizeI > roomSizeI||pastRSizeI < roomSizeI)
			{
				groundTransforamZ = groundTransforamZ - (roomSizeI-pastRSizeI) * sizeOfInt; //+++++++++++++
			}

			if (entranceDoorPlaceI >= roomSizeJ && entranceDoorPlaceI>=0)
			{
				groundTransforamX =sizeOfInt+ groundTransforamX + (entranceDoorPlaceI-roomSizeJ) * sizeOfInt;
				entranceDoorPlaceI = roomSizeJ-1;
			}
			
		}
		else
		{
			if (entranceDoorPlace == 0)
			{
				if (entranceDoorPlaceJ >= roomSizeI && entranceDoorPlaceJ>=0)
				{
					groundTransforamZ =sizeOfInt+ groundTransforamZ + (entranceDoorPlaceJ-roomSizeI) * sizeOfInt;
					entranceDoorPlaceJ = roomSizeI-1;
				}
			}
		
			if (entranceDoorPlace == 1)
			{
				if (entranceDoorPlaceI >= roomSizeJ && entranceDoorPlaceI>=0)
				{
					groundTransforamX =sizeOfInt+ groundTransforamX + (entranceDoorPlaceI - roomSizeJ) * sizeOfInt;
					entranceDoorPlaceI = roomSizeJ-1;
				}
			}
		}
				
		int oldDoorPlaceI = 0;
		int oldDoorPlaceJ = 0;
		int nextDoorSide = Random.Range(0, sizeOfInt); // 0 - West, 1 South, 2 Ost, 3 North
		while (entranceDoorPlace == nextDoorSide || nextDoorSide==lockedSide)
		{
			 nextDoorSide = Random.Range(0, sizeOfInt); // 0 - West, 1 South, 2 Ost, 3 North
		}
//		int nextDoorSide = 2; // 0 - West, 1 South, 2 Ost, 3 North
		for(int i = 0; i<roomSizeI;i++){
				for(int j = 0; j<roomSizeJ;j++){
					if (ground.Length > 0)
					{
						GameObject toClean = Instantiate(ground[Random.Range(0, ground.Length)],
							new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
								groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean);
					}

					if (hasRoof)
					{
						GameObject toClean31 = Instantiate(roofs[Random.Range(0,roofs.Length)], new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY+sizeOfInt,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean31);
					}

					//Enemy Spawner
					if(generatedDungeonNumber>=2&&SetRangeChance(enemyRange)&&enemies.Length>0){
						GameObject toClean22 =	Instantiate(enemies[Random.Range(0,enemies.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean22);
					}
					if (entranceDoorPlace == -1)
					{
						if (nextDoorSide==0)
					{
						if(j==0&&i!=dootPlaceJ){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==roomSizeI-1){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==0){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(j==roomSizeJ-1){ 	
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
					}
					else if (nextDoorSide==1)
					{
						if(j==0){ 	
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==roomSizeI-1){
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==0&&j!=doorPlaceI){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(j==roomSizeJ-1){ 	
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
					}
					else if (nextDoorSide==2)
					{
						if(j==0){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==roomSizeI-1){
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==0){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(j==roomSizeJ-1&&i!=dootPlaceJ){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
					}
					else if(nextDoorSide==3)
					{
						if(j==0){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==roomSizeI-1&&j!=doorPlaceI){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==0){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(j==roomSizeJ-1){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
					}
							
					}
					else
					{
						if (nextDoorSide == 0 && entranceDoorPlace == 2)
						{
							if (j == 0 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 0 && entranceDoorPlace == 3)
						{
							if (j == 0 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 0 && entranceDoorPlace == 1)
						{
							if (j == 0 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 1 && entranceDoorPlace == 3)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 1 && entranceDoorPlace == 2)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 1 && entranceDoorPlace == 0)
						{
							if (j == 0 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 2 && entranceDoorPlace == 0)
						{
							if (j == 0 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 2 && entranceDoorPlace == 1)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 2 && entranceDoorPlace == 3)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 3 && entranceDoorPlace == 1)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 3 && entranceDoorPlace == 2)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 3 && entranceDoorPlace == 0)
						{
							if (j == 0 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
					}

	//environmentToWalls
//					if(SetRangeChance(furnitureRange)){
//						if(j==0&&i!=entranceDoorPlaceJ&&i%3==0){
//							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-1.9f+(j*sizeOfInt)+groundTransforamX,1.7f,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(j==0&&i!=entranceDoorPlaceJ){
//							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-1.4f+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0,90, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(j==roomSizeJ-1&&i!=dootPlaceJ&&i%3==0){
//							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(1.9f+(j*sizeOfInt)+groundTransforamX,1.7f,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,-90, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(j==roomSizeJ-1&&i!=dootPlaceJ){
//							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(1.4f+(j*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(i==0&&j!=entranceDoorPlaceI&&j%3==0){
//							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,0, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(i==0&&j!=entranceDoorPlaceI){
//							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(i==roomSizeI-1&&j!=doorPlaceI&&j%3==0){
//							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f,1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(i==roomSizeI-1&&j!=doorPlaceI){
//							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//					}
					//Columns
					if(SetRangeChance(columnRange)&&columns.Length>0){
						if(j==0&&i!=entranceDoorPlaceJ){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						
						else if(j==roomSizeJ-1&&i!=dootPlaceJ){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=entranceDoorPlaceI){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+groundTransforamX,transformY,-2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSizeI-1&&j!=doorPlaceI){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+groundTransforamX,transformY,2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//furniture
					if(SetRangeChance(furnitureRange)&&furniture.Length>0){
						if(j!=0&&i!=0&&i!=roomSizeI-1&&j!=roomSizeJ-1){
							GameObject toClean2 = Instantiate(furniture[Random.Range(0,furniture.Length)],new Vector3((j*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.identity);
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
				}
			}
		Debug.Log("Mid"+generatedDungeonNumber);
		if(generatedDungeonNumber<maxDungeonSize-1){
		if(nextDoorSide==0){
			int tunelSizeX =  Random.Range(3,8);
			for(int m = 0; m<tunelSizeX;m++){
				if (columns.Length > 0)
				{
					GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-5.9f - (m * sizeOfInt) + groundTransforamX, transformY,
							-2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
					GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-5.9f - (m * sizeOfInt) + groundTransforamX, transformY,
							2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
					objectsToCleanDungeon.Add(tunelToClean1);
					objectsToCleanDungeon.Add(tunelToClean2);
				}

				if (ground.Length > 0)
				{
					GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(-sizeOfFloat - (m * sizeOfFloat) + groundTransforamX, transformY,
							(dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(tunelToClean3);
				}

				if (hasRoof)
				{
					GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0,roofs.Length)],new Vector3(-sizeOfFloat-(m*sizeOfFloat)+groundTransforamX,transformY+sizeOfInt,(dootPlaceJ*sizeOfFloat)+groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(tunelToClean31);
				}

				GameObject tunelToCleansizeOfInt = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-sizeOfFloat-(m*sizeOfFloat)+groundTransforamX,transformY,-2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
				GameObject tunelToClean5 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-sizeOfFloat-(m*sizeOfFloat)+groundTransforamX,transformY,2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
				
				
				objectsToCleanDungeon.Add(tunelToCleansizeOfInt);
				objectsToCleanDungeon.Add(tunelToClean5);

				tunelVector3Saver.Add(new Vector3(-sizeOfFloat - (m * sizeOfFloat) + groundTransforamX, transformY,(dootPlaceJ * sizeOfFloat) + groundTransforamZ));
			}
			groundTransX=-(roomSizeJ*sizeOfInt)-(tunelSizeX*sizeOfInt)+groundTransforamX;
			groundTransZ = groundTransforamZ;
			this.entranceDoorPlaceJ = dootPlaceJ;
			this.entranceDoorPlace = 2;
			canCreateDungeon = true;


			//				oldDoorPlaceI=16;
		}
		else if(nextDoorSide==1){
			int tunelSize =  Random.Range(3,8);
			for(int n = 0; n<tunelSize;n++){
				if (columns.Length > 0)
				{
					GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							-5.9f - (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));
					GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							-5.9f - (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));
					objectsToCleanDungeon.Add(tunelToClean1);
					objectsToCleanDungeon.Add(tunelToClean2);
				}

				if (ground.Length > 0)
				{
					GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							-sizeOfFloat - (n * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(tunelToClean3);
				}

				if (hasRoof)
				{
					GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY+sizeOfInt, -sizeOfFloat - (n * sizeOfInt) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(tunelToClean31);
				}

				GameObject tunelToCleansizeOfInt =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,-sizeOfFloat-(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				GameObject tunelToClean5 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,-sizeOfFloat-(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				
				
				objectsToCleanDungeon.Add(tunelToCleansizeOfInt);
				objectsToCleanDungeon.Add(tunelToClean5);
		
				tunelVector3Saver.Add(new Vector3(doorPlaceI*sizeOfInt+groundTransforamX,transformY,-sizeOfFloat-(n*sizeOfInt)+groundTransforamZ));
			}
			groundTransZ=-(roomSizeI*sizeOfInt)-(tunelSize*sizeOfInt)+groundTransforamZ;
			groundTransX = groundTransforamX;
			this.entranceDoorPlaceI = doorPlaceI;
			this.entranceDoorPlace = 3;
			canCreateDungeon = true;

					
		}
		else if(nextDoorSide==2){
					int tunelSizeX =  Random.Range(3,8);
					for(int m = 0; m<tunelSizeX;m++){
						if (columns.Length > 0)
						{
							GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(-1.9f + (roomSizeJ * sizeOfInt) + (m * sizeOfInt) + groundTransforamX, transformY,
									-2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
							GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(-1.9f + (roomSizeJ * sizeOfInt) + (m * sizeOfInt) + groundTransforamX, transformY,
									2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(tunelToClean1);
							objectsToCleanDungeon.Add(tunelToClean2);
						}

						if (ground.Length > 0)
						{
							GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX,
									transformY, (dootPlaceJ * sizeOfFloat) + groundTransforamZ),
								Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(tunelToClean3);
						}

						if (hasRoof)
						{
							GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
								new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX, transformY+sizeOfInt,
									(dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(tunelToClean31);
						}

						GameObject tunelToCleansizeOfInt =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSizeJ*sizeOfFloat)+(m*sizeOfFloat)+groundTransforamX,transformY,-2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						GameObject tunelToClean5 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSizeJ*sizeOfFloat)+(m*sizeOfFloat)+groundTransforamX,transformY,2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						
						
						objectsToCleanDungeon.Add(tunelToCleansizeOfInt);
						objectsToCleanDungeon.Add(tunelToClean5);
						
						tunelVector3Saver.Add(new Vector3((roomSizeJ*sizeOfFloat)+(m*sizeOfFloat)+groundTransforamX, transformY,(dootPlaceJ*sizeOfFloat)+groundTransforamZ));					
					}
					groundTransX=(roomSizeJ*sizeOfInt)+(tunelSizeX*sizeOfInt)+groundTransforamX;
					groundTransZ = groundTransforamZ;
					this.entranceDoorPlaceJ = dootPlaceJ;
					this.entranceDoorPlace = 0;
					canCreateDungeon = true;

					
					//				oldDoorPlaceI=16;
				}
				else if(nextDoorSide==3){
					int tunelSize =  Random.Range(3,8);
					for(int n = 0; n<tunelSize;n++){
						if (columns.Length > 0)
						{
							GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(-2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
									-1.9f + (roomSizeI * sizeOfFloat) + (n * sizeOfFloat) + groundTransforamZ),
								Quaternion.Euler(0, 180, 0));
							GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
									-1.9f + (roomSizeI * sizeOfFloat) + (n * sizeOfFloat) + groundTransforamZ),
								Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(tunelToClean1);
							objectsToCleanDungeon.Add(tunelToClean2);
						}

						if (ground.Length > 0)
						{
							GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY,
									(roomSizeI * sizeOfFloat) + (n * sizeOfInt) + groundTransforamZ),
								Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(tunelToClean3);
						}

						if (hasRoof)
						{
							GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
								new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY+sizeOfInt,
									(roomSizeI * sizeOfFloat) + (n * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(tunelToClean31);
						}

						GameObject tunelToCleansizeOfInt =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,(roomSizeI*sizeOfFloat)+(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
						GameObject tunelToClean5 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,(roomSizeI*sizeOfFloat)+(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
						
						
						objectsToCleanDungeon.Add(tunelToCleansizeOfInt);
						objectsToCleanDungeon.Add(tunelToClean5);
						
						tunelVector3Saver.Add(new Vector3(doorPlaceI*sizeOfInt+groundTransforamX,transformY,(roomSizeI*sizeOfFloat)+(n*sizeOfInt)+groundTransforamZ) );
					}
					groundTransZ=(roomSizeI*sizeOfInt)+(tunelSize*sizeOfInt)+groundTransforamZ;
					groundTransX = groundTransforamX;
					this.entranceDoorPlaceI = doorPlaceI;
					this.entranceDoorPlace = 1;
					canCreateDungeon = true;

				}
		}
		Debug.Log("End"+generatedDungeonNumber);
			pastRoomSizeI = roomSizeI;
			pastRoomSizeJ = roomSizeJ;
			if (generatedDungeonNumber==0)
			{
				player.transform.position=new Vector3(playerSpawnPosX,playerSpawnPosY,playerSpawnPosZ);
			}
			generatedDungeonNumber = generatedDungeonNumber+ 1;
			if (generatedDungeonNumber < maxDungeonSize)
			{
				GenProgressionDungeon(groundTransX, groundTransZ, this.entranceDoorPlaceI, this.entranceDoorPlaceJ, this.entranceDoorPlace, pastRoomSizeI, pastRoomSizeJ, lockedSide);
			}		
	}
	
	public void GenDungeon(int groundTransforamX, int groundTransforamZ, int entranceDoorPlaceI, int entranceDoorPlaceJ, int entranceDoorPlace, int pastRSizeI, int pastRSizeJ, int lockedSide)
	{
		if (generatedDungeonNumber==1)
		{
			lockedSide = entranceDoorPlace;
		}
		
		
		int roomSizeI = Random.Range(roomSizeIlow,roomSizeImax);
		
		int roomSizeJ = Random.Range(roomSizeJlow,roomSizeJmax);
		int doorPlaceI =  Random.Range(0,roomSizeJ);
		if(doorPlaceI==roomSizeI){
			doorPlaceI=doorPlaceI-1;
		}	
		int dootPlaceJ =  Random.Range(0,roomSizeI);
		if(dootPlaceJ==roomSizeJ){
			dootPlaceJ=dootPlaceJ-1;
		}
		
		if (entranceDoorPlace == 2)	
		{
			if (entranceDoorPlaceJ >= roomSizeI && entranceDoorPlaceJ>=0)
			{
				groundTransforamZ =sizeOfInt+ groundTransforamZ + (entranceDoorPlaceJ-roomSizeI) * sizeOfInt;
				entranceDoorPlaceJ = roomSizeI-1;
			}

			if (pastRSizeJ > roomSizeJ||pastRSizeJ < roomSizeJ)
			{
				groundTransforamX = groundTransforamX - (roomSizeJ-pastRSizeJ) * sizeOfInt; //+++++++++++
			}
			
		}		
		else if (entranceDoorPlace == 3)
		{
			if (pastRSizeI > roomSizeI||pastRSizeI < roomSizeI)
			{
				groundTransforamZ = groundTransforamZ - (roomSizeI-pastRSizeI) * sizeOfInt; //+++++++++++++
			}

			if (entranceDoorPlaceI >= roomSizeJ && entranceDoorPlaceI>=0)
			{
				groundTransforamX =sizeOfInt+ groundTransforamX + (entranceDoorPlaceI-roomSizeJ) * sizeOfInt;
				entranceDoorPlaceI = roomSizeJ-1;
			}
			
		}
		else
		{
			if (entranceDoorPlace == 0)
			{
				if (entranceDoorPlaceJ >= roomSizeI && entranceDoorPlaceJ>=0)
				{
					groundTransforamZ =sizeOfInt+ groundTransforamZ + (entranceDoorPlaceJ-roomSizeI) * sizeOfInt;
					entranceDoorPlaceJ = roomSizeI-1;
				}
			}
		
			if (entranceDoorPlace == 1)
			{
				if (entranceDoorPlaceI >= roomSizeJ && entranceDoorPlaceI>=0)
				{
					groundTransforamX =sizeOfInt+ groundTransforamX + (entranceDoorPlaceI - roomSizeJ) * sizeOfInt;
					entranceDoorPlaceI = roomSizeJ-1;
				}
			}
		}
				
		int oldDoorPlaceI = 0;
		int oldDoorPlaceJ = 0;
		int nextDoorSide = Random.Range(0, sizeOfInt); // 0 - West, 1 South, 2 Ost, 3 North
		while (entranceDoorPlace == nextDoorSide || nextDoorSide==lockedSide)
		{
			 nextDoorSide = Random.Range(0, sizeOfInt); // 0 - West, 1 South, 2 Ost, 3 North
		}
//		int nextDoorSide = 2; // 0 - West, 1 South, 2 Ost, 3 North
		for(int i = 0; i<roomSizeI;i++){
				for(int j = 0; j<roomSizeJ;j++){
					if (ground.Length > 0)
					{
						GameObject toClean = Instantiate(ground[Random.Range(0, ground.Length)],
							new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
								groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean);
					}

					if (hasRoof)
					{
						GameObject toClean31 = Instantiate(roofs[Random.Range(0,roofs.Length)], new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY+sizeOfInt,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean31);
					}

					//Enemy Spawner
					if(generatedDungeonNumber>=2&&SetRangeChance(enemyRange)&&enemies.Length>0){
						GameObject toClean22 =	Instantiate(enemies[Random.Range(0,enemies.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean22);
					}
					if (entranceDoorPlace == -1)
					{
						if (nextDoorSide==0)
					{
						if(j==0&&i!=dootPlaceJ){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==roomSizeI-1){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==0){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(j==roomSizeJ-1){ 	
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
					}
					else if (nextDoorSide==1)
					{
						if(j==0){ 	
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==roomSizeI-1){
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==0&&j!=doorPlaceI){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(j==roomSizeJ-1){ 	
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
					}
					else if (nextDoorSide==2)
					{
						if(j==0){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==roomSizeI-1){
							GameObject toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==0){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(j==roomSizeJ-1&&i!=dootPlaceJ){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
					}
					else if(nextDoorSide==3)
					{
						if(j==0){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==roomSizeI-1&&j!=doorPlaceI){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(i==0){
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
						if(j==roomSizeJ-1){ 	
							GameObject	toClean3 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean3);
						}
					}
							
					}
					else
					{
						if (nextDoorSide == 0 && entranceDoorPlace == 2)
						{
							if (j == 0 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 0 && entranceDoorPlace == 3)
						{
							if (j == 0 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 0 && entranceDoorPlace == 1)
						{
							if (j == 0 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 1 && entranceDoorPlace == 3)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 1 && entranceDoorPlace == 2)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 1 && entranceDoorPlace == 0)
						{
							if (j == 0 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 2 && entranceDoorPlace == 0)
						{
							if (j == 0 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 2 && entranceDoorPlace == 1)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 2 && entranceDoorPlace == 3)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != dootPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 3 && entranceDoorPlace == 1)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0 && j != entranceDoorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 3 && entranceDoorPlace == 2)
						{
							if (j == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
						else if (nextDoorSide == 3 && entranceDoorPlace == 0)
						{
							if (j == 0 && i != entranceDoorPlaceJ)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == roomSizeI - 1 && j != doorPlaceI)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (i == 0)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClea12);
							}

							if (j == roomSizeJ - 1)
							{
								GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
									new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
										groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClea12);
							}
						}
					}

	//environmentToWalls
//					if(SetRangeChance(furnitureRange)){
//						if(j==0&&i!=entranceDoorPlaceJ&&i%3==0){
//							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-1.9f+(j*sizeOfInt)+groundTransforamX,1.7f,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(j==0&&i!=entranceDoorPlaceJ){
//							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-1.4f+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0,90, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(j==roomSizeJ-1&&i!=dootPlaceJ&&i%3==0){
//							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(1.9f+(j*sizeOfInt)+groundTransforamX,1.7f,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,-90, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(j==roomSizeJ-1&&i!=dootPlaceJ){
//							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(1.4f+(j*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(i==0&&j!=entranceDoorPlaceI&&j%3==0){
//							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,0, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(i==0&&j!=entranceDoorPlaceI){
//							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(i==roomSizeI-1&&j!=doorPlaceI&&j%3==0){
//							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f,1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//						else if(i==roomSizeI-1&&j!=doorPlaceI){
//							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
//							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
//						}
//					}
					//Columns
					if(SetRangeChance(columnRange)&&columns.Length>0){
						if(j==0&&i!=entranceDoorPlaceJ){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						
						else if(j==roomSizeJ-1&&i!=dootPlaceJ){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=entranceDoorPlaceI){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+groundTransforamX,transformY,-2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSizeI-1&&j!=doorPlaceI){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+groundTransforamX,transformY,2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//furniture
					if(SetRangeChance(furnitureRange)&&furniture.Length>0){
						if(j!=0&&i!=0&&i!=roomSizeI-1&&j!=roomSizeJ-1){
							GameObject toClean2 = Instantiate(furniture[Random.Range(0,furniture.Length)],new Vector3((j*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.identity);
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
				}
			}
		Debug.Log("Mid"+generatedDungeonNumber);
		if(generatedDungeonNumber<maxDungeonSize-1){
		if(nextDoorSide==0){
			int tunelSizeX =  roomSizeImax;
			for(int m = 0; m<tunelSizeX;m++){
				if (columns.Length > 0)
				{
					GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-5.9f - (m * sizeOfInt) + groundTransforamX, transformY,
							-2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
					GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-5.9f - (m * sizeOfInt) + groundTransforamX, transformY,
							2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
					objectsToCleanDungeon.Add(tunelToClean1);
					objectsToCleanDungeon.Add(tunelToClean2);
				}

				if (ground.Length > 0)
				{
					GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(-sizeOfFloat - (m * sizeOfFloat) + groundTransforamX, transformY,
							(dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(tunelToClean3);
				}

				if (hasRoof)
				{
					GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0,roofs.Length)],new Vector3(-sizeOfFloat-(m*sizeOfFloat)+groundTransforamX,transformY+sizeOfInt,(dootPlaceJ*sizeOfFloat)+groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(tunelToClean31);
				}

				GameObject tunelToCleansizeOfInt = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-sizeOfFloat-(m*sizeOfFloat)+groundTransforamX,transformY,-2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
				GameObject tunelToClean5 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-sizeOfFloat-(m*sizeOfFloat)+groundTransforamX,transformY,2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
				
				
				objectsToCleanDungeon.Add(tunelToCleansizeOfInt);
				objectsToCleanDungeon.Add(tunelToClean5);

				tunelVector3Saver.Add(new Vector3(-sizeOfFloat - (m * sizeOfFloat) + groundTransforamX, transformY,(dootPlaceJ * sizeOfFloat) + groundTransforamZ));
			}
			groundTransX=-(roomSizeJ*sizeOfInt)-(tunelSizeX*sizeOfInt)+groundTransforamX;
			groundTransZ = groundTransforamZ;
			this.entranceDoorPlaceJ = dootPlaceJ;
			this.entranceDoorPlace = 2;
			canCreateDungeon = true;


			//				oldDoorPlaceI=16;
		}
		else if(nextDoorSide==1){
			int tunelSize =  roomSizeJmax;
			for(int n = 0; n<tunelSize;n++){
				if (columns.Length > 0)
				{
					GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							-5.9f - (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));
					GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							-5.9f - (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));
					objectsToCleanDungeon.Add(tunelToClean1);
					objectsToCleanDungeon.Add(tunelToClean2);
				}

				if (ground.Length > 0)
				{
					GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							-sizeOfFloat - (n * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(tunelToClean3);
				}

				if (hasRoof)
				{
					GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY+sizeOfInt, -sizeOfFloat - (n * sizeOfInt) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(tunelToClean31);
				}

				GameObject tunelToCleansizeOfInt =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,-sizeOfFloat-(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				GameObject tunelToClean5 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,-sizeOfFloat-(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				
				
				objectsToCleanDungeon.Add(tunelToCleansizeOfInt);
				objectsToCleanDungeon.Add(tunelToClean5);
		
				tunelVector3Saver.Add(new Vector3(doorPlaceI*sizeOfInt+groundTransforamX,transformY,-sizeOfFloat-(n*sizeOfInt)+groundTransforamZ));
			}
			groundTransZ=-(roomSizeI*sizeOfInt)-(tunelSize*sizeOfInt)+groundTransforamZ;
			groundTransX = groundTransforamX;
			this.entranceDoorPlaceI = doorPlaceI;
			this.entranceDoorPlace = 3;
			canCreateDungeon = true;

					
		}
		else if(nextDoorSide==2){
					int tunelSizeX =  roomSizeImax;
					for(int m = 0; m<tunelSizeX;m++){
						if (columns.Length > 0)
						{
							GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(-1.9f + (roomSizeJ * sizeOfInt) + (m * sizeOfInt) + groundTransforamX, transformY,
									-2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
							GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(-1.9f + (roomSizeJ * sizeOfInt) + (m * sizeOfInt) + groundTransforamX, transformY,
									2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(tunelToClean1);
							objectsToCleanDungeon.Add(tunelToClean2);
						}

						if (ground.Length > 0)
						{
							GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX,
									transformY, (dootPlaceJ * sizeOfFloat) + groundTransforamZ),
								Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(tunelToClean3);
						}

						if (hasRoof)
						{
							GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
								new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX, transformY+sizeOfInt,
									(dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(tunelToClean31);
						}

						GameObject tunelToCleansizeOfInt =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSizeJ*sizeOfFloat)+(m*sizeOfFloat)+groundTransforamX,transformY,-2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						GameObject tunelToClean5 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSizeJ*sizeOfFloat)+(m*sizeOfFloat)+groundTransforamX,transformY,2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						
						
						objectsToCleanDungeon.Add(tunelToCleansizeOfInt);
						objectsToCleanDungeon.Add(tunelToClean5);
						
						tunelVector3Saver.Add(new Vector3((roomSizeJ*sizeOfFloat)+(m*sizeOfFloat)+groundTransforamX, transformY,(dootPlaceJ*sizeOfFloat)+groundTransforamZ));					
					}
					groundTransX=(roomSizeJ*sizeOfInt)+(tunelSizeX*sizeOfInt)+groundTransforamX;
					groundTransZ = groundTransforamZ;
					this.entranceDoorPlaceJ = dootPlaceJ;
					this.entranceDoorPlace = 0;
					canCreateDungeon = true;

					
					//				oldDoorPlaceI=16;
				}
				else if(nextDoorSide==3){
					int tunelSize =  roomSizeJmax;
					for(int n = 0; n<tunelSize;n++){
						if (columns.Length > 0)
						{
							GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(-2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
									-1.9f + (roomSizeI * sizeOfFloat) + (n * sizeOfFloat) + groundTransforamZ),
								Quaternion.Euler(0, 180, 0));
							GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
									-1.9f + (roomSizeI * sizeOfFloat) + (n * sizeOfFloat) + groundTransforamZ),
								Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(tunelToClean1);
							objectsToCleanDungeon.Add(tunelToClean2);
						}

						if (ground.Length > 0)
						{
							GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY,
									(roomSizeI * sizeOfFloat) + (n * sizeOfInt) + groundTransforamZ),
								Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(tunelToClean3);
						}

						if (hasRoof)
						{
							GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
								new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY+sizeOfInt,
									(roomSizeI * sizeOfFloat) + (n * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(tunelToClean31);
						}

						GameObject tunelToCleansizeOfInt =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,(roomSizeI*sizeOfFloat)+(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
						GameObject tunelToClean5 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,(roomSizeI*sizeOfFloat)+(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
						
						
						objectsToCleanDungeon.Add(tunelToCleansizeOfInt);
						objectsToCleanDungeon.Add(tunelToClean5);
						
						tunelVector3Saver.Add(new Vector3(doorPlaceI*sizeOfInt+groundTransforamX,transformY,(roomSizeI*sizeOfFloat)+(n*sizeOfInt)+groundTransforamZ) );
					}
					groundTransZ=(roomSizeI*sizeOfInt)+(tunelSize*sizeOfInt)+groundTransforamZ;
					groundTransX = groundTransforamX;
					this.entranceDoorPlaceI = doorPlaceI;
					this.entranceDoorPlace = 1;
					canCreateDungeon = true;

				}
		}
		Debug.Log("End"+generatedDungeonNumber);
			pastRoomSizeI = roomSizeI;
			pastRoomSizeJ = roomSizeJ;
			if (generatedDungeonNumber==0)
			{
				player.transform.position=new Vector3(playerSpawnPosX,playerSpawnPosY,playerSpawnPosZ);
			}
			generatedDungeonNumber = generatedDungeonNumber+ 1;
			if (generatedDungeonNumber < maxDungeonSize)
			{
				GenDungeon(groundTransX, groundTransZ, this.entranceDoorPlaceI, this.entranceDoorPlaceJ, this.entranceDoorPlace, pastRoomSizeI, pastRoomSizeJ, lockedSide);
			}		
	}
	
	
	
	public void GenPerpendicularDungeon(){
	int roomSize  = Random.Range(roomSizeIlow,roomSizeImax);
	bool isDoorPlaceX  = false;
	int doorPlaceX  = 0;
//	var randomGround : int = Random.Range(0,ground.length);
//	var isQuadrat : boolean = quadratRoom();
		int doorPlace  =  Random.Range(0,roomSize);
	if(doorPlace==roomSize){
		doorPlace=doorPlace-1;
	}
	int oldDoorPlace  = 0;
	int groundTransforamZ  = groundTransZ;
	int grTrZ  = groundTransX;
	
	int dungeonSize  = Random.Range(5,maxDungeonSize);
	int dungeonSizeCounter  = 0;
	while(dungeonSizeCounter<dungeonSize){
		for(int i  = 0; i<roomSize;i++){
			for(int j  = 0; j<roomSize;j++){
				if (ground.Length > 0)
				{
					GameObject toClean = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(0 + (j * sizeOfInt) + grTrZ, transformY, groundTransforamZ + (i * sizeOfInt)),
						Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean);
				}

				if (hasRoof)
					{
						GameObject toClean11 = Instantiate(roofs[Random.Range(0,roofs.Length)],new  Vector3(0+(j*sizeOfInt)+grTrZ,transformY+sizeOfInt,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean11);
					}

					if(dungeonSizeCounter>=2&&SetRangeChance(enemyRange)&&enemies.Length>0){
						GameObject toClean11 =	Instantiate(enemies[Random.Range(0,enemies.Length)],new  Vector3(0+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean11);
					}
					
					if(j==0){ 	
						GameObject toClean1 =Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(-2+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
						objectsToCleanDungeon.Add(toClean1);
					} 
					
					if(!isDoorPlaceX){
						if(j==roomSize-1){ 	
							GameObject toClean1 =Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(2+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean1);
						}
					}
					else{
						if(j==roomSize-1&&i!=doorPlaceX){ 	
							GameObject toClean1 =	Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(2+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean1);
						}
					}
					
					if(dungeonSizeCounter!=0){
						if(i==0&&j!=oldDoorPlace){
							GameObject toClean1 =	Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(0+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean1);
						}
					}
					else{
						if(i==0){
							GameObject toClean1 =Instantiate(walls[Random.Range(0,walls.Length)],new  Vector3(0+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean1);
						}
					}
									
					if(i==roomSize-1&&j!=doorPlace){
						GameObject toClean1 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
						objectsToCleanDungeon.Add(toClean1);
					}	
					
					//environmentToWalls
					if(SetRangeChance(wallEnvRange)&&environmentToWall.Length>0){
						 if(j==0&&i!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-1.4f+(j*sizeOfInt)+grTrZ,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(j==roomSize-1&&i!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(1.4f+(j*sizeOfFloat)+grTrZ,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,transformY,-1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSize-1&&j!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,transformY,1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
					if(SetRangeChance(lampsRange)&&lamps.Length>0){
						if(j==0&&i!=oldDoorPlace&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-1.9f+(j*sizeOfInt)+grTrZ,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(j==roomSize-1&&i!=doorPlace&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(1.9f+(j*sizeOfInt)+grTrZ,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,-90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlace&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,1.7f+transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,0, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSize-1&&j!=doorPlace&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,1.7f+transformY,1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
					
					//Columns
					if(SetRangeChance(columnRange)&&columns.Length>0){
						if(j==0&&i!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-2f+(j*sizeOfFloat)+grTrZ,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						
						else if(j==roomSize-1&&i!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(2f+(j*sizeOfFloat)+grTrZ,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+grTrZ,transformY,-2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSize-1&&j!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+grTrZ,transformY,2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//furniture
					if(SetRangeChance(furnitureRange)&&furniture.Length>0){
						if(j!=0&&i!=0&&i!=roomSize-1&&j!=roomSize){
							GameObject toClean2 = Instantiate(furniture[Random.Range(0,furniture.Length)],new Vector3((j*sizeOfFloat)+grTrZ,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.identity);
							objectsToCleanDungeon.Add(toClean2);
						}
					}
			}
		}
		
		if(isDoorPlaceX){
			int tunelSizeX =  Random.Range(1,10);
			for(int m  = 0; m<tunelSizeX;m++){
				if (ground.Length > 0)
				{
					GameObject toClean1 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3((roomSize * sizeOfInt) + (m * sizeOfInt) + grTrZ, transformY,
							(doorPlaceX * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean1);

				}

				if (hasRoof)
				{
					GameObject toClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3((roomSize * sizeOfInt) + (m * sizeOfInt)+grTrZ, transformY+sizeOfInt, (doorPlaceX * sizeOfInt) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean31);
				}

				GameObject toClean2 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSize*sizeOfInt)+(m*sizeOfInt)+grTrZ,transformY,-2+(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
				GameObject toClean3 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSize*sizeOfInt)+(m*sizeOfInt)+grTrZ,transformY,2+(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
				objectsToCleanDungeon.Add(toClean2);
				objectsToCleanDungeon.Add(toClean3);
			}
			int groundTransforamX = tunelSizeX*sizeOfInt+grTrZ;
			int dungeonSizeCounterRight = 0;
			int dungeonSizeRight = Random.Range(5,15);	
			while(dungeonSizeCounterRight<dungeonSizeRight){
					for(int s  = 0; s<roomSize;s++){
					for(int v  = 0; v<roomSize;v++){
						if (ground.Length > 0)
						{
							GameObject toClean1 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3((roomSize * sizeOfInt) + (groundTransforamX) + (v * sizeOfInt), transformY,
									groundTransforamZ + (s * sizeOfInt)), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toClean1);
						}

						if (hasRoof)
							{
								GameObject toClean31 = Instantiate(roofs[Random.Range(0,roofs.Length)],new Vector3((roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY+sizeOfInt,groundTransforamZ+(s*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
								objectsToCleanDungeon.Add(toClean31);
							}

							if(dungeonSizeCounter>=2&&SetRangeChance(enemyRange)&&enemies.Length>0){
								GameObject toClean11 =	Instantiate(enemies[Random.Range(0,enemies.Length)],new Vector3((roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ+(s*sizeOfInt))  , Quaternion.Euler(0, 0, 0));
								objectsToCleanDungeon.Add(toClean11);
							}
							if(v==0&&s!=doorPlaceX){ 	
								GameObject toClean2 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ+(s*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
								objectsToCleanDungeon.Add(toClean2);
							} 
							else{
								if(v==roomSize-1&&s!=doorPlaceX){ 	
									GameObject toClean2 =	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ+(s*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
									objectsToCleanDungeon.Add(toClean2);
								}
							}
							
							if(dungeonSizeCounterRight!=0){
								if(s==0){
									GameObject toClean2 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ-2+(s*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
									objectsToCleanDungeon.Add(toClean2);
								}
							}
							else{
								if(s==0){
									GameObject toClean2 =	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ-2+(s*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
									objectsToCleanDungeon.Add(toClean2);
								}
							}
											
							if(s==roomSize-1){
								GameObject toClean2 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(roomSize*sizeOfInt)+(groundTransforamX)+(v*sizeOfInt),transformY,groundTransforamZ+2+(s*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
								objectsToCleanDungeon.Add(toClean2);
							}	
							
							
							//environmentToWalls
					if(SetRangeChance(wallEnvRange)&&environmentToWall.Length>0){
						 if(v==0&&s!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-1.4f+(roomSize*sizeOfInt)+(v*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(s*sizeOfInt)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(v==roomSize-1&&s!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(1.4f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==0&&v!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,-1.4f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==roomSize-1&&v!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,1.4f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
							if(SetRangeChance(lampsRange)&&lamps.Length>0){
						if(v==0&&s!=oldDoorPlace&&s%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-1.9f+(roomSize*sizeOfInt)+(v*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(v==roomSize-1&&s!=doorPlace&&s%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(1.9f+(roomSize*sizeOfInt)+(v*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,-90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==0&&v!=oldDoorPlace&&v%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,1.7f+transformY,-1.9f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,0, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==roomSize-1&&v!=doorPlace&&v%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,1.7f+transformY,1.9f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//Columns
					if(SetRangeChance(columnRange)&&columns.Length>0){
						if(v==0&&s!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						
						else if(v==roomSize-1&&s!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(2f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==0&&v!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,-2f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(s==roomSize-1&&v!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,2f+groundTransforamZ+(s*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//furniture
					if(SetRangeChance(furnitureRange)&&furniture.Length>0){
						if(v!=0&&s!=0&&s!=roomSize-1&&v!=roomSize-1){
							GameObject toClean2 = Instantiate(furniture[Random.Range(0,furniture.Length)],new Vector3((roomSize*sizeOfInt)+(v*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(s*sizeOfFloat)) , Quaternion.identity);
							objectsToCleanDungeon.Add(toClean2);
						}
					}
							
					}
				}
				
				int tunelSizeXTun =  Random.Range(1,10);
				
				if(dungeonSizeCounterRight!=dungeonSizeRight-1){
					for(int p = 0; p<tunelSizeXTun;p++){
						if (ground.Length > 0)
						{
							GameObject toClean1 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3((roomSize * sizeOfInt) * 2 + (p * sizeOfInt) + groundTransforamX,
									transformY, (doorPlaceX * sizeOfInt) + groundTransforamZ),
								Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toClean1);
						}

						if (hasRoof)
						{
							GameObject toClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
								new Vector3((roomSize * sizeOfInt) * 2 + (p * sizeOfInt) + groundTransforamX, transformY+sizeOfInt,
									(doorPlaceX * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toClean31);
						}

						GameObject toClean2 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSize*sizeOfInt)*2+(p*sizeOfInt)+groundTransforamX,transformY,-2+(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						GameObject toClean3 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSize*sizeOfInt)*2+(p*sizeOfInt)+groundTransforamX,transformY,2+(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						
						objectsToCleanDungeon.Add(toClean2);
						objectsToCleanDungeon.Add(toClean3);
					}
					
					groundTransforamX=(roomSize*sizeOfInt)+(tunelSizeXTun*sizeOfInt)+groundTransforamX;
				}
				else{
					GameObject toClean1 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(roomSize*sizeOfInt)*2+groundTransforamX,transformY,(doorPlaceX*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				objectsToCleanDungeon.Add(toClean1);
				}
				
				dungeonSizeCounterRight=dungeonSizeCounterRight+1;
			}	
		}
		int tunelSize =  Random.Range(1,6);

		if(dungeonSizeCounter!=dungeonSize-1){
			for(int n = 0; n<tunelSize;n++){
				if (ground.Length > 0)
				{
					GameObject toClean1 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(doorPlace * sizeOfInt + grTrZ, transformY,
							(roomSize * sizeOfInt) + (n * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean1);

				}

				if (hasRoof)
				{
					GameObject toClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3(doorPlace * sizeOfInt+grTrZ, transformY+sizeOfInt, (roomSize * sizeOfInt) + (n * sizeOfInt) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean31);
				}

				GameObject toClean2 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(doorPlace*sizeOfInt)+grTrZ,transformY,(roomSize*sizeOfInt)+(n*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				GameObject toClean3 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(doorPlace*sizeOfInt)+grTrZ,transformY,(roomSize*sizeOfInt)+(n*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				objectsToCleanDungeon.Add(toClean2);
				objectsToCleanDungeon.Add(toClean3);
			}
		}
		else{
			GameObject toClean1 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(doorPlace*sizeOfInt+grTrZ,transformY,-2+(roomSize*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
			objectsToCleanDungeon.Add(toClean1);
		}
		
		groundTransforamZ=(roomSize*sizeOfInt)+(tunelSize*sizeOfInt)+groundTransforamZ;
		oldDoorPlace=doorPlace;
		roomSize = Random.Range(sizeOfInt,9);			
		while(roomSize<=oldDoorPlace){
			roomSize = Random.Range(sizeOfInt,9);
		}		
		doorPlace =  Random.Range(0,roomSize);	
		if(doorPlace==roomSize){
			doorPlace=doorPlace-1;
		}
		isDoorPlaceX =  Random.Range(0,roomSize) % 2 == 0 ? true : false;
		if(isDoorPlaceX){	
			doorPlaceX=  Random.Range(0,roomSize);	
			if(doorPlaceX==roomSize){
			doorPlaceX=doorPlaceX-1;
			}
		}
		
		
		dungeonSizeCounter=dungeonSizeCounter+1;
	}
	player.transform.position=new Vector3(playerSpawnPosX,playerSpawnPosY,playerSpawnPosZ);	
}
	
	
	
		public void GenLineDungeon(){
	int roomSize  = Random.Range(roomSizeIlow,roomSizeImax);
	int doorPlace  =  Random.Range(0,roomSize);
	if(doorPlace==roomSize){
		doorPlace=doorPlace-1;
	}
	int oldDoorPlace  = 0;
	int groundTransforamZ = groundTransZ;
	int groundTransforamX = groundTransX;
	
	int dungeonSize  = Random.Range(5,maxDungeonSize);
	int dungeonSizeCounter  = 0;
	while(dungeonSizeCounter<dungeonSize){
		for(int i = 0; i<roomSize;i++){
			for(int j = 0; j<roomSize;j++){
				if (ground.Length > 0)
				{
					GameObject toClean = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
							groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean);
				}

				if (hasRoof)
					{
						GameObject toClean3 = Instantiate(roofs[Random.Range(0, roofs.Length)],new Vector3(0 + (j * sizeOfInt)+groundTransforamX, transformY+sizeOfInt, groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean3);
					}

					if(dungeonSizeCounter>=2&&SetRangeChance(enemyRange)&&enemies.Length>0){
						GameObject toClean1 =	Instantiate(enemies[Random.Range(0,enemies.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean1);
					}
					
					if(j==0){ 	
					GameObject toClean1 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
					objectsToCleanDungeon.Add(toClean1);
					} 
					
					if(j==roomSize-1){ 	
						GameObject toClean1 =	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean1);
					}
					
					if(dungeonSizeCounter!=0){
						if(i==0&&j!=oldDoorPlace){
							GameObject toClean1 =	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean1);
						}
					}
					else{
						if(i==0){
							GameObject toClean1 =	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean1);
						}
					}
									
					if(i==roomSize-1&&j!=doorPlace){
						GameObject toClean1 =	Instantiate(walls[Random.Range(0,walls.Length)], new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
						objectsToCleanDungeon.Add(toClean1);
					}
					
					//environmentToWalls
					if(SetRangeChance(wallEnvRange)&&environmentToWall.Length>0){
						 if(j==0&&i!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-1.4f+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(j==roomSize-1&&i!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(1.4f+(j*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSize-1&&j!=doorPlace){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
					if(SetRangeChance(lampsRange)&&lamps.Length>0){
						if(j==0&&i!=oldDoorPlace&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-1.9f+(j*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(j==roomSize-1&&i!=doorPlace&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(1.9f+(j*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,-90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlace&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f+transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,0, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSize-1&&j!=doorPlace&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f+transformY,1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
					
					//Columns
					if(SetRangeChance(columnRange)&&columns.Length>0){
						if(j==0&&i!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						
						else if(j==roomSize-1&&i!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+groundTransforamX,transformY,-2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSize-1&&j!=doorPlace){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+groundTransforamX,transformY,2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//furniture
					if(SetRangeChance(furnitureRange)&&furniture.Length>0){
						if(j!=0&&i!=0&&i!=roomSize-1&&j!=roomSize){
							GameObject toClean2 = Instantiate(furniture[Random.Range(0,furniture.Length)],new Vector3((j*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.identity);
							objectsToCleanDungeon.Add(toClean2);
						}
					}
			}
		}
		int tunelSize =  Random.Range(1,6);

		if(dungeonSizeCounter!=dungeonSize-1){
			for(int n = 0; n<tunelSize;n++){
				if (ground.Length > 0)
				{
					GameObject toClean2 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(doorPlace * sizeOfInt + groundTransforamX, transformY,
							(roomSize * sizeOfInt) + (n * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean2);
				}

				if (hasRoof)
				{
					GameObject toClean21 =	Instantiate(roofs[Random.Range(0,roofs.Length)],new Vector3(doorPlace*sizeOfInt+groundTransforamX,transformY+sizeOfInt,(roomSize*sizeOfInt)+(n*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 0, 0));
					objectsToCleanDungeon.Add(toClean21);
				}
				GameObject toClean3 =	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(doorPlace*sizeOfInt)+groundTransforamX,transformY,(roomSize*sizeOfInt)+(n*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				GameObject toCleansizeOfInt =	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(doorPlace*sizeOfInt)+groundTransforamX,transformY,(roomSize*sizeOfInt)+(n*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
				
				objectsToCleanDungeon.Add(toClean3);
				objectsToCleanDungeon.Add(toCleansizeOfInt);
			}
		}
		else{
			GameObject toClean1 =	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(doorPlace*sizeOfInt+groundTransforamX,transformY,-2+(roomSize*sizeOfInt)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
			objectsToCleanDungeon.Add(toClean1);
		}
		
		groundTransforamZ=(roomSize*sizeOfInt)+(tunelSize*sizeOfInt)+groundTransforamZ;
		oldDoorPlace=doorPlace;
		roomSize = Random.Range(sizeOfInt,9);			
		while(roomSize<=oldDoorPlace){
			roomSize = Random.Range(sizeOfInt,9);
		}		
		doorPlace =  Random.Range(0,roomSize);	
		if(doorPlace==roomSize){
			doorPlace=doorPlace-1;
		}
		
		dungeonSizeCounter=dungeonSizeCounter+1;
	}
	player.transform.position=new Vector3(playerSpawnPosX,playerSpawnPosY,playerSpawnPosZ);	
}

	public void GenSnakeDungeon(){
		int roomSizeI = Random.Range(roomSizeIlow,roomSizeImax);
		int roomSizeJ = Random.Range(roomSizeJlow,roomSizeJmax);
		int doorPlaceI =  Random.Range(0,roomSizeJ);
		if(doorPlaceI==roomSizeI){
			doorPlaceI=doorPlaceI-1;
		}	
		int dootPlaceJ =  Random.Range(0,roomSizeI);
		if(dootPlaceJ==roomSizeJ){
			dootPlaceJ=dootPlaceJ-1;
		}	 
		int oldDoorPlaceI = 0;
		int oldDoorPlaceJ = 0;
		int groundTransforamX = groundTransX;
		int groundTransforamZ = groundTransZ;
		int dungeonSize = Random.Range(5,maxDungeonSize);
		int dungeonSizeCounter = 0;
		bool isDoorPlaceX = isDoorPlaceXorZ();
		while(dungeonSizeCounter<dungeonSize){
			for(int i = 0; i<roomSizeI;i++){
				for(int j = 0; j<roomSizeJ;j++){
					if (ground.Length > 0)
					{
						GameObject toClean = Instantiate(ground[Random.Range(0, ground.Length)],
							new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
								groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean);
					}

					if (hasRoof)
					{
						GameObject toClean31 = Instantiate(roofs[Random.Range(0,roofs.Length)], new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY+sizeOfInt,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean31);
					}

					if(dungeonSizeCounter>=2&&SetRangeChance(enemyRange)&&enemies.Length>0){
						GameObject toClean1 =	Instantiate(enemies[Random.Range(0,enemies.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 0, 0));
						objectsToCleanDungeon.Add(toClean1);
					}		
					
					
					
					if(isDoorPlaceX){
						if(j==roomSizeJ-1&&i!=dootPlaceJ){ 	
							GameObject toClean2 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						if(i==roomSizeI-1){
							GameObject toClean2 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					else{									
						if(i==roomSizeI-1&&j!=doorPlaceI){
							GameObject toClean2 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						if(j==roomSizeJ-1){ 	
							GameObject toClean2 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}	
					
					if(dungeonSizeCounter!=0){
						if(i==0&&j!=oldDoorPlaceI){
							GameObject toClean2 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						if(j==0&&i!=oldDoorPlaceJ){ 	
							GameObject toClean2 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					else{
						if(j==0){ 	
							GameObject toClean2 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						if(i==0){
							GameObject toClean2 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(0+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ-2+(i*sizeOfInt)) , Quaternion.Euler(0, 180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//environmentToWalls
					if(SetRangeChance(wallEnvRange)&&environmentToWall.Length>0){
						 if(j==0&&i!=oldDoorPlaceJ){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-1.4f+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(j==roomSizeJ-1&&i!=dootPlaceJ){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(1.4f+(j*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlaceI){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSizeI-1&&j!=doorPlaceI){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
					if(SetRangeChance(lampsRange)&&lamps.Length>0){
						if(j==0&&i!=oldDoorPlaceJ&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-1.9f+(j*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(j==roomSizeJ-1&&i!=dootPlaceJ&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(1.9f+(j*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,-90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlaceI&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f+transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,0, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSizeI-1&&j!=doorPlaceI&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f+transformY,1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
					
					//Columns
					if(SetRangeChance(columnRange)&&columns.Length>0){
						if(j==0&&i!=oldDoorPlaceJ){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						
						else if(j==roomSizeJ-1&&i!=dootPlaceJ){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==0&&j!=oldDoorPlaceI){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+groundTransforamX,transformY,-2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
						else if(i==roomSizeI-1&&j!=doorPlaceI){
							GameObject toClean2 = Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-1.9f+(j*sizeOfFloat)+groundTransforamX,transformY,2f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					//furniture
					if(SetRangeChance(furnitureRange)&&furniture.Length>0){
						if(j!=0&&i!=0&&i!=roomSizeI-1&&j!=roomSizeJ-1){
							GameObject toClean2 = Instantiate(furniture[Random.Range(0,furniture.Length)],new Vector3((j*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.identity);
							objectsToCleanDungeon.Add(toClean2);
						}
					}
					
				}
			}
			if(dungeonSizeCounter!=dungeonSize-1){
				if(isDoorPlaceX){
					int tunelSizeX =  Random.Range(1,10);
					for(int m = 0; m<tunelSizeX;m++){
						if (columns.Length > 0)
						{
							GameObject toCleanTun1 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(-1.9f + (roomSizeJ * sizeOfInt) + (m * sizeOfInt) + groundTransforamX,
									transformY, -2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ),
								Quaternion.Euler(0, 90, 0));
							GameObject toCleanTun2 = Instantiate(columns[Random.Range(0, columns.Length)],
								new Vector3(-1.9f + (roomSizeJ * sizeOfInt) + (m * sizeOfInt) + groundTransforamX,
									transformY, 2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ),
								Quaternion.Euler(0, 90, 0));
							objectsToCleanDungeon.Add(toCleanTun1);
							objectsToCleanDungeon.Add(toCleanTun2);
						}

						if (ground.Length > 0)
						{
							GameObject toCleanTun3 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX,
									transformY, (dootPlaceJ * sizeOfFloat) + groundTransforamZ),
								Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toCleanTun3);
						}

						if (hasRoof)
						{
							GameObject toCleanTun31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
								new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX, transformY+sizeOfInt,
									(dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toCleanTun31);
						}

						GameObject toCleanTunsizeOfInt = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSizeJ*sizeOfFloat)+(m*sizeOfFloat)+groundTransforamX,transformY,-2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						GameObject toCleanTun5 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3((roomSizeJ*sizeOfFloat)+(m*sizeOfFloat)+groundTransforamX,transformY,2f+(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
						
						
						objectsToCleanDungeon.Add(toCleanTunsizeOfInt);
						objectsToCleanDungeon.Add(toCleanTun5);
					}
					groundTransforamX=(roomSizeJ*sizeOfInt)+(tunelSizeX*sizeOfInt)+groundTransforamX;
					
					//				oldDoorPlaceI=16;
				}
				else{
					int tunelSize =  Random.Range(1,6);
					for(int n = 0; n<tunelSize;n++){
						if(columns.Length>0){
						GameObject toCleanTun1 =Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(-2f+doorPlaceI*sizeOfInt+groundTransforamX,transformY,-1.9f+(roomSizeI*sizeOfFloat)+(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0,180, 0));
						GameObject toCleanTun2 =	Instantiate(columns[Random.Range(0,columns.Length)],new Vector3(2f+doorPlaceI*sizeOfInt+groundTransforamX,transformY,-1.9f+(roomSizeI*sizeOfFloat)+(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0,180, 0));
						objectsToCleanDungeon.Add(toCleanTun1);
						objectsToCleanDungeon.Add(toCleanTun2);
						}

						if (ground.Length > 0)
						{
							GameObject toCleanTun3 = Instantiate(ground[Random.Range(0, ground.Length)],
								new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY,
									(roomSizeI * sizeOfFloat) + (n * sizeOfInt) + groundTransforamZ),
								Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toCleanTun3);
						}

						if (hasRoof)
						{
							GameObject toCleanTun31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
								new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY+sizeOfInt,
									(roomSizeI * sizeOfFloat) + (n * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
							objectsToCleanDungeon.Add(toCleanTun31);
						}

						GameObject toCleanTunsizeOfInt =	Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,(roomSizeI*sizeOfFloat)+(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
						GameObject toCleanTun5 =Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2f+(doorPlaceI*sizeOfInt)+groundTransforamX,transformY,(roomSizeI*sizeOfFloat)+(n*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
						
						objectsToCleanDungeon.Add(toCleanTunsizeOfInt);
						objectsToCleanDungeon.Add(toCleanTun5);
					}
					groundTransforamZ=(roomSizeI*sizeOfInt)+(tunelSize*sizeOfInt)+groundTransforamZ;
					
				}
			}
			else{
				if(isDoorPlaceX){
					GameObject toCleanwall1 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(-2f+(roomSizeJ*sizeOfFloat)+groundTransforamX,transformY,(dootPlaceJ*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 90, 0));
					objectsToCleanDungeon.Add(toCleanwall1);
				}
				else{
					GameObject toCleanwall1 = Instantiate(walls[Random.Range(0,walls.Length)],new Vector3(doorPlaceI*sizeOfFloat+groundTransforamX,transformY,-2f+(roomSizeI*sizeOfFloat)+groundTransforamZ) , Quaternion.Euler(0, 180, 0));
					objectsToCleanDungeon.Add(toCleanwall1);
				}
			}
			
			if(isDoorPlaceX){
				oldDoorPlaceJ=dootPlaceJ;
				oldDoorPlaceI=-1;
			}
			else{
				oldDoorPlaceI=doorPlaceI;
				oldDoorPlaceJ=-1;
			}
			
			roomSizeI = Random.Range(sizeOfInt,9);	
			
			roomSizeJ = Random.Range(sizeOfInt,9);	
			
			while(roomSizeI<=oldDoorPlaceJ){
				roomSizeI = Random.Range(sizeOfInt,9);
			}		
			doorPlaceI =  Random.Range(0,roomSizeJ);
			if(doorPlaceI==roomSizeJ){
				doorPlaceI=doorPlaceI-1;
			}
			
			while(roomSizeJ<=oldDoorPlaceI){
				roomSizeJ = Random.Range(sizeOfInt,9);
			}	
			dootPlaceJ =  Random.Range(0,roomSizeI);
			if(dootPlaceJ==roomSizeI){
				dootPlaceJ=dootPlaceJ-1;
			}
			
			isDoorPlaceX = isDoorPlaceXorZ();
			dungeonSizeCounter=dungeonSizeCounter+1;
		}
		player.transform.position=new Vector3(playerSpawnPosX,playerSpawnPosY,playerSpawnPosZ);	
	}

	

}
