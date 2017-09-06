using UnityEngine;
public class GameManagerMisc : MonoBehaviour {

    private static GameObject Manager;

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

}
