// Jason Johansson
/*
 * from Brackeys on Youtube @
 * https://www.youtube.com/watch?v=JivuXdrIHK0
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseMenu;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   //needs to be updated for Android
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        isPaused = false;
    }
    void Pause()
    {
        PauseMenu.SetActive(true);
        isPaused = true;
    }
    public void ExitToHub()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("HUB");
    }

    public void Disconnect() //needs support
    {
        Debug.Log("Disconnecting");     
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
