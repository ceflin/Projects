using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 0.0f;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * moveSpeed;

        horizontal *= Time.deltaTime;
        vertical *= Time.deltaTime;

        transform.Translate(horizontal, 0, vertical);
    }
}
