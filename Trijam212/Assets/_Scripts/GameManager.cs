using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject windowsStartup;
    public GameObject windowsXP;
    public GameObject outro;

    private VideoPlayer windowsStartupVideo;
    private VideoPlayer windowsXPVideo;
    private VideoPlayer outroVideo;


    private int gameState = 0;
    private int currentGameState = 0;
    public bool gameFinished = false;
    public bool gameReset = false;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() 
    {
        ShowAll(true);

        windowsStartupVideo = windowsStartup.GetComponent<VideoPlayer>();
        windowsXPVideo = windowsXP.GetComponentInChildren<VideoPlayer>();
        outroVideo = outro.GetComponent<VideoPlayer>();

        windowsStartupVideo.Prepare();
        windowsXPVideo.Prepare();
        outroVideo.Prepare();
    }


    void Update() 
    {
        ChangeGamestate();

        if(gameState != currentGameState)
        {
            switch(gameState)
            {
                case 3:
                    outro.SetActive(true);
                    outroVideo.Play();
                    break;
                case 2:
                    windowsXP.SetActive(true);
                    windowsXPVideo.Play();
                    break;
                case 1:
                    windowsStartup.SetActive(true);
                    windowsStartupVideo.Play();
                    break;
                default:
                    // ShowAll(false);
                    break;
            }
            currentGameState = gameState;
        }
    }


    private void ChangeGamestate()
    {
        if(gameState == 0 && windowsStartupVideo.isPrepared && windowsXPVideo.isPrepared && outroVideo.isPrepared)
        {
            Debug.Log("Videos are prepared. Switching to WindowsStartup.");
            ShowAll(false);
            gameState = 1;
        }
        else if(gameState == 1 && windowsStartupVideo.isPaused)
        {
            Debug.Log("WindowsStartup Video finished. Switching to WindowsXP.");
            ShowAll(false);
            gameState = 2;
        }
        else if(gameFinished)
        {
            Debug.Log("Game won. Switching to Outro.");
            ShowAll(false);
            gameState = 3;
        }
        else if(gameState == 2 && gameReset)
        {
            Debug.Log("Game lost. Switching to WindowsStartup.");
            ShowAll(false);
            gameReset = false;
            gameState = 1;
        }
    }


    private void ShowAll(bool io)
    {
        windowsStartup.SetActive(io);
        windowsXP.SetActive(io);
        outro.SetActive(io);
    }


    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}

