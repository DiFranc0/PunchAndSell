using UnityEngine;

public class LevelUpAreaManager : MonoBehaviour
{
    [SerializeField] IntVariable moneyQuant;
    [SerializeField] IntVariable playerLevel;
    [SerializeField] IntVariable cargoMax;


    [SerializeField] ColorsList playerColorsList;
    [SerializeField] MaxCargoList cargoMaxList;
    [SerializeField] MatVariable playerMat;

    [SerializeField] int moneyCost = 100; // Cost to level up

    AudioSource audioSource; // Audio source for playing level up sounds

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLevel.Value = 0; // Initialize player level to 0
        playerMat.Material.color = Color.white; // Set initial player color
        audioSource = GetComponent<AudioSource>(); // Get the audio source component for playing sounds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (moneyQuant.Value >= moneyCost) // Check if the player has enough money to level up
            {
                LevelUp();
                moneyQuant.Value -= moneyCost; // Deduct the cost of leveling up
                moneyCost += 100; // Increase the cost for the next level up
            }
            else
            {
                Debug.Log("Not enough money to level up!"); // Log a message if not enough money
            }
        }
        
    }

    public void LevelUp()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        playerMat.Material.color = playerColorsList.Items[playerLevel.Value];
        cargoMax.Value = cargoMaxList.Items[playerLevel.Value];
        playerLevel.Value++;
    }
}
