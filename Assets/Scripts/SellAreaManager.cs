using System.Collections;
using UnityEngine;

public class SellAreaManager : MonoBehaviour
{
    [SerializeField] ItemsList cargoList;
    [SerializeField] IntVariable moneyQuant;
    [SerializeField] int pricePerItem = 20; // Price for each item sold
    [SerializeField] PoolableObject personPool; // Poolable object for person prefabs

    [SerializeField] Transform[] spawnPoints;

    AudioSource audioSource; // Audio source for playing money sounds
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        moneyQuant.Value = 0; // Initialize money quantity to 0
    }

    private void OnTriggerEnter(Collider other)
    {
        if(cargoList.Items.Count > 0)
        {
            StartCoroutine(SellPerson());
        }
        
    }

    IEnumerator SellPerson()
    {
        Transform spawnPos;
        for (int i = cargoList.Items.Count -1; i >= 0; i--)
        {
            var item = cargoList.Items[i];

            yield return new WaitForSeconds(0.4f);
            
            if (audioSource != null)
            {
                audioSource.Play(); // Play the sound when selling the person
            }

            cargoList.Items.RemoveAt(i);
            moneyQuant.Value += pricePerItem; // Increase money by the price of the person

            spawnPos = item.GetComponent<WaypointSetter>().GetCurrentWaypoint(); // Get the spawn point of the person
            personPool.GetObject(spawnPos); // Spawn a new person at the spawn point

            item.GetComponent<PoolableObjectInstance>().ReturnToPool(); // Return the item to the pool
        }
        
    }
}
