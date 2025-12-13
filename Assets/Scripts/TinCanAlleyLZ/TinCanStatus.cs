using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinCanStatus : MonoBehaviour
{

    // Cans
    private static List<GameObject> cans = new List<GameObject>();
    // vars
    public static int Score { get; private set; }


    void Start()
    {
        Score = 0;
    }

    public static void AddCanToList(GameObject can) => cans.Add(can);


    public static void ResetCans()
    {
        Score = 0;

        foreach (GameObject can in cans)
        {
            can.GetComponent<Can>().ResetPos();
        }
    }

    public static void TallyScore()
    {
        foreach (GameObject can in cans)
        {
            if (!can.GetComponent<Can>().Standing)
            {
                Score += 1;
            }
        }
    }
}
