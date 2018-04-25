using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform target;

    public float moveSpeed = 3f;

    public LayerMask raycastLayers;
    public float rayDistance = 10f;

    private Vector3 rayCollisionNormal;
    private Vector3 hitLocationThisFrame = Vector3.zero;
    private bool hitThisFrame = false;

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
            hitThisFrame = true;
            transform.LookAt(target);
            transform.position = transform.forward * moveSpeed;
        }
        //if (hitInfo.transform.tag == "Player")
        //{
        //    transform.LookAt(target);
        //    transform.position = transform.forward * Time.deltaTime * moveSpeed;
        //}


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);
    }
}
