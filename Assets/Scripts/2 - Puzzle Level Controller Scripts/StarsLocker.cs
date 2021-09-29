using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsLocker : MonoBehaviour
{
    [SerializeField]
    private PuzzleGameSaver puzzleGameSaver;

    [SerializeField]
    private GameObject[] level1Stars, level2Stars, level3Stars, level4Stars, level5Stars;

    public int[] candyPuzzleLevelStars;
    public int[] transportPuzzleLevelStars;
    public int[] fruitPuzzleLevelStars;

    public void ActivateStars(int level, string selectedPuzzle)
    {
        GetStars();
        int stars;

        switch (selectedPuzzle)
        {
            case "Candy Puzzle":
                stars = candyPuzzleLevelStars[level - 1];
                ActivateLevelStars(level, stars);
                break;
            case "Transport Puzzle":
                stars = transportPuzzleLevelStars[level - 1];
                ActivateLevelStars(level, stars);
                break;
            case "Fruit Puzzle":
                stars = fruitPuzzleLevelStars[level - 1];
                ActivateLevelStars(level, stars);
                break;
        }
    }

    void ActivateLevelStars(int level, int stars)
    {
        switch (level)
        {
            case 1:
                if (stars > 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level1Stars[i].SetActive(true);
                    }
                }
                break;
            case 2:
                if (stars > 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level2Stars[i].SetActive(true);
                    }
                }
                break;
            case 3:
                if (stars > 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level3Stars[i].SetActive(true);
                    }
                }
                break;
            case 4:
                if (stars > 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level4Stars[i].SetActive(true);
                    }
                }
                break;
            case 5:
                if (stars > 0)
                {
                    for (int i = 0; i < stars; i++)
                    {
                        level5Stars[i].SetActive(true);
                    }
                }
                break;
        }
    }

    public void DeactivateStars()
    {
        for (int i = 0; i < level1Stars.Length; i++)
        {
            level1Stars[1].SetActive(false);
            level2Stars[1].SetActive(false);
            level3Stars[1].SetActive(false);
            level4Stars[1].SetActive(false);
            level5Stars[1].SetActive(false);
        }
    }

    void GetStars()
    {
        candyPuzzleLevelStars = puzzleGameSaver.candyPuzzleLevelStars;
        transportPuzzleLevelStars = puzzleGameSaver.transportPuzzleLevelStars;
        fruitPuzzleLevelStars = puzzleGameSaver.fruitPuzzleLevelStars;
    }
}
