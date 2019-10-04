using UnityEngine;
using System.Collections;
public class FollowCam : MonoBehaviour
{
    static public FollowCam S; // a FollowCam Singleton
                               // fields set in the Unity Inspector pane
    public bool _____________________________;
    // fields set dynamically
    public GameObject poi; // The point of interest
    public float camZ; // The desired Z pos of the camera
    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }
    void Update()
    {
        // if there's only one line following an if, it doesn't need braces
        if (poi == null) return; // return if there is no poi
                                 // Get the position of the poi
        Vector3 destination = poi.transform.position;
        // Retain a destination.z of camZ
        destination.z = camZ;
        // Set the camera to the destination
        transform.position = destination;
    }
}