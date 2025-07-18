using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] IntVariable moneyQuant;
    [SerializeField] IntVariable cargoMaxQuant;
    [SerializeField] IntVariable playerLevel;
    [SerializeField] ItemsList cargoList;

    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text cargoText;
    [SerializeField] TMP_Text levelText;

    int cargoQuant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoneyText();
        UpdateCargoText();
        UpdateLevelText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "MONEY: " + moneyQuant.Value.ToString();
    }

    public void UpdateCargoText()
    {
        cargoQuant = cargoList.Items.Count;
        cargoText.text = "CARGO: " + cargoQuant.ToString() + "/" + cargoMaxQuant.Value.ToString();
    }

    public void UpdateLevelText()
    {
        levelText.text = "LVL: " + (playerLevel.Value + 1);
    }
}
