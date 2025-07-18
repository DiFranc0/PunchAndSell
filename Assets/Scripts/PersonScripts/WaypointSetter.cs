using UnityEngine;

public class WaypointSetter : MonoBehaviour
{

    [SerializeField] Transform currentWaypoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentWaypoint(Transform waypoint)
    {
        currentWaypoint = waypoint; // Set the current waypoint to the provided transform
        Debug.Log("Current waypoint set to: " + currentWaypoint.name); // Log the name of the current waypoint
    }

    public Transform GetCurrentWaypoint()
    {
        return currentWaypoint; // Return the current waypoint transform
    }
}
