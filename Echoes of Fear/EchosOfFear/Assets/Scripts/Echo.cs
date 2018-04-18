using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{

    public float time = 2.0f;
    public GameObject echo;

    private bool disableEcho = false;
    public float disableDuration = 7.0f;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!disableEcho)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject echoCopy = Instantiate(echo, transform.position + (transform.forward * 2), transform.rotation);
                Destroy(echoCopy, time);
                disableEcho = true;
                Invoke("EnableEcho", disableDuration);
            }
        }
    }

    void EnableEcho()
    {
        disableEcho = false;
    }
}
