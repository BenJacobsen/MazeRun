using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Menu { Main, GameSetup, MainSettings, InGameSettings, Pause, EndGame, None}

//MenuController keeps track of current menu, menu switching and menu displaying
public class MenuController : MonoBehaviour {

    public Menu CurrentMenu;
    private GameObject MainPanel;
    private GameObject GameSetupPanel;
    private GameObject PausePanel;
    private GameObject MainSettingsPanel;
    private GameObject InGameSettingsPanel;
    private GameObject EndGamePanel;
    private Camera MenuCamera;
    private Menu NewMenu;

	void Start () {
        NewMenu = Menu.Main;
        CurrentMenu = Menu.Main;
        MenuCamera = GameObject.Find("MenuCamera").GetComponent<Camera>();
        MainPanel = GameObject.Find("MainMenuPanel");

        GameSetupPanel = GameObject.Find("GameSetupPanel");
        GameSetupPanel.SetActive(false);

        PausePanel = GameObject.Find("PausePanel");
        PausePanel.SetActive(false);

        MainSettingsPanel = GameObject.Find("MainSettingsPanel");
        MainSettingsPanel.SetActive(false);

        InGameSettingsPanel = GameObject.Find("InGameSettingsPanel");
        InGameSettingsPanel.SetActive(false);

        EndGamePanel = GameObject.Find("EndGamePanel");
        EndGamePanel.SetActive(false);
    }

	void Update () {
        if (CurrentMenu == Menu.None || CurrentMenu == Menu.Pause ||
            CurrentMenu == Menu.InGameSettings)
        {
            MenuCamera.enabled = false;
        }
        else
        {
            MenuCamera.enabled = true;

        }
            if (NewMenu != CurrentMenu)
        {
            if (CurrentMenu != Menu.None)
            {
                GetMenu(CurrentMenu).SetActive(false);
            }
            if (NewMenu != Menu.None)
            {
                GetMenu(NewMenu).SetActive(true);
            }
            CurrentMenu = NewMenu;
        }

        if (NewMenu == Menu.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown("escape"))
        {
            EscapePress();
        }
    }

    public void SetEndScreen (bool isWinnerPlayer)
    {
        ChangeMenuTo("EndGame");
    }

    private GameObject GetMenu(Menu menu)
    {
        switch (menu)
        {
            case Menu.Main:
                return MainPanel;
            case Menu.GameSetup:
                return GameSetupPanel;
            case Menu.Pause:
                return PausePanel;
            case Menu.MainSettings:
                return MainSettingsPanel;
            case Menu.InGameSettings:
                return InGameSettingsPanel;
            case Menu.EndGame:
                return EndGamePanel;
            default:
                return new GameObject();

        }
    }

    private Menu GetMenuByString(string menu)
    {
        switch (menu)
        {
            case "Main":
                return Menu.Main;
            case "GameSetup":
                return Menu.GameSetup;
            case "Pause":
                return Menu.Pause;
            case "MainSettings":
                return Menu.MainSettings;
            case "InGameSettings":
                return Menu.InGameSettings;
            case "EndGame":
                return Menu.EndGame;
            case "None":
                return Menu.None;
            default:
                return Menu.None;

        }
    }

    public void ChangeMenuTo (string nextMenuString)  //string?
    {
        Menu nextMenu = GetMenuByString(nextMenuString);
        if (NewMenu == nextMenu)
        {
            return;
        }
        NewMenu = nextMenu;
    }

    private void EscapePress ()
    {
        if (NewMenu == Menu.None)
        {
            ChangeMenuTo("Pause");
        }
        else if (NewMenu == Menu.Pause)
        {
            ChangeMenuTo("None");
        }
    }

}
