using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{

   [SerializeField] int points = 1;

    private void OnTriggerEnter(Collider other)
    {
        
        FindObjectOfType<Score>().ChangeScore(points);

        Debug.Log("Score!");
        Debug.Log(FindObjectOfType<Score>().GetScore());
    }

}
