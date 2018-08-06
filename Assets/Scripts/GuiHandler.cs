using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class GuiHandler : MonoBehaviour
{


    public GameObject inGameMenu;
    public Button pauseButton;
    public Button playButtonMenu;
    public Button restartButtonMenu;
    public GameController controller;

    public Button openButton;
    public GameObject scrollMenu;

    public GameObject bottomMenu;
    public GameObject others;
    public GameObject topBar;
    public Player player;

    public GameObject brakeButton;
    public bool gameStarted = false;
    public bool opened = true;
    public GameObject powerUpPanel;
    public GameObject powerUpButton;
    // Use this for initialization
    void Start()
    {
        openPowerUpMenu();


    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonUp(0) &&!IsPointerOverUIObject() && gameStarted==false)
        {
            StartGame();

        }
    }
    public void StartGame()
    {
        bottomMenu.SetActive(false);
        others.SetActive(false);
        topBar.SetActive(true);
        powerUpButton.SetActive(true);

        gameStarted = true;
    }
    public void loadGame(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void openPowerUpMenu()
    {
              
        if (opened==false)
        {
            powerUpPanel.SetActive(true);
            opened = true;
        }
        else
        {
            powerUpPanel.SetActive(false);
            opened = false;
        }
    }
  


}
