using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityDungeon : MonoBehaviour
{
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
	public GameObject player;
	public int transformY = 0;
	public int groundTransZ = 0;
	public int groundTransX = 0;
	private bool canCreateDungeon = true;
	private bool canDestroyObjects = false;
	private float destroyObjectTimer = 0f;

	public List<Vector3> tunelVector3Saver;

	// Use this for initialization
	private int entranceDoorPlaceI = -1;
	private int entranceDoorPlaceJ = -1;
	private int entranceDoorPlace = -1;
	private int pastRoomSizeI = 0;
	private int pastRoomSizeJ = 0;
	public List<GameObject> objectsToClean;


	private List<Tunnel> tunrelsToClean = new List<Tunnel>();

	private List<Tunnel> environmentToClean = new List<Tunnel>();

//	public Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
	private int generatedDungeonNumber = 0;

	public int roomSizeIlow = 4;
	public int roomSizeImax = 10;
	public int roomSizeJlow = 4;
	public int roomSizeJmax = 10;

	public int
		enemyRange =
			10; // 1/10 chance to spawn enemy for available floor position. if enemy Range is for example equals 2, than there are 1/2 chance that floor has an enemy  

	public int furnitureRange = 2;
	public int columnRange = 2;


	public bool hasRoof = false;
	public float playerSpawnPosY = 1;
	public float playerSpawnPosX = 1;
	public float playerSpawnPosZ = 1;
	public float sizeOfFloat = 4;
	public int sizeOfInt = 4;

	public List<GameObject> objForDisEffect;
	public GameObject disEffect;
	
	void Start()
	{
		GenInfinityDungeon(groundTransX, groundTransZ, true, -1, -1, -1, -1, -1);
//		tunelVector3Saver[0].Set(0, 0, 0);
	}

	private void transformPosition(GameObject thisObject)
	{
		thisObject.transform.Translate(Vector3.up * 90); 
	}

	void Update()
	{
		if (canCreateDungeon)
		{
			if (V3Equal(player.transform.position, tunelVector3Saver[1]) ||
			    V3Equal(player.transform.position, tunelVector3Saver[2]))
			{
				
				for (int i = 0; i < objectsToClean.Count; i++)
				{
//					objForDisEffect.Add(Instantiate(objectsToClean[i]));
					if (disEffect != null&&i%1==0)
					{
						Instantiate(disEffect,
							new Vector3(objectsToClean[i].transform.position.x, objectsToClean[i].transform.position.y,
								objectsToClean[i].transform.position.z),Quaternion.Euler(0, 0, 0));
					}
					Destroy(objectsToClean[i]);
				}

				canCreateDungeon = false;
				tunelVector3Saver.Clear();
				objectsToClean.Clear();

				GenInfinityDungeon(groundTransX, groundTransZ, false, entranceDoorPlaceI, entranceDoorPlaceJ,
					entranceDoorPlace, pastRoomSizeI, pastRoomSizeJ);
				for (int j = 0; j < tunrelsToClean.Count; j++)
				{
					if (tunrelsToClean[j].tunnelCounter.Equals(generatedDungeonNumber - 2))
					{
//						objForDisEffect.Add(Instantiate(tunrelsToClean[j].tunnelPart));
						Destroy(tunrelsToClean[j].tunnelPart);
					}

				}


				for (int k = 0; k < environmentToClean.Count; k++)
				{
					if (environmentToClean[k].tunnelCounter.Equals(generatedDungeonNumber - 1))
					{
//						objForDisEffect.Add(Instantiate(environmentToClean[k].tunnelPart));
						Destroy(environmentToClean[k].tunnelPart);
					}
				}
			}
		}


	}

	bool SetRangeChance(int range)
	{
		return Random.Range(0, range) == 0;
	}


	public void GenInfinityDungeon(int groundTransforamX, int groundTransforamZ, bool canReplacePlayerPosition,
		int entranceDoorPlaceI, int entranceDoorPlaceJ, int entranceDoorPlace, int pastRSizeI, int pastRSizeJ)
	{
		generatedDungeonNumber += 1;
		int roomSizeI = Random.Range(roomSizeIlow, roomSizeImax);
		int roomSizeJ = Random.Range(roomSizeJlow, roomSizeJmax);
		int doorPlaceI = Random.Range(0, roomSizeJ);
		if (doorPlaceI == roomSizeI)
		{
			doorPlaceI = doorPlaceI - 1;
		}

		int dootPlaceJ = Random.Range(0, roomSizeI);
		if (dootPlaceJ == roomSizeJ)
		{
			dootPlaceJ = dootPlaceJ - 1;
		}

		if (entranceDoorPlace == 2)
		{
			if (entranceDoorPlaceJ >= roomSizeI && entranceDoorPlaceJ >= 0)
			{
				groundTransforamZ = sizeOfInt + groundTransforamZ + (entranceDoorPlaceJ - roomSizeI) * sizeOfInt;
				entranceDoorPlaceJ = roomSizeI - 1;
			}

			if (pastRSizeJ > roomSizeJ || pastRSizeJ < roomSizeJ)
			{
				groundTransforamX = groundTransforamX - (roomSizeJ - pastRSizeJ) * sizeOfInt; //+++++++++++
			}

		}
		else if (entranceDoorPlace == 3)
		{
			if (pastRSizeI > roomSizeI || pastRSizeI < roomSizeI)
			{
				groundTransforamZ = groundTransforamZ - (roomSizeI - pastRSizeI) * sizeOfInt; //+++++++++++++
			}

			if (entranceDoorPlaceI >= roomSizeJ && entranceDoorPlaceI >= 0)
			{
				groundTransforamX = sizeOfInt + groundTransforamX + (entranceDoorPlaceI - roomSizeJ) * sizeOfInt;
				entranceDoorPlaceI = roomSizeJ - 1;
			}

		}
		else
		{
			if (entranceDoorPlace == 0)
			{
				if (entranceDoorPlaceJ >= roomSizeI && entranceDoorPlaceJ >= 0)
				{
					groundTransforamZ = sizeOfInt + groundTransforamZ + (entranceDoorPlaceJ - roomSizeI) * sizeOfInt;
					entranceDoorPlaceJ = roomSizeI - 1;
				}
			}

			if (entranceDoorPlace == 1)
			{
				if (entranceDoorPlaceI >= roomSizeJ && entranceDoorPlaceI >= 0)
				{
					groundTransforamX = sizeOfInt + groundTransforamX + (entranceDoorPlaceI - roomSizeJ) * sizeOfInt;
					entranceDoorPlaceI = roomSizeJ - 1;
				}
			}
		}

		int oldDoorPlaceI = 0;
		int oldDoorPlaceJ = 0;
		int nextDoorSide = Random.Range(0, 4); // 0 - West, 1 South, 2 Ost, 3 North
		while (entranceDoorPlace == nextDoorSide)
		{
			nextDoorSide = Random.Range(0, 4); // 0 - West, 1 South, 2 Ost, 3 North
		}

//		int nextDoorSide = 2; // 0 - West, 1 South, 2 Ost, 3 North
		for (int i = 0; i < roomSizeI; i++)
		{
			for (int j = 0; j < roomSizeJ; j++)
			{
				if (ground.Length > 0)
				{
					GameObject toClean = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
							groundTransforamZ + (i * sizeOfInt)),
						Quaternion.Euler(0, 0, 0));
					objectsToClean.Add(toClean);
				}

				if (hasRoof)
				{
					GameObject toClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY + sizeOfInt, groundTransforamZ + (i * sizeOfInt)),
						Quaternion.Euler(0, 0, 0));
					objectsToClean.Add(toClean31);
				}

				//Enemy Spawner
				if (generatedDungeonNumber >= 2 && SetRangeChance(enemyRange) && enemies.Length > 0)
				{
					Instantiate(enemies[Random.Range(0, enemies.Length)],
						new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfInt)),
						Quaternion.Euler(0, 0, 0));
				}

				if (entranceDoorPlace == -1)
				{
					if (nextDoorSide == 0)
					{
						if (j == 0 && i != dootPlaceJ)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfInt)),
								Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClean3);
						}

						if (i == roomSizeI - 1)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClean3);
						}

						if (i == 0)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClean3);
						}

						if (j == roomSizeJ - 1)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfInt)),
								Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClean3);
						}
					}
					else if (nextDoorSide == 1)
					{
						if (j == 0)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfInt)),
								Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClean3);
						}

						if (i == roomSizeI - 1)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClean3);
						}

						if (i == 0 && j != doorPlaceI)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClean3);
						}

						if (j == roomSizeJ - 1)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfInt)),
								Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClean3);
						}
					}
					else if (nextDoorSide == 2)
					{
						if (j == 0)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfInt)),
								Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClean3);
						}

						if (i == roomSizeI - 1)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClean3);
						}

						if (i == 0)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClean3);
						}

						if (j == roomSizeJ - 1 && i != dootPlaceJ)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfInt)),
								Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClean3);
						}
					}
					else if (nextDoorSide == 3)
					{
						if (j == 0)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfInt)),
								Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClean3);
						}

						if (i == roomSizeI - 1 && j != doorPlaceI)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClean3);
						}

						if (i == 0)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClean3);
						}

						if (j == roomSizeJ - 1)
						{
							GameObject toClean3 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfInt)),
								Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClean3);
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
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1 && i != entranceDoorPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 0 && entranceDoorPlace == 3)
					{
						if (j == 0 && i != dootPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1 && j != entranceDoorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 0 && entranceDoorPlace == 1)
					{
						if (j == 0 && i != dootPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0 && j != entranceDoorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 1 && entranceDoorPlace == 3)
					{
						if (j == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1 && j != entranceDoorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0 && j != doorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 1 && entranceDoorPlace == 2)
					{
						if (j == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0 && j != doorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1 && i != entranceDoorPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 1 && entranceDoorPlace == 0)
					{
						if (j == 0 && i != entranceDoorPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0 && j != doorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 2 && entranceDoorPlace == 0)
					{
						if (j == 0 && i != entranceDoorPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1 && i != dootPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 2 && entranceDoorPlace == 1)
					{
						if (j == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0 && j != entranceDoorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1 && i != dootPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 2 && entranceDoorPlace == 3)
					{
						if (j == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1 && j != entranceDoorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1 && i != dootPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 3 && entranceDoorPlace == 1)
					{
						if (j == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1 && j != doorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0 && j != entranceDoorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 3 && entranceDoorPlace == 2)
					{
						if (j == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1 && j != doorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1 && i != entranceDoorPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
					else if (nextDoorSide == 3 && entranceDoorPlace == 0)
					{
						if (j == 0 && i != entranceDoorPlaceJ)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(-2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == roomSizeI - 1 && j != doorPlaceI)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (i == 0)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(0 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ - 2 + (i * sizeOfInt)), Quaternion.Euler(0, 180, 0));
							objectsToClean.Add(toClea12);
						}

						if (j == roomSizeJ - 1)
						{
							GameObject toClea12 = Instantiate(walls[Random.Range(0, walls.Length)],
								new Vector3(2 + (j * sizeOfInt) + groundTransforamX, transformY,
									groundTransforamZ + (i * sizeOfInt)), Quaternion.Euler(0, 90, 0));
							objectsToClean.Add(toClea12);
						}
					}
				}
					
					if(SetRangeChance(furnitureRange)&&environmentToWall.Length>0){
						 if(j==0&&i!=entranceDoorPlaceJ){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-1.4f+(j*sizeOfInt)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfInt)) , Quaternion.Euler(0,90, 0));
							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
						}
						else if(j==roomSizeJ-1&&i!=dootPlaceJ){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(1.4f+(j*sizeOfFloat)+groundTransforamX,transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
						}
						else if(i==0&&j!=entranceDoorPlaceI){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,-1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
						}
						else if(i==roomSizeI-1&&j!=doorPlaceI){
							GameObject toClean2 = Instantiate(environmentToWall[Random.Range(0,environmentToWall.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,transformY,1.4f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
						}
					}
					
					if(SetRangeChance(furnitureRange)&&lamps.Length>0){
						if(j==0&&i!=entranceDoorPlaceJ&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-1.9f+(j*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,90, 0));
							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
						}
						else if(j==roomSizeJ-1&&i!=dootPlaceJ&&i%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(1.9f+(j*sizeOfInt)+groundTransforamX,1.7f+transformY,groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,-90, 0));
							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
						}
						else if(i==0&&j!=entranceDoorPlaceI&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f+transformY,-1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,0, 0));
							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
						}
						else if(i==roomSizeI-1&&j!=doorPlaceI&&j%3==0){
							GameObject toClean2 = Instantiate(lamps[Random.Range(0,lamps.Length)],new Vector3(-2f+(j*sizeOfFloat)+groundTransforamX,1.7f+transformY,1.9f+groundTransforamZ+(i*sizeOfFloat)) , Quaternion.Euler(0,180, 0));
							environmentToClean.Add(new Tunnel(generatedDungeonNumber,toClean2));
						}
					}
				//Columns
				if (SetRangeChance(columnRange) && columns.Length > 0)
				{
					if (j == 0 && i != entranceDoorPlaceJ)
					{
						GameObject toClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
							new Vector3(-2f + (j * sizeOfFloat) + groundTransforamX, transformY,
								-1.9f + groundTransforamZ + (i * sizeOfFloat)), Quaternion.Euler(0, 90, 0));
						environmentToClean.Add(new Tunnel(generatedDungeonNumber, toClean2));
					}

					else if (j == roomSizeJ - 1 && i != dootPlaceJ)
					{
						GameObject toClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
							new Vector3(2f + (j * sizeOfFloat) + groundTransforamX, transformY,
								-1.9f + groundTransforamZ + (i * sizeOfFloat)), Quaternion.Euler(0, 90, 0));
						environmentToClean.Add(new Tunnel(generatedDungeonNumber, toClean2));
					}
					else if (i == 0 && j != entranceDoorPlaceI)
					{
						GameObject toClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
							new Vector3(-1.9f + (j * sizeOfFloat) + groundTransforamX, transformY,
								-2f + groundTransforamZ + (i * sizeOfFloat)), Quaternion.Euler(0, 180, 0));
						environmentToClean.Add(new Tunnel(generatedDungeonNumber, toClean2));
					}
					else if (i == roomSizeI - 1 && j != doorPlaceI)
					{
						GameObject toClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
							new Vector3(-1.9f + (j * sizeOfFloat) + groundTransforamX, transformY,
								2f + groundTransforamZ + (i * sizeOfFloat)), Quaternion.Euler(0, 180, 0));
						environmentToClean.Add(new Tunnel(generatedDungeonNumber, toClean2));
					}
				}

				//furniture
				if (SetRangeChance(furnitureRange) && furniture.Length > 0)
				{
					if (j != 0 && i != 0 && i != roomSizeI - 1 && j != roomSizeJ - 1)
					{
						GameObject toClean2 = Instantiate(furniture[Random.Range(0, furniture.Length)],
							new Vector3((j * sizeOfFloat) + groundTransforamX, transformY, groundTransforamZ + (i * sizeOfFloat)),
							Quaternion.identity);
						environmentToClean.Add(new Tunnel(generatedDungeonNumber, toClean2));
					}
				}

			}
		}

		if (nextDoorSide == 0)
		{
			int tunelSizeX = Random.Range(3, 8);
			for (int m = 0; m < tunelSizeX; m++)
			{
				if (columns.Length > 0)
				{
					GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-5.9f - (m * sizeOfInt) + groundTransforamX, transformY,
							-2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
					GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-5.9f - (m * sizeOfInt) + groundTransforamX, transformY,
							2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean1));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean2));
				}

				if (ground.Length > 0)
				{
					GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(-sizeOfFloat - (m * sizeOfFloat) + groundTransforamX, transformY,
							(dootPlaceJ * sizeOfFloat) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean3));

				}

				if (hasRoof)
				{
					GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3(-sizeOfFloat - (m * sizeOfFloat) + groundTransforamX, transformY + sizeOfInt,
							(dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean31));
				}

				GameObject tunelToCleansizeOfInt = Instantiate(walls[Random.Range(0, walls.Length)],
					new Vector3(-sizeOfFloat - (m * sizeOfFloat) + groundTransforamX, transformY,
						-2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));
				GameObject tunelToClean5 = Instantiate(walls[Random.Range(0, walls.Length)],
					new Vector3(-sizeOfFloat - (m * sizeOfFloat) + groundTransforamX, transformY,
						2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));

				tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToCleansizeOfInt));
				tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean5));

				tunelVector3Saver.Add(new Vector3(-sizeOfFloat - (m * sizeOfFloat) + groundTransforamX, transformY,
					(dootPlaceJ * sizeOfFloat) + groundTransforamZ));
			}

			groundTransX = -(roomSizeJ * sizeOfInt) - (tunelSizeX * sizeOfInt) + groundTransforamX;
			groundTransZ = groundTransforamZ;
			this.entranceDoorPlaceJ = dootPlaceJ;
			this.entranceDoorPlace = 2;
			canCreateDungeon = true;


			//				oldDoorPlaceI=16;
		}
		else if (nextDoorSide == 1)
		{
			int tunelSize = Random.Range(3, 8);
			for (int n = 0; n < tunelSize; n++)
			{
				if (columns.Length > 0)
				{
					GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							-5.9f - (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));
					GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(2f + doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							-5.9f - (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean1));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean2));
				}

				if (ground.Length > 0)
				{
					GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							-sizeOfFloat - (n * sizeOfInt) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean3));

				}

				if (hasRoof)
				{
					GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY + sizeOfInt,
							-sizeOfFloat - (n * sizeOfInt) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean31));
				}

				GameObject tunelToCleansizeOfInt = Instantiate(walls[Random.Range(0, walls.Length)],
					new Vector3(2f + (doorPlaceI * sizeOfInt) + groundTransforamX, transformY,
						-sizeOfFloat - (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
				GameObject tunelToClean5 = Instantiate(walls[Random.Range(0, walls.Length)],
					new Vector3(-2f + (doorPlaceI * sizeOfInt) + groundTransforamX, transformY,
						-sizeOfFloat - (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));

				tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToCleansizeOfInt));
				tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean5));

				tunelVector3Saver.Add(new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY,
					-sizeOfFloat - (n * sizeOfInt) + groundTransforamZ));
			}

			groundTransZ = -(roomSizeI * sizeOfInt) - (tunelSize * sizeOfInt) + groundTransforamZ;
			groundTransX = groundTransforamX;
			this.entranceDoorPlaceI = doorPlaceI;
			this.entranceDoorPlace = 3;
			canCreateDungeon = true;


		}
		else if (nextDoorSide == 2)
		{
			int tunelSizeX = Random.Range(3, 8);
			for (int m = 0; m < tunelSizeX; m++)
			{
				if (columns.Length > 0)
				{
					GameObject tunelToClean1 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-1.9f + (roomSizeJ * sizeOfInt) + (m * sizeOfInt) + groundTransforamX, transformY,
							-2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
					GameObject tunelToClean2 = Instantiate(columns[Random.Range(0, columns.Length)],
						new Vector3(-1.9f + (roomSizeJ * sizeOfInt) + (m * sizeOfInt) + groundTransforamX, transformY,
							2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean1));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean2));
				}

				if (ground.Length > 0)
				{
					GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX, transformY,
							(dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean3));

				}

				if (hasRoof)
				{
					GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX, transformY + sizeOfInt,
							(dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean31));
				}

				GameObject tunelToCleansizeOfInt = Instantiate(walls[Random.Range(0, walls.Length)],
					new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX, transformY,
						-2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));
				GameObject tunelToClean5 = Instantiate(walls[Random.Range(0, walls.Length)],
					new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX, transformY,
						2f + (dootPlaceJ * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 180, 0));

				tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToCleansizeOfInt));
				tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean5));

				tunelVector3Saver.Add(new Vector3((roomSizeJ * sizeOfFloat) + (m * sizeOfFloat) + groundTransforamX, transformY,
					(dootPlaceJ * sizeOfFloat) + groundTransforamZ));
			}

			groundTransX = (roomSizeJ * sizeOfInt) + (tunelSizeX * sizeOfInt) + groundTransforamX;
			groundTransZ = groundTransforamZ;
			this.entranceDoorPlaceJ = dootPlaceJ;
			this.entranceDoorPlace = 0;
			canCreateDungeon = true;


			//				oldDoorPlaceI=16;
		}
		else if (nextDoorSide == 3)
		{
			int tunelSize = Random.Range(3, 8);
			for (int n = 0; n < tunelSize; n++)
			{
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
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean1));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean2));
				}

				if (ground.Length > 0)
				{
					GameObject tunelToClean3 = Instantiate(ground[Random.Range(0, ground.Length)],
						new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY,
							(roomSizeI * sizeOfFloat) + (n * sizeOfInt) + groundTransforamZ),
						Quaternion.Euler(0, 0, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean3));

				}

				if (hasRoof)
				{
					GameObject tunelToClean31 = Instantiate(roofs[Random.Range(0, roofs.Length)],
						new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY + sizeOfInt,
							(roomSizeI * sizeOfFloat) + (n * sizeOfInt) + groundTransforamZ), Quaternion.Euler(0, 0, 0));
					tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean31));
				}

				GameObject tunelToCleansizeOfInt = Instantiate(walls[Random.Range(0, walls.Length)],
					new Vector3(2f + (doorPlaceI * sizeOfInt) + groundTransforamX, transformY,
						(roomSizeI * sizeOfFloat) + (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));
				GameObject tunelToClean5 = Instantiate(walls[Random.Range(0, walls.Length)],
					new Vector3(-2f + (doorPlaceI * sizeOfInt) + groundTransforamX, transformY,
						(roomSizeI * sizeOfFloat) + (n * sizeOfFloat) + groundTransforamZ), Quaternion.Euler(0, 90, 0));

				tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToCleansizeOfInt));
				tunrelsToClean.Add(new Tunnel(generatedDungeonNumber, tunelToClean5));

				tunelVector3Saver.Add(new Vector3(doorPlaceI * sizeOfInt + groundTransforamX, transformY,
					(roomSizeI * sizeOfFloat) + (n * sizeOfInt) + groundTransforamZ));
			}

			groundTransZ = (roomSizeI * sizeOfInt) + (tunelSize * sizeOfInt) + groundTransforamZ;
			groundTransX = groundTransforamX;
			this.entranceDoorPlaceI = doorPlaceI;
			this.entranceDoorPlace = 1;
			canCreateDungeon = true;

		}

		pastRoomSizeI = roomSizeI;
		pastRoomSizeJ = roomSizeJ;
		if (canReplacePlayerPosition)
		{
			player.transform.position = new Vector3(playerSpawnPosX, playerSpawnPosY, playerSpawnPosZ);
		}
	}





	public bool Approximately(Vector3 me, Vector3 other, float percentage)
	{
		var dx = me.x - other.x;
		if (Mathf.Abs(dx) > me.x * percentage)
			return false;

		var dy = me.y - other.y;
		if (Mathf.Abs(dy) > me.y * percentage)
			return false;

		var dz = me.z - other.z;

		return Mathf.Abs(dz) >= me.z * percentage;
	}

	public bool V3Equal(Vector3 a, Vector3 b)
	{
		return Vector3.SqrMagnitude(a - b) < 2;
	}
	
}