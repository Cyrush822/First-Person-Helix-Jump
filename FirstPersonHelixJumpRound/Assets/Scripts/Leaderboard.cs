using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Leaderboard : MonoBehaviour {
    public LeaderboardEntry[] leaderBoardEntries; //0 = most recent, 3 = least recent
    public string dateLastRecorded;
    public KeyCode protect;
    public KeyCode protect1;
    public KeyCode protect2;
    public KeyCode protect3;
    public KeyCode protect4;
    public bool protectOff = false;

    public GameObject adminModeText;
    public bool editMode;
    public KeyCode editModeOn;
    public KeyCode editModeOn1;
    public GameObject editModeTextCheck;


    // Use this for initialization
    void Start () {
        print("faefhaf" + DateTime.Parse("7/1/2019 00:00:00", System.Globalization.CultureInfo.InvariantCulture).AddDays(21));
        print("faefhaf2" + DateTime.Today);
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.O) && protectOff && !editMode)
        {
            resetDate();
            print("reset date");
        }
        if(Input.GetKey(protect))
        {
            if (Input.GetKey(protect1))
            {
                if (Input.GetKey(protect2))
                {
                    if (Input.GetKey(protect3))
                    {
                        if (Input.GetKeyDown(protect4))
                        {
                            protectOff = true;
                            adminModeText.SetActive(true);
                        }
                    }
                }
            }
        }
        if(Input.GetKey(editModeOn))
        {
            if(Input.GetKeyDown(editModeOn1) && protectOff)
            {
                editModeTextCheck.SetActive(!editModeTextCheck.activeSelf);
                editMode = !editMode;
            }
        }
        if (Input.GetKey(KeyCode.N) && protectOff && !editMode)
        {
            EraseAll();
            print("hm");
        }
    }
    public bool GetProtectOff()
    {
        return protectOff;
    }
    public void TurnOn()
    {
        this.gameObject.SetActive(true);
    }

    public void SetNewLeaderBoardEntry(int score, string name, int MLGmodeOn = 0)
    {
        int entriesLengthMinusOne = leaderBoardEntries.Length - 1;
        int[] tempScores = new int[entriesLengthMinusOne];
        int[] tempWeeks = new int[entriesLengthMinusOne];
        string[] tempNames = new string[entriesLengthMinusOne];
        for (int index = 0; index < entriesLengthMinusOne; index++)
        {
            tempScores[index] = leaderBoardEntries[index].GetScoreValue();
        }
        for (int index = 0; index < entriesLengthMinusOne; index++)
        {
            tempWeeks[index] = leaderBoardEntries[index].GetWeekValue();
        }
        for (int index = 0; index < entriesLengthMinusOne; index++)
        {
            tempNames[index] = leaderBoardEntries[index].GetNameValue();
        }

        for (int index = 0; index < entriesLengthMinusOne; index++)
        {
            leaderBoardEntries[index + 1].SetWeekNameScore(tempWeeks[index], tempNames[index], tempScores[index], leaderBoardEntries[index].MLGOn);
        }
        leaderBoardEntries[0].SetWeekNameScore(tempWeeks[0] + 1, name, score, MLGmodeOn == 1);
        UpdateAllEntries();

        /*GameObject[] tempEntries = new GameObject[leaderBoardEntries.Length];
        for (int i = 0; i < leaderBoardEntries.Length - 1; i++)
        {
            tempEntries[i+1] = leaderBoardEntries[i];
            tempEntries[i+1].GetComponent<LeaderboardEntry>().Code = i+1;
        }
        tempEntries[0] = tempEntries[3];
        tempEntries[0].GetComponent<LeaderboardEntry>().Code = 0;
        tempEntries[0].GetComponent<LeaderboardEntry>().SetWeekNameScore(tempEntries[1].GetComponent<LeaderboardEntry>().savedWeek + 1, name, score);
        leaderBoardEntries = tempEntries;
        UpdateAllEntries();*/
    }

    public void UpdateAllEntries()
    {
        foreach(LeaderboardEntry entry in leaderBoardEntries)
        {
            entry.updateEntry();
        }
    }
    void test()
    {
        SetNewLeaderBoardEntry(50, "cyrus");
    }
    void test2()
    {
        SetNewLeaderBoardEntry(1000, "sixth");
    }
    void test3()
    {
        SetNewLeaderBoardEntry(2000, "seventh");
    }
    void test4()
    {
        SetNewLeaderBoardEntry(3000, "eight");
    }
    void test5()
    {
        SetNewLeaderBoardEntry(4000, "ninth");
    }

    public bool CheckIfWeekPassed()
    {//month day year
        dateLastRecorded = SaveSystem.GetString("lastDate", "1/7/2019");//third monday of the year 2019
        print("NONCONVERTED: " + dateLastRecorded);
        //DateTime dLRconverted = DateTime.Parse(dateLastRecorded, );
        DateTime dLRconverted = DateTime.Parse(dateLastRecorded, System.Globalization.CultureInfo.InvariantCulture);
        DateTime RecordDateAfterOneWeek = dLRconverted.AddDays(7);
        print("ADDED: " + RecordDateAfterOneWeek);
        print("TODAY: " + DateTime.Today);
        if(DateTime.Compare(RecordDateAfterOneWeek, DateTime.Today) > 0)//recorddate is later
        {
            //print(DateTime.Today);
            //print(RecordDateAfterOneWeek);
            return false;
        }
        else if (DateTime.Compare(RecordDateAfterOneWeek, DateTime.Today) <= 0)//recorddate is later
        {
            //print(DateTime.Today);
            //print(RecordDateAfterOneWeek);
            while (DateTime.Compare(dLRconverted, DateTime.Today.AddDays(-6)) <= 0)//adds 7 days to tempDate until it's the most recent monday. AddDay() works if there's a new year.
            {
                dLRconverted = dLRconverted.AddDays(7);
            }
            SaveSystem.SetString("lastDate", dLRconverted.ToString("MM/dd/yyyy"));
            print("CONVERTED: " + SaveSystem.GetString("lastDate"));
            return true;
        }
        else
        {
            print("SAMTHANG WAN WRON");
            return false;
        }

    }
    public void resetDate()
    {
        dateLastRecorded = "1/7/2019";
        SaveSystem.SetString("lastDate", dateLastRecorded);
    }

    public void EraseAll()
    {
        //int pNumber = SaveSystem.GetInt("2playerNumberSaved", 0);
        //SaveSystem.SetInt("2playerNumberSaved", pNumber);
    }


}
