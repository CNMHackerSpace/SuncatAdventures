using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManagerMAG : MonoBehaviour
{
    public enum GameStage
    {
        Stage1_FixedColors,
        Stage2_RandomColors
    }

    public GameStage currentStage = GameStage.Stage1_FixedColors;

    public List<MemoryTileMAG> tiles;        // Assign all tiles in Inspector
    public List<Color> possibleColors;       // Colors to assign, in pairs (used in Stage 2)

    private List<Color> shuffledColors;

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        if (currentStage == GameStage.Stage1_FixedColors)
        {
            // Stage 1: Reset tiles, but keep their existing tileColor as assigned in Inspector
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].gameObject.SetActive(true);
                tiles[i].ResetTile();
            }
        }
        else if (currentStage == GameStage.Stage2_RandomColors)
        {
            // Stage 2: Randomize and assign colors
            List<Color> colorPool = new();
            foreach (var c in possibleColors)
            {
                colorPool.Add(c);
                colorPool.Add(c);
            }

            shuffledColors = ShuffleList(colorPool);

            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].gameObject.SetActive(true);
                tiles[i].tileColor = shuffledColors[i];
                tiles[i].ResetTile();
            }
        }
    }

    public void CheckForWin()
    {
        foreach (var tile in tiles)
        {
            if (tile.gameObject.activeSelf)
                return;
        }

        Debug.Log("All tiles matched! Resetting game...");
        ResetGame();
    }

    private List<Color> ShuffleList(List<Color> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
        return list;
    }
}
