using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// TODO: You must set the values for the enum
public enum GameMode
{
    idle,
    playing,
    levelEnd

}


// TODO: implement the MissionDemolition script
public class MissionDemolition : MonoBehaviour
{
    static public MissionDemolition S; // a Singleton
                                       // fields set in the Unity Inspector pane
    public GameObject[] castles; // An array of the castles
    public Text UIText_Level; // The GT_Level GUIText
    public Text UIText_Shots; // The GT_Score GUIText
    public Vector3 castlePos; // The place to put castles
    public bool _____________________________;
    // fields set dynamically
    public int level; // The current level
    public int levelMax; // The number of levels
    public int shotsTaken;
    public GameObject castle; // The current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Slingshot"; // FollowCam mode





    // Use this for initialization
    void Start()
    {
        S = this; // Define the Singleton
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        // Get rid of the old castle if one exists
        if (castle != null)
        {
            Destroy(castle);
        }
        // Destroy old projectiles if they exist
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }
        // Instantiate the new castle
        castle = Instantiate(castles[level]) as GameObject;
        castle.transform.position = castlePos;
        shotsTaken = 0;
        // Reset the camera
        SwitchView("Both");
        ProjectileLine.S.Clear();
        // Reset the goal
        Goal.goalMet = false;
        ShowGT();
        mode = GameMode.playing;

    }

    void UpdateGUI()
    {
        // Show the data in the GUITexts
        UIText_Level.text = "Level: " + (level + 1) + " of " + levelMax;
        UIText_Shots.text = "Shots Taken: " + shotsTaken;
    }

    void ShowGT()
    {
        // Show the data in the GUITexts
        UIText_Level.text = "Level: " + (level + 1) + " of " + levelMax;
        UIText_Shots.text = "Shots Taken: " + shotsTaken;
    }
    void Update()
    {
        ShowGT();
        // Check for level end
        if (mode == GameMode.playing && Goal.goalMet)
        {
            // Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            // Zoom out
            SwitchView("Both");
            // Start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }

    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    void OnGUI()
    {
        // Draw the GUI button for view switching at the top of the screen
        Rect buttonRect = new Rect((Screen.width / 2) - 50, 10, 100, 24);
        switch (showing)
        {
            case "Slingshot":
                if (GUI.Button(buttonRect, "Show Castle"))
                {
                    SwitchView("Castle");
                }
                break;
            case "Castle":
                if (GUI.Button(buttonRect, "Show Both"))
                {
                    SwitchView("Both");
                }
                break;
            case "Both":
                if (GUI.Button(buttonRect, "Show Slingshot"))
                {
                    SwitchView("Slingshot");
                }
                break;
        }
    }

    public void SwitchView(string eView = "")
    {
        S.showing = eView;
        switch (S.showing)
        {
            case "Slingshot":
                FollowCam.poi = null;
                break;
            case "Castle":
                FollowCam.poi = S.castle;
                break;
            case "Both":
                FollowCam.poi = GameObject.Find("ViewBoth");
                break;
        }
    }

    // Static method that allows code anywhere to increment shotsTaken
    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
