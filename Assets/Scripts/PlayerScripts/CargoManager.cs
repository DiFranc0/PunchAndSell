using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    [SerializeField] Transform cargoTopPoint; // The point where cargo items will be added
    [SerializeField] ItemsList cargoList;
    [SerializeField] IntVariable currentMaxCargo;

    [SerializeField] PoolableObject personPool; // Poolable object for person prefabs

    [SerializeField] int initialMaxCargo = 3; // Initial maximum cargo count

    void OnEnable()
    {
        cargoList.Items.Clear(); // Clear the cargo list when the manager is enabled
        currentMaxCargo.Value = initialMaxCargo; // Reset the current max cargo count

        // Subscribe to the event when a cargo item is added
        PickPerson.OnPickedUp += (person) => StartCoroutine(AddToCargo(person));
    }

    IEnumerator AddToCargo(GameObject person)
    {
        if(cargoList.Items.Count >= currentMaxCargo.Value)
        {
            Debug.Log("Cargo is full! Cannot add more items.");
            yield return new WaitForSeconds(1f); // Wait for 1 second before exiting
           
            person.GetComponent<PoolableObjectInstance>().ReturnToPool(); // Return the person to the pool

            Transform spawnPoint = person.GetComponent<WaypointSetter>().GetCurrentWaypoint(); // Get person spawn point
            personPool.GetObject(spawnPoint); // Spawn a new person at spawn point

            yield break; // Exit if cargo is full
        }

        yield return new WaitForSeconds(1f); // Wait for 1 second before adding to cargo

        Rigidbody[] personRigidbodies = person.GetComponentsInChildren<Rigidbody>();
        Collider[] personColliders = person.GetComponentsInChildren<Collider>();

        foreach (Rigidbody rb in personRigidbodies)
        {
            rb.isKinematic = true; 
            rb.useGravity = false; 

        }

        foreach (Collider col in personColliders)
        {
            col.enabled = false; // Disable the colliders to prevent physics interactions
        }
        //person.transform.SetParent(cargoTopPoint.parent);
        person.transform.position = cargoTopPoint.position; // Set the position to the cargo top point
        person.GetComponent<Animator>().enabled = true; // Disable the animator to prevent further animations
        person.transform.localEulerAngles = new Vector3(0, -90, 0); // Set the rotation to match the cargo top point

        cargoList.Items.Add(person); // Add the person to the cargo list
        //cargoTopPoint.position += new Vector3(0, 0.5f, 0); // Move the cargo top point down to stack the next item
    }
    
}
