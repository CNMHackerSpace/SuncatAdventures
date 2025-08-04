using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int _health; // Player's health

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = 100; // Initialize player's health
    }

    public void Hurt(int damage)
    {
        _health -= damage; // Reduce health by damage amount
        Debug.Log("Player hurt! Current health: " + _health);
        Messenger<int>.Broadcast(GameEvent.PLAYER_HURT, _health); // Notify that the player was hurt
    }

    public void AddHealth(int healthToAdd)
    {
        _health += healthToAdd; // Increase health by the specified amount
        Debug.Log("Health added! Current health: " + _health);
        Messenger<int>.Broadcast(GameEvent.PLAYER_HURT, _health); // Notify that the player's health has changed
    }
}
