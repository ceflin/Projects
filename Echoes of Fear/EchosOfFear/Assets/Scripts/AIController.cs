using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform target;

    public float moveSpeed = 4.5f;

    public LayerMask raycastLayers;
    public float rayDistance = 10f;

    private Vector3 rayCollisionNormal;
    private Vector3 hitLocationThisFrame = Vector3.zero;
    private bool hitThisFrame = false;
    private float timer = 1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hitThisFrame = false;

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, raycastLayers.value))
        {
            print("Raycast hit " + hitInfo.transform.name + " at " + hitInfo.point);
            rayCollisionNormal = hitInfo.normal;
            hitLocationThisFrame = hitInfo.point;
        }
    }
}
