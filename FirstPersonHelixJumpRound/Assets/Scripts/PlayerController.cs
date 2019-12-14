using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController: MonoBehaviour {
	private Rigidbody boi;
	public float jumpForce = 2f;
	public float comboJumpForce = 3f;
	public float jumpInterval = 0f;
	public float jumpTimer;
	public GameObject safePlat;
	public GameObject harmfulPlat; 


	public Canvas gameOver;
	public GameObject Died;
	public GameObject restart;
	public GameObject blackout;
	public Renderer rend;
	public GameObject Player;
	public Rotation rotation;
	public bool restartEnable = false;

	public int points;
	public int highScore ;
	public float pointTimer = 0f;
	public float pointInterval = 0.1f;
    public int pointBase = 1;
    public TextMeshProUGUI point;
	public Text highScoreDisplay;
	public bool groundTouched = true;//for seeing if the ball touched the ground before getting another point
	public int pointEarned = 1;//for adding 1 to whenever the ball doesn't touch the ground before getting hit. 
	public int combo;

	public BounceDestroy bounceDestroy;
	public GameObject bottomSensor;


	public Text pointAddition;
	public float displayPointEarnedTimer = 0f;
	public float displayPointEarnedCoolDown = 0.5f;

	public Spawner spawner;
    public bool alive = true;
    public KeyCode resetHighScore;
    public KeyCode resetHighScoreLvl1;
    public KeyCode resetHighScoreLvl2;
    public KeyCode Quit;
    public KeyCode QuitProtect;
    public KeyCode revealPlayerNumber;
    public int playerNumber = 0;
    public Text playerNumberText;

    public AudioSource myAudio;
    public AudioClip smallBounce;
    public AudioClip bigBounce;

    public string highScoreName;
    public InputField highScoreNameInput;
    public GameObject highScoreNameInputObject;
    public bool setHighScoreName;
    public bool HighscoreMLG;

    public BounceDestroy BDScript;
    public GameObject leaderBoard;

    public float raycastLength;

   
    // Use this for initialization
    void Start () {
        Screen.SetResolution(1600, 900, true);
        HighscoreMLG = (SaveSystem.GetInt("HighMLG", 0) == 1) ;
        highScoreNameInput.characterLimit = 24;
        highScore = SaveSystem.GetInt ("Highscore", 0);
        highScoreName = SaveSystem.GetString("Highscorename", "None");
		HighScoreDisplay ();
        BDScript = GetComponentInChildren<BounceDestroy>();
		boi = GetComponent<Rigidbody>();
		rotation = Player.GetComponent<Rotation> ();
		combo = 1;
		bounceDestroy = bottomSensor.GetComponent<BounceDestroy> ();
		spawner = GetComponent<Spawner> ();
        myAudio = GetComponent<AudioSource>();
        highScoreNameInputObject.SetActive(false);
        playerNumber = PlayerPrefs.GetInt("playerNumberSaved", 0);
        print("Today is " + System.DateTime.Today.ToString());
        //print(System.DateTime.Today.Date.DayOfYear.ToString());
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey (KeyCode.Return) && restartEnable) {
            if(setHighScoreName)
            {
                SaveSystem.SetString("Highscorename", highScoreNameInput.text);
                highScoreNameInput.enabled = false;
                setHighScoreName = false;
            }
			SceneManager.LoadScene ("MainGame");
			restartEnable = false;
            PlayerPrefs.SetInt("playerNumberSaved", playerNumber+1);
            playerNumber = PlayerPrefs.GetInt("playerNumberSaved");
        }
        if(Input.GetKeyDown(revealPlayerNumber)  && alive)
        {
            playerNumberText.text = "Player#" + playerNumber;
            playerNumberText.gameObject.SetActive(!playerNumberText.gameObject.activeSelf);
        }
        if (Input.GetKey(KeyCode.V))
        {
            if (Input.GetKeyDown(KeyCode.L) && alive)
            {
                leaderBoard.SetActive(!leaderBoard.activeSelf);
                print(SaveSystem.GetString("lastDate", "1 / 21 / 2019 00:00:00"));
            }
        }
        if (Time.fixedTime > displayPointEarnedTimer) {
			pointAddition.text = "";
		}

        if(Input.GetKey(resetHighScore))
        {
            if(Input.GetKey(resetHighScoreLvl1))
            {
                if (Input.GetKeyDown(resetHighScoreLvl2))
                {
                    ResetHighScore();
                }
            }
        }
        if(Input.GetKey(QuitProtect))
        {
            if(Input.GetKeyDown(Quit))
            {
                Application.Quit();
            }
        }
        if (HighscoreMLG)
        {
            highScoreDisplay.color = Color.HSVToRGB(Mathf.PingPong((Time.time * 1 + 0.5f), 1), 0.75f, 0.75f);
        }
        boi.velocity = new Vector3(boi.velocity.x, Mathf.Clamp(boi.velocity.y, -15, 1000), boi.velocity.z);
	}

    private void ResetHighScore()
    {
        SaveSystem.SetInt("Highscore", 0);
        SaveSystem.SetString("Highscorename", "none");
        SaveSystem.SetInt("HighMLG", 0);
        highScoreName = SaveSystem.GetString("Highscorename");
        HighScoreDisplay();
        highScore = SaveSystem.GetInt("Highscore");
    }

    void OnCollisionEnter (Collision collision)
    {
        //HitPlatform(collision);

    }

    private void HitPlatform(Collider collision)
    {
       
        if (alive)
        {
            if (jumpTimer < Time.fixedTime)
            {
                if (collision.gameObject.name == "goodTriangle")
                {
                    if (combo > 2)
                    {
                        if (alive)
                        {
                            boi.velocity = new Vector2(0, 0);
                            boi.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        }
                        pointEarned = pointEarned + pointBase;
                        points += pointEarned;
                        point.text = "Points: " + points;
                        displayPointAdditionTest();
                        combo = 1;
                        //BDScript.ActivateDestroy(collision.relativeVelocity.y);
                        BDScript.ActivateDestroy(20);
                        spawner.SpawnPlatform();
                        myAudio.PlayOneShot(bigBounce);
                    }
                    else
                    {
                        if (alive)
                        {
                            boi.velocity = new Vector2(0, 0);
                            boi.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        }
                        myAudio.PlayOneShot(smallBounce);
                    }
                }
                else if (collision.gameObject.name == "badTriangle" || collision.gameObject.name == "badWall")
                {
                    if (combo > 2)
                    {
                        if (alive)
                        {
                            boi.velocity = new Vector2(0, 0);
                            boi.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        }
                        pointEarned = (pointEarned + pointBase) * 2;
                        points += pointEarned;
                        point.text = "Points: " + points;
                        displayPointAdditionTest();
                        combo = 1;
                        //BDScript.ActivateDestroy(collision.relativeVelocity.y);
                        BDScript.ActivateDestroy(20);
                        spawner.SpawnPlatform();
                        myAudio.PlayOneShot(bigBounce);
                    }
                    else
                    {
                        GameOver();
                    }
                }
                if (combo > 2)
                {
                    //print(collision.relativeVelocity.y);
                }
            }
            jumpTimer = Time.fixedTime + jumpInterval;
            groundTouched = true;

        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * raycastLength, Color.white, -8);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, raycastLength))
        {
            print(hit.collider.gameObject);
       
            HitPlatform(hit.collider);
        }
    }

    void GameOver()
	{
        if (alive)
        {
            GetComponent<BoxCollider>().enabled = true;
            Died.SetActive(true);
            restart.SetActive(true);
            blackout.SetActive(true);
            rotation.enabled = false;
            spawner.enabled = false;
            restartEnable = true;
            leaderBoard.SetActive(true);
        }
        Leaderboard leaderboardComp = leaderBoard.GetComponent<Leaderboard>();
        if (points <= highScore && alive)//no high score
        {
            leaderboardComp.TurnOn();
            if (highScore > 0)
            {
                if (leaderboardComp.CheckIfWeekPassed())
                {
                    if (highScoreName == "")
                    {
                        leaderboardComp.SetNewLeaderBoardEntry(highScore, "anon", SaveSystem.GetInt("HighMLG", 0));
                    }
                    else
                    {
                        leaderboardComp.SetNewLeaderBoardEntry(highScore, highScoreName, SaveSystem.GetInt("HighMLG", 0));
                    }
                    ResetHighScore();
                }
            }
            alive = false;
        }
        if (points > highScore && alive) {//yes high score
            if(highScore > 0)
            {
                if (leaderboardComp.CheckIfWeekPassed())
                {
                    if (highScoreName == "")
                    {
                        leaderboardComp.SetNewLeaderBoardEntry(highScore, "anon", SaveSystem.GetInt("HighMLG", 0));
                    }
                    else
                    {
                        leaderboardComp.SetNewLeaderBoardEntry(highScore, highScoreName, SaveSystem.GetInt("HighMLG", 0));
                    }
                }
            }
            leaderBoard.SetActive(false);//don't want to show leaderboard and high score together
            highScore = points;
            SaveSystem.SetInt ("Highscore", highScore);
            if (FindObjectOfType<ChangeTextColor>().MLGMODE)
            {
                SaveSystem.SetInt("HighMLG", 1);
            }
            else
            {
                SaveSystem.SetInt("HighMLG", 0);
            }
            HighScoreDisplay ();
            highScoreNameInputObject.SetActive(true);
            Died.SetActive(false);
            restart.SetActive(false);
            setHighScoreName = true;
        }
        alive = false;
    }

	public void Points()
	{
		if (pointTimer < Time.fixedTime && alive) {
			if (groundTouched == true) {
				pointEarned = pointBase;
				points += pointEarned;
				groundTouched = false;
				combo = 1;
				displayPointAdditionTest ();
			} 
			else
			{
				pointEarned += pointBase;
				points += pointEarned;
				combo += 1;
				displayPointAdditionTest ();

			}
			point.text = "Points: " + points;
		}
		pointTimer = Time.fixedTime + pointInterval;
	}
		

	/*public IEnumerator displayPointAddition()
	{
		pointAddition.text = "+" + pointEarned;
		yield return new WaitForSeconds (1f);
		pointAddition.text = "";
	}*/

	public void displayPointAdditionTest()
	{
		pointAddition.text = "+" + pointEarned;
		displayPointEarnedTimer = Time.fixedTime + displayPointEarnedCoolDown;
	}

	public void HighScoreDisplay()
	{
        if(highScoreName == "")
        {
            highScoreDisplay.text = "highscore: " + highScore + "(anon)";
        }
        else
        {
            highScoreDisplay.text = "highscore: " + highScore + " (" + highScoreName + ")";
        }
	}

    public void doublePointsOn()
    {
        //pointBase = 1;
    }
    public void doublePointsOff()
    {
        pointBase = 1;
    }
}
