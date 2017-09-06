using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    [Range(0f,1f)]
    public float timeToFade;

    private GameObject mainUI;

    private GameObject FadeUI;
    private GameObject FadePanel;
    private bool fading;
    private string sceneToLoad;

    private bool loading;
    private bool onLoad;
    public void Start()
    {
        FadePanel = GameObject.FindGameObjectWithTag("Fade");
        FadeUI = GameObject.Find("LoadFade");
        mainUI = GameObject.FindWithTag("UI");
        sceneToLoad = "";

        FadeUI.SetActive(false);
    }

    public void FinishLevel()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "level1")
        {
            LoadScene("level2");
        } else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "level2")
        {
            LoadScene("level3");
        } else
        {
            LoadScene("MainMenu");
        }
    }

    public void LoadScene(string scene)
    {
        sceneToLoad = scene;
        loading = true;
        if (mainUI)
        {
            mainUI.SetActive(false);
        }
    }

    public void FadeOut()
    {
        //Debug.Log(FadePanel.GetComponent<Image>().color.a);
        if (loading)
        {
            FadeUI.SetActive(true);
            if (FadePanel.GetComponent<Image>().color.a >= 0.98f)
            {
                //Debug.Log("Done!");
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
                FadePanel.GetComponent<Image>().color = new Color(FadePanel.GetComponent<Image>().color.r, FadePanel.GetComponent<Image>().color.g, FadePanel.GetComponent<Image>().color.b, 1);
                fading = true;
                loading = false;
            }
            else
            {
                FadePanel.GetComponent<Image>().color = new Color(FadePanel.GetComponent<Image>().color.r,
                    FadePanel.GetComponent<Image>().color.g, FadePanel.GetComponent<Image>().color.b,
                    Vector3.Lerp(new Vector3(FadePanel.GetComponent<Image>().color.a, 0, 0),
                    new Vector3(1, 0, 0), timeToFade).x);
            }
        }
    }

    public void FadeIn()
    {
        if (FadePanel.GetComponent<Image>().color.a <= 0.03f)
        {
            onLoad = false;
            FadePanel.GetComponent<Image>().color = new Color(FadePanel.GetComponent<Image>().color.r, FadePanel.GetComponent<Image>().color.g, FadePanel.GetComponent<Image>().color.b, 0);
            fading = false;
            FadeUI.SetActive(false);
        } else
        {
            if (!onLoad)
            {
                OnSceneLoad();
                onLoad = true;
            }
            FadePanel.GetComponent<Image>().color = new Color(FadePanel.GetComponent<Image>().color.r,
                    FadePanel.GetComponent<Image>().color.g, FadePanel.GetComponent<Image>().color.b,
                    Vector3.Lerp(new Vector3(FadePanel.GetComponent<Image>().color.a, 0, 0),
                    new Vector3(0, 0, 0), timeToFade).x);
            fading = true;
        }

    }

    void OnSceneLoad()
    {
        GetComponent<GameTimer>().SceneCheck(sceneToLoad);
        GetComponent<GameTimer>().StartTimer();
    }

    public void FixedUpdate()
    {
        FadeOut();
        if (!loading)
        {
            FadeIn();
        }
    }

    public void ReloadScene()
    {
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
