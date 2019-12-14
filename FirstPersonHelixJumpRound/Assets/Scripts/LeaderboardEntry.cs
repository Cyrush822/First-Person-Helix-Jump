using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardEntry : MonoBehaviour {
    public TextMeshProUGUI weekCount;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerScore;

    public int savedWeek;
    public string savedName;
    public int savedScore;
    public bool MLGOn;
    public int Code;//required. Input number in array (top is 0, bottom should be 3)
    // Use this for initialization
    public EditModeInput EDI;
    public Leaderboard lb;
    private void Awake()
    {
        lb = FindObjectOfType<Leaderboard>();
        AssignVars();
        MLGOn = SaveSystem.GetInt("savedMLG", 0) == 1;
        updateEntry();
    }

    void Start ()
    {
        //DummyValues();
    }


    private void AssignVars()
    {
        TextMeshProUGUI[] TMProGUIs = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI checkTM in TMProGUIs)
        {
            if (checkTM.gameObject.tag == "Name")
            {
                playerName = checkTM;
            }
            else if (checkTM.gameObject.tag == "Week")
            {
                weekCount = checkTM;
            }
            else if (checkTM.gameObject.tag == "Score")
            {
                playerScore = checkTM;
            }
            else
            {
                print("TMPRO not found");

                break;
            }

        }
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKey(KeyCode.P) && lb.GetProtectOff() && !lb.editMode)
        {
            resetEntry();
        }
        if (Input.GetKey(KeyCode.U) && lb.GetProtectOff() && !lb.editMode)
        {
            resetDate();
        }
        if (MLGOn)
        {
            Color changingColor = Color.HSVToRGB(Mathf.PingPong((Time.time * 1 + 0.5f), 1), 0.75f, 0.75f);
            playerName.color = changingColor;
            weekCount.color = changingColor;
            playerScore.color = changingColor;
        }
    }

    public void SetWeekNameScore(int setWeek, string setName, int setScore, bool MLG = false)
    {
        SaveSystem.SetInt("savedWeek" + Code, setWeek);
        SaveSystem.SetInt("savedScore" + Code, setScore);
        SaveSystem.SetString("savedName" + Code, setName);
        if(MLG)
        {
            SaveSystem.SetInt("savedMLG" + Code, 1);
        }
        else
        {
            SaveSystem.SetInt("savedMLG" + Code, 0);
        }
    } 

    public void updateEntry()
    {
        AssignVars();
        savedWeek = SaveSystem.GetInt("savedWeek" + Code, 0);
        savedName = SaveSystem.GetString("savedName" + Code, "N/A");
        savedScore = SaveSystem.GetInt("savedScore" + Code, 0);
        MLGOn = SaveSystem.GetInt("savedMLG" + Code, 0) == 1;
        playerName.color = Color.white;
        weekCount.color = Color.white;
        playerScore.color = Color.white;
        weekCount.text = "Week\n" + savedWeek;
        playerName.text = savedName;
        playerScore.text = savedScore.ToString();
    }

    public void resetEntry()
    {
        SaveSystem.SetInt("savedScore" + Code, 0);
        SaveSystem.SetString("savedName" + Code, "Reset");
        SaveSystem.SetInt("savedMLG" + Code, 0);
        playerName.color = Color.white;
        weekCount.color = Color.white;
        playerScore.color = Color.white;
        updateEntry();
    }
    public void banEntry()
    {
        SaveSystem.SetInt("savedScore" + Code, 0);
        SaveSystem.SetString("savedName" + Code, "deleted because inappropriate");
        SaveSystem.SetInt("savedMLG" + Code, 0);
        playerName.color = Color.white;
        weekCount.color = Color.white;
        playerScore.color = Color.white;
        updateEntry();
    }
    public void resetDate()
    {
        SaveSystem.SetInt("savedWeek" + Code, 0);
        updateEntry();
    }

    public int GetScoreValue()
    {
        return savedScore;
    }
    public int GetWeekValue()
    {
        return savedWeek;
    }
    public string GetNameValue()
    {
        return savedName;
    }

    public void Clicked()
    {
        Leaderboard ldScript = FindObjectOfType<Leaderboard>();
        if (ldScript.GetProtectOff() && !ldScript.editMode)
        {
            banEntry();
        }
        else if (ldScript.GetProtectOff() && ldScript.editMode)
        {
            EDI.gameObject.SetActive(true);
            EDI.Initialize(gameObject);
        }
       
    }
}
