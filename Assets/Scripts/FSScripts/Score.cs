using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField] int score;

    public int GetScore()
    {
        return score;
    }

    public void ChangeScore( int change)
    {
        score = score + change;
    }
}