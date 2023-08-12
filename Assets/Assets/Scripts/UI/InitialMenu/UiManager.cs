using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    private string playerNameTag;
    private string lobbyID;
    
    public void PlayButton() //* This method is executed when the player press the play button
    {
        if (playerNameTag == null)
        {
            // Show error message
        }
        else if (lobbyID != null)
        {
            // Conect to lobby
        }
        else
        {
            // Create Lobby and enter
        }
    }

    public void ExitButton() //* This method is executed when the player press the exit button
    {
        Application.Quit();
    }

    public void ConfigButton() //* This method is executed when the player press the config button
    {
        // Implement config menu
    }

    public void OnNameChanged(string name) //* This method is executed when the player change the name
    {
        playerNameTag = name;
    }

    public void OnLobbyIDChanged(string id) //* This method is executed when the player change the lobby ID
    {
        lobbyID = id;
    }

}
