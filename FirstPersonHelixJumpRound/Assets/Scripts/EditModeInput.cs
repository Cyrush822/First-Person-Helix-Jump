using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditModeInput : MonoBehaviour {
    GameObject leaderboardEntryToEdit;
    bool occupied;
    int week;
    string name;
    int score;
    bool MLG;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(GameObject leaderboardEntry)
    {
        if(!occupied)
        {
            leaderboardEntryToEdit = leaderboardEntry;
            occupied = true;
        }
    }

    public void ApplyNewEntry()
    {
        Text[] Childrentext = gameObject.GetComponentsInChildren<Text>();
        foreach(Text Ct in Childrentext)
        {
            if(Ct.gameObject.name == "WeekText")
            {
                week = int.Parse(Ct.text);
            }
            if (Ct.gameObject.name == "NameText")
            {
                name = Ct.text;
            }
            if (Ct.gameObject.name == "ScoreText")
            {
                score = int.Parse(Ct.text);
            }
            if (Ct.gameObject.name == "MLGText")
            {
                if(Ct.text == "1")
                {
                    MLG = true;
                }
                else
                {
                    MLG = false;
                }
            }
        }
        leaderboardEntryToEdit.GetComponent<LeaderboardEntry>().SetWeekNameScore(week, name, score, MLG);
        leaderboardEntryToEdit.GetComponent<LeaderboardEntry>().updateEntry();
        occupied = false;
        this.gameObject.SetActive(false);
    }
}
