using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int points = 0;
    public int health = 500;

    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI healthText;

    void Start()
    {
        UpdateUI();
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (pointsText != null)
            pointsText.text = $"Points: {points}";
        if (healthText != null)
            healthText.text = $"Health: {health}";
    }
}