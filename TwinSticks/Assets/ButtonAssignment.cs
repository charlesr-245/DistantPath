using UnityEngine.UI;
using UnityEngine;

public class ButtonAssignment : MonoBehaviour {


    private GameObject GameManager;
	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("GameManager");
        GetComponent<Button>().onClick.AddListener(StartGame);
	}
	
    void StartGame()
    {
        GameManager.GetComponentInChildren<SceneManager>().LoadScene("level1");
    }
}
