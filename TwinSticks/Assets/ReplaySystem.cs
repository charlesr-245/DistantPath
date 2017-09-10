using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

    private const int bufferFrames = 100;
    private int maxFrame = -1;
    private int currentFrame = -1;
    private KeyFrame[] keyFrames = new KeyFrame[bufferFrames];

    private Rigidbody rb;
    private GameObject GameManager;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameManager = GameObject.Find("GameManager");
    }

    private void Update()
    {
        PlayBackRecord();
    }

    private void PlayBackRecord()
    {
        currentFrame++;
        if (currentFrame > maxFrame)
        {
            maxFrame = currentFrame;
        }
        if (currentFrame == bufferFrames)
        {
            currentFrame = 0;
        }
        if (GameManager.GetComponent<GameManagerMisc>().PlayBack())
        {
            PlayBack();
        }
        else
        {
            Record();
        }
    }

    void PlayBack()
    {
        rb.isKinematic = true;
        if (currentFrame == maxFrame)
        {
            currentFrame = 0;
        }
        int frame = currentFrame % maxFrame;

        frame = Time.frameCount % maxFrame;
        transform.position = keyFrames[frame].position;
        transform.rotation = keyFrames[frame].rotation;
    }

    private void Record()
    {
        if (rb.isKinematic)
        {
            rb.isKinematic = false;
        }
        float time = Time.time;
        Debug.Log("Writing to frame: " + currentFrame);

        keyFrames[currentFrame] = new KeyFrame(time, transform.position, transform.rotation);
    }
}

/// <summary>
/// A structure for storing time, rotation, and position. Designed to be used for a replay system.
/// </summary>
public struct KeyFrame {
    public float frameTime;
    public Vector3 position;
    public Quaternion rotation;

    public KeyFrame(float time, Vector3 pos, Quaternion rot)
    {
        frameTime = time;
        position = pos;
        rotation = rot;
    }
}
