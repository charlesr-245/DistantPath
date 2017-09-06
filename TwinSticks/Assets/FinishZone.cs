using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour {

    private GameObject ManagementScripts;

    private bool finished;
    private float countdown;
    private float countdownTime = 3f;

    private void Start()
    {
        ManagementScripts = GameObject.Find("ManagementScripts");
    }

    private void Update()
    {
        if (finished)
        {
            countdown += Time.deltaTime;
        }
        if (countdown >= countdownTime && finished)
        {
            finished = false;
            ManagementScripts.GetComponent<SceneManager>().FinishLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAll") || other.gameObject.layer == LayerMask.NameToLayer("Player1") || other.gameObject.layer == LayerMask.NameToLayer("Player2"))
        {
            other.GetComponentInParent<PlayerManager>().DisableAll();
            finished = true;
        }
    }


}
