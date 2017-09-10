using UnityEngine;

public class GameManagerMisc : MonoBehaviour {

    private static GameObject Manager;

    private bool playBack;

    private void Awake()
    {
        //Make sure there's only one instance of the GameManager
        if (Manager == null)
        {
            DontDestroyOnLoad(gameObject);
            Manager = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckPlayback();
        CheckPause();
    }

    void CheckPause()
    {
        if (Input.GetButtonDown("Pause") && Time.timeScale != 0)
        {
            Pause();
            
        }
        else if (Time.timeScale == 0 && Input.GetButtonDown("Pause"))
        {
            Resume();
        }
    }

    void Pause()
    {
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
    }

    void Resume()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.001f;
    }

    private void OnApplicationPause()
    {
        Pause();
    }

    private void OnApplicationFocus(bool focus)
    {
        Resume();
    }


    public bool PlayBack()
    {
        return playBack;
    }

    private void CheckPlayback()
    {
        if (Input.GetButton("Replay"))
        {
            playBack = true;
        }
        else
        {
            playBack = false;
        }
    }
}
