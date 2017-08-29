using UnityEngine;
using UnityEngine.UI;
public class SceneManager : MonoBehaviour {

    [Range(0f,1f)]
    public float timeToFade;

    private GameObject mainUI;

    private GameObject FadePanel;
    private bool fading;
    private string sceneToLoad;

    public void Start()
    {
        FadePanel = GameObject.FindGameObjectWithTag("Fade");
        mainUI = GameObject.FindWithTag("UI");
        sceneToLoad = "";
    }

    public void LoadScene(string scene)
    {
        sceneToLoad = scene;
        mainUI.SetActive(false);
    }

    public void FadeOut()
    {
        Debug.Log(FadePanel.GetComponent<Image>().color.a);
        if (sceneToLoad != "")
        {
            if (FadePanel.GetComponent<Image>().color.a >= 0.98f)
            {
                Debug.Log("Done!");
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
                sceneToLoad = "";
                FadePanel.GetComponent<Image>().color = new Color(FadePanel.GetComponent<Image>().color.r, FadePanel.GetComponent<Image>().color.g, FadePanel.GetComponent<Image>().color.b, 1);
                fading = true;
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
            FadePanel.GetComponent<Image>().color = new Color(FadePanel.GetComponent<Image>().color.r, FadePanel.GetComponent<Image>().color.g, FadePanel.GetComponent<Image>().color.b, 0);
            fading = false;
        } else
        {
            FadePanel.GetComponent<Image>().color = new Color(FadePanel.GetComponent<Image>().color.r,
                    FadePanel.GetComponent<Image>().color.g, FadePanel.GetComponent<Image>().color.b,
                    Vector3.Lerp(new Vector3(FadePanel.GetComponent<Image>().color.a, 0, 0),
                    new Vector3(0, 0, 0), timeToFade).x);
            fading = true;
        }

    }

    public void FixedUpdate()
    {
        FadeOut();
        if (fading)
        {
            FadeIn();
        }
    }

}
