using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject safePlatform;
	public GameObject harmfulPlatform;
	public GameObject blankPlatform;
	public GameObject Pillar;
	float Deg = 0f;
	float PlatHeight= -1f;
	float PlatInterval = 2f;
	int[] spawnList = new int[16];
	bool cancelNextBlank = false; //all spaces in the platforms are 2 gaps wide, and so spawning 2 blank "sensors" gaps would mean the sphere gets double points for jumping in between. Thus, I created 1 big 2 gap wide sensor, and I only need to it to spawn once, because it fills up 2 spaces.
	public int initialPlatformSpawn = 50;

	public List<GameObject> harmfulPlatList = new List<GameObject> ();
	public int ListNumber = 1;
	public int MovingPlatNumberBottom;
	public int MovingPlatNumberTop;
	public bool changedTop = false;
	public bool changedBottom = false;
	public bool platformMovable = true;



	public int difficulty = 0;
	private int b1;//1 = nothing, 2 = unsafe, 3 = safe
	private int b2;
	private int b3;
    private int b4;
    private int b5;

	private int h1;
	private int h2;
	private int h3;
	private int h4;
	private int h5;
	private int h6;
	private int h7;

    public GameObject wall;
    public float wallSpawnChance1;
    public float wallSpawnChance2;
    public float[] wallHeights;
    public float wallMinAngularDistance;
	public PlayerController PlayerController;

    float wallSpawnChance = 33f;

    public bool[] checks = new bool[5];
	// Use this for initialization
	void Start () {
		for (int pos = 0; pos < spawnList.Length; pos++) {
			spawnList [pos] = 3;
		}
        //for (int b = 0; b < initialPlatformSpawn; b++)
        //{
        //    SpawnPlatform();
        //}

		PlayerController = GetComponent<PlayerController> ();

//for setting all values to 3 in the array

		/*for (int pos = 0; pos < spawnList.Length; pos++) {
			print ("number: " + pos + " is " + spawnList [pos]);
		}//for checking the values in each of the array
		*/
		harmfulPlatList.Add(harmfulPlatform);
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void initialSpawn()
    {
        for (int b = 0; b < initialPlatformSpawn; b++)
        {
            SpawnPlatform();
        }
    }
    public void ForceDifficultyChange()
    {
        difficulty = 0;
    }

    public void UnforceDifficultyChange()
    {
        difficulty = 0;
    }
    private void CheckDifficulty()
    {
    
        if (PlayerController.points >= 5 && PlayerController.points < 40 && !checks[0])
        {
            difficulty = Mathf.Clamp(difficulty += 1, 0, 5);
            checks[0] = true;
        }
        if (PlayerController.points >= 40 && PlayerController.points < 70 && !checks[1])
        {
            difficulty = Mathf.Clamp(difficulty += 1, 0, 5);
            if (difficulty == 5)
            {
                wallSpawnChance = 67f;
            }
            checks[1] = true;
        }

        if (PlayerController.points >= 70 && PlayerController.points < 120 && !checks[2])
        {
            difficulty = Mathf.Clamp(difficulty += 1, 0, 5);
            if (difficulty == 5)
            {
                wallSpawnChance = 67f;
            }
            checks[2] = true;
        }

        if (PlayerController.points >= 120 && PlayerController.points < 200 && !checks[3])
        {
            difficulty = Mathf.Clamp(difficulty += 1, 0, 5);
            if(difficulty == 5)
            {
                wallSpawnChance = 67f;
            }
            checks[3] = true;
        }
        if (PlayerController.points >= 200 && !checks[4])
        {
            difficulty = Mathf.Clamp(difficulty += 1, 0, 5);
            if (difficulty == 5)
            {
                wallSpawnChance = 67f;
            }
            checks[4] = true;
        }
    
    }

    public void SpawnPlatform ()
	{
        CheckDifficulty();
        int rand = Random.Range(0, 100);
		PlatHeight = PlatHeight - PlatInterval;
		Randomizer (spawnList);
		for (int test = 0; Deg < 360f; Deg = Deg + 22.5f, test++) {//creates a platform at PlatHeight, with rotation equal to the for statement, which will spawn one in 22.5 degrees intervals, stops at 360 degrees. Creates a total of 16 "pie slices" 
			if (spawnList [test] == 1 && cancelNextBlank == false) {
				//Instantiate (blankPlatform, new Vector3 (0, PlatHeight, -1), Quaternion.Euler (new Vector3 (-90, Deg + 11.25f, 0)));
				//cancelNextBlank = true;
			}
			else if (spawnList [test] == 1 && cancelNextBlank == true) {
				//cancelNextBlank = false;
			}
			else if (spawnList [test] == 2) { 
				GameObject badplat = Instantiate (harmfulPlatform, new Vector3 (0, PlatHeight, -1), Quaternion.Euler (new Vector3 (0, Deg, 0)));
				//print ("working");
				if (difficulty >= 4 && rand < wallSpawnChance) {
                    MovingPlatNumberBottom = ((test + 16) - 1) % 16;
					//print ("AfterNumberBottom" + MovingPlatNumberBottom);

					MovingPlatNumberTop = ((test + 16) + 1) % 16;
                    //print ("beforeNumberTop" + MovingPlatNumberTop);
					//print ("AfterNumberTop" + MovingPlatNumberTop);
                    int MovingPlatNumberBottom2 = ((test + 16) - 3) % 16;
                    int MovingPlatNumberTop2 = ((test + 16) + 3) % 16;
                    if (spawnList [MovingPlatNumberBottom] == 1 && platformMovable == true) { 
						platformMovable = false;
						//print ("working5");
						badplat.GetComponent<HarmfulPlatRotation> ().Direction = 2;
                        if(spawnList[MovingPlatNumberTop] == 1)
                        {
                            badplat.GetComponent<HarmfulPlatRotation>().Direction = 3;
                        }
                        if (spawnList[MovingPlatNumberBottom2] == 1)
                        {
                            badplat.GetComponent<HarmfulPlatRotation>().rotationrange *= 2;
                        }
                    
					}
					if (spawnList [MovingPlatNumberTop] == 1 && platformMovable == true) { 
						platformMovable = false;
						//print ("working6");
						badplat.GetComponent<HarmfulPlatRotation> ().Direction = 1;

                        if (spawnList[MovingPlatNumberTop2] == 1)
                        {
                            badplat.GetComponent<HarmfulPlatRotation>().rotationrange *= 2;
                        }
                    }
                    spawnList[test] = 5;//5 means its moving
				}
						
			
			}
          
            //}
            else if (spawnList [test] == 3) {
				Instantiate (safePlatform, new Vector3 (0f, PlatHeight, -1f), Quaternion.Euler (new Vector3 (0f, Deg, 0f)));
			}

		}
        if (difficulty == 5)
        {
            int wallsToSpawn = CheckForSpawnWall();
            if (wallsToSpawn == 1)
            {
                float wallRotate = CalculateWallRotation(spawnList);
                float wallHeightMultiplier = WallHeightMultiplierCalculate();
                SpawnWall(new Vector3(0f, PlatHeight, -1f), new Vector3(0, wallRotate, 0), wallHeightMultiplier);
            }
            if (wallsToSpawn == 2)
            {
                float wallRotate = CalculateWallRotation(spawnList);//spawn first wall
                float wallHeightMultiplier = WallHeightMultiplierCalculate();
                SpawnWall(new Vector3(0f, PlatHeight, -1f), new Vector3(0, wallRotate, 0), wallHeightMultiplier);


                float wallRotate2 = CalculateWallRotation(spawnList);//spawn second wall
                while (!WallsAreNotTooClose(wallRotate, wallRotate2))
                {
                    wallRotate2 = CalculateWallRotation(spawnList);
                }
                float wallHeightMultiplier2 = WallHeightMultiplierCalculate();
                SpawnWall(new Vector3(0f, PlatHeight, -1f), new Vector3(0, wallRotate2, 0), wallHeightMultiplier2);
            }
        }
        Instantiate (Pillar, new Vector3(0f, PlatHeight, -1f), transform.rotation);
		Instantiate (blankPlatform, new Vector3(0f, PlatHeight, -1f), Quaternion.Euler (new Vector3 (-90, 0f, 0f)));

		Deg = 0f;
		changedTop = false;
		changedBottom = false;

		for (int pos = 0; pos < spawnList.Length; pos++) {
			spawnList [pos] = 3;
		}
		platformMovable = true;
	}

	int[] Randomizer (int[] spawnList)
	{
		b1 = Random.Range (0, 15);
		spawnList[b1] = 1;
		if (b1 != 15) {
			spawnList [b1 + 1] = 1;//if it is anything but 16, position number b1 and b1+1 (double spaced blanks), will be blank
		} else {
			spawnList [b1 - 1] = 1;//if it is 16, position number b1 and b1-1 (double spaced blanks), will be blank (must be pos16 and pos15)
		}

		b2 = Random.Range (0, 15);
		while(spawnList[b2] != 3 || spawnList[b2+1] !=3)
		{
			b2 = Random.Range (0, 15);//this prevents b2 to be the same as b1, creating repeat values. It also prevents it from being next to b1, so it won't have double spaced platforms
		}
		spawnList [b2] = 1;
		if (b2 != 15) {
			spawnList [b2 + 1] = 1;//if it is anything but 16, position number b2 and b2+1 (double spaced blanks), will be blank
		} else {
			spawnList [b2 - 1] = 1;//if it is 16, position number b2 and b2-1 (double spaced blanks), will be blank (must be pos16 and pos15)
		}

		if (difficulty == 0) {
			b3 = Random.Range (0, 15);
			while(spawnList[b3] != 3 || spawnList[b3+1] !=3)
			{
				b3 = Random.Range (0, 15);//this prevents b2 to be the same as b1, creating repeat values. It also prevents it from being next to b1, so it won't have double spaced platforms
			}
			spawnList [b3] = 1;
			if (b3 != 15) {
				spawnList [b3 + 1] = 1;//if it is anything but 16, position number b2 and b2+1 (double spaced blanks), will be blank
			} else {
				spawnList [b3 - 1] = 1;//if it is 16, position number b2 and b2-1 (double spaced blanks), will be blank (must be pos16 and pos15)
			}

		}

        if (difficulty >= 1) 
		{
			h1 = Random.Range (0, 16);
			while (spawnList [h1] != 3) 
			{
				h1 = Random.Range (0, 16);
			}
			spawnList [h1] = 2;


			h2 = Random.Range (0, 16);
			while (spawnList [h2] != 3) 
			{
				h2 = Random.Range (0, 16);
			}
			spawnList [h2] = 2;

			h3 = Random.Range (0, 16);

			while (spawnList [h3] != 3) 
			{
				h3 = Random.Range (0, 16);
			}
			spawnList [h3] = 2;


			if (difficulty >= 2) 
			{
				h4 = Random.Range (0, 16);
				while (spawnList [h4] != 3) 
				{
					h4 = Random.Range (0, 16);
				}
				spawnList [h4] = 2;

			}
			if (difficulty >= 3) 
			{
				h5 = Random.Range (0, 16);
				while (spawnList [h5] != 3) 
				{
					h5 = Random.Range (0, 16);
				}
				spawnList [h5] = 2;
			}
			if (difficulty >= 4) {
				h6 = Random.Range (0, 16);
				while (spawnList [h6] != 3) 
				{
					h6 = Random.Range (0, 16);
				}
				spawnList [h6] = 2;
			}

		}
			

		return(spawnList);
	}

    int CheckForSpawnWall()
    {
        float rand = Random.Range(0, 100);
        if(rand < wallSpawnChance1)
        {
            float rand2 = Random.Range(0, 100);
            if (rand2 < wallSpawnChance2)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 0;
        }
    }

    float WallHeightMultiplierCalculate()
    {
        int rand = Random.Range(0, wallHeights.Length - 1);
        return (wallHeights[rand]);
    }

    void SpawnWall(Vector3 Wallposition, Vector3 Wallrotation, float heightMultiplier)
    {
        GameObject spawnedWall = Instantiate(wall, Wallposition, Quaternion.Euler(Wallrotation));
        spawnedWall.gameObject.transform.localScale = new Vector3(spawnedWall.transform.localScale.x, spawnedWall.transform.localScale.y * heightMultiplier, spawnedWall.transform.localScale.z);
    }

    float CalculateWallRotation(int[] SpawnList)
    {
        int platNumber = Random.Range(0, 15);
        while(spawnList[platNumber] == 1 || spawnList[platNumber] == 5)
        {
            platNumber = Random.Range(0, 15);
        }
        float PlatNumberRotation = platNumber * 22.5f;
        float rotationRangeMin = PlatNumberRotation - 11.25f;
        float rotationRangeMax = PlatNumberRotation + 11.25f;
        return Random.Range(rotationRangeMin, rotationRangeMax);
    }
    bool WallsAreNotTooClose(float rotation1, float rotation2)
    {
        float angleDifference = Mathf.DeltaAngle(rotation1, rotation2);
        return Mathf.Abs(angleDifference) >= wallMinAngularDistance;
    }
	/*IEnumerator Spawning ()
	{
		for (int a = 0; a < 50; a++) {
			SpawnPlatform ();
			yield return new WaitForSeconds (0.1f);
		}
	}*/ //this is for spawning 50 platforms in 5 seconds
}

