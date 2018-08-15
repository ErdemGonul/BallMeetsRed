using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public  class SettingsHandler : MonoBehaviour {
    public GameObject coinText;

    public bool soundOn;
    public bool musicOn;
    public bool vibrateOn;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    public void loadGame(int index)
    {
        //applySettings();
        SceneManager.LoadScene(index);
    }

    public  void vibrationOn()
    {
        bool vibrateOn = PlayerPrefs.GetInt("vibration") == 1 ? true : false;
        if (vibrateOn)
        {
           
            if (SceneManager.GetActiveScene().name == "settings")
            {
              
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "vibration off";
                PlayerPrefs.SetInt("vibration", 0);
            }
        }
        else
        {

            
            if (SceneManager.GetActiveScene().name == "settings")
            {
                EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "vibration on";
                PlayerPrefs.SetInt("vibration", 1);
            }
        }
       
    }
    public void makeReview()
    {
        Application.OpenURL("market://details?id=" + "com.ErdemGonul.BallMeetsAir");
        
    }
    public void reportBug()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text.EndsWith("g"))
        {
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "ruppygames@gmail.com";

        }
        else
        {
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "report bug";
        }
    }
    public void showLeaderBoards()
    {
        GooglePlayScript.ShowLeaderBoardUI();
    }
}
