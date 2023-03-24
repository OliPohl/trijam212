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
        HideAll();
        windowsStartupVideo = windowsStartup.GetComponent<VideoPlayer>();
        windowsStartupVideo.Prepare();
        windowsStartup.SetActive(true);
    }


    void Update() 
    {
        if(gameFinished)
        {
            HideAll();
            outro.SetActive(true);
        }
        else if(gameReset)
        {
           HideAll(); 
           windowsStartup.SetActive(true);
           gameReset = false;
        }
        else if(!(windowsStartupVideo.isPlaying))
        {
            HideAll();
            windowsXP.SetActive(true);
        }
    }


    private void HideAll()
    {
        windowsStartup.SetActive(false);
        windowsXP.SetActive(false);
        outro.SetActive(false);
    }


    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}

