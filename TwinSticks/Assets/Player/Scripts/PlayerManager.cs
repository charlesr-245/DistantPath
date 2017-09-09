using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private bool playerDead;
    public GameObject DeathCamera;
    public GameObject DeathCollider;

    private float countdown = 0;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "deathPlane")
        {
            KillPlayer();
        }
        else if (other.gameObject.tag == "loopLayerChange1")
        {
            gameObject.layer = LayerMask.NameToLayer("Player1");
            foreach(Transform c in gameObject.transform)
            {
                if (c.gameObject.name == "BikeModel")
                {
                    foreach (Transform ch in c.gameObject.transform)
                    {
                        ch.gameObject.layer = LayerMask.NameToLayer("Player1");
                    }
                    return;
                }
            }
        } else if (other.gameObject.tag =="loopLayerChange2")
        {
            gameObject.layer = LayerMask.NameToLayer("Player2");
            foreach (Transform c in gameObject.transform)
            {
                if (c.gameObject.name == "BikeModel")
                {
                    foreach (Transform ch in c.gameObject.transform)
                    {
                        ch.gameObject.layer = LayerMask.NameToLayer("Player2");
                    }
                    return;
                }
            }
        }
    }

    public void DisableAll()
    {
        GetComponent<BikeMotor>().DisableMovement();
        GetComponent<BikeLeaning>().DisableLeaning();
        GameObject.Find("ManagementScripts").GetComponent<GameTimer>().SetPaused(true);
        DisableDeath();
    }

    void DisableDeath()
    {
        DeathCollider.GetComponent<BoxCollider>().isTrigger = false;
    }
    void EnableDeath()
    {
        DeathCollider.GetComponent<BoxCollider>().isTrigger = true;
    }

    public void KillPlayer()

    {
        GameObject.Find("ManagementScripts").GetComponent<GameTimer>().SetPaused(true);
        Destroy(GetComponent<BoxCollider>());
        DisableAll();
        foreach (Transform c in transform)
        {
            if (c.gameObject.name == "BikeModel")
            {
                foreach (Transform ch in c)
                {
                    ch.gameObject.AddComponent<Rigidbody>();
                    ch.gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, transform.position,500f);
                    playerDead = true;
                }
                return;
            }
        }
    }

    public bool GetLifeStatus()
    {
        return !playerDead;
    }

    private void Update()
    {
        if (playerDead && countdown >= 3f)
        {
            
            playerDead = false;
            ReloadScene();
        }
        if (playerDead)
        {
            countdown += Time.deltaTime;
        }
        //Debug.Log(countdown);
    }

    void ReloadScene()
    {
        GetComponent<AnimationManager>().RestartCamera();
        //GameObject.Find("ManagementScripts").GetComponent<SceneManager>().LoadScene("MainMenu");
        GameObject.Find("ManagementScripts").GetComponent<SceneManager>().ReloadScene();
    }
}