/*if (difficulty == 0)
        {
            b4 = Random.Range(0, 15);
            while (spawnList[b4] != 3 || spawnList[b4 + 1] != 3)
            {
                b4 = Random.Range(0, 15);//this prevents b2 to be the same as b1, creating repeat values. It also prevents it from being next to b1, so it won't have double spaced platforms
            }
            spawnList[b4] = 1;
            if (b4 != 15)
            {
                spawnList[b4 + 1] = 1;//if it is anything but 16, position number b2 and b2+1 (double spaced blanks), will be blank
            }
            else
            {
                spawnList[b4 - 1] = 1;//if it is 16, position number b2 and b2-1 (double spaced blanks), will be blank (must be pos16 and pos15)
            }

        }
        if (difficulty == 0)
        {
            b5 = Random.Range(0, 15);
            while (spawnList[b5] != 3 || spawnList[b5 + 1] != 3)
            {
                b5 = Random.Range(0, 15);//this prevents b2 to be the same as b1, creating repeat values. It also prevents it from being next to b1, so it won't have double spaced platforms
            }
            spawnList[b5] = 1;
            if (b5 != 15)
            {
                spawnList[b5 + 1] = 1;//if it is anything but 16, position number b2 and b2+1 (double spaced blanks), will be blank
            }
            else
            {
                spawnList[b5 - 1] = 1;//if it is 16, position number b2 and b2-1 (double spaced blanks), will be blank (must be pos16 and pos15)
            }

        }*/
