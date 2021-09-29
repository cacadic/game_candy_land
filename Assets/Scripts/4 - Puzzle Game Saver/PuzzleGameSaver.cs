using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PuzzleGameSaver : MonoBehaviour
{
    private GameData gameData;

    public bool[] candyPuzzleLevels;
    public bool[] transportPuzzleLevels;
    public bool[] fruitPuzzleLevels;

    public int[] candyPuzzleLevelStars;
    public int[] transportPuzzleLevelStars;
    public int[] fruitPuzzleLevelStars;

    private bool isGameStartedForTheFirstTime;

    public float musicVolume;

    private string filePath;

    private void Awake()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        filePath = Application.persistentDataPath + "/GameData.dat";
        LoadGameData();

        if (gameData != null)
        {
            isGameStartedForTheFirstTime = gameData.isGameStartedForTheFirstTime;
        }
        else
        {
            isGameStartedForTheFirstTime = true;
        }

        if (isGameStartedForTheFirstTime)
        {
            isGameStartedForTheFirstTime = false;
            musicVolume = 1;

            candyPuzzleLevels = new bool[5];
            transportPuzzleLevels = new bool[5];
            fruitPuzzleLevels = new bool[5];

            candyPuzzleLevels[0] = true;

            for (int i = 1; i < 5; i++)
            {
                candyPuzzleLevels[i] = false;
                transportPuzzleLevels[i] = false;
                fruitPuzzleLevels[i] = false;
            }

            candyPuzzleLevelStars = new int[5];
            transportPuzzleLevelStars = new int[5];
            fruitPuzzleLevelStars = new int[5];

            for (int i = 0; i < 5; i++)
            {
                candyPuzzleLevelStars[i] = 0;
                transportPuzzleLevelStars[i] = 0;
                fruitPuzzleLevelStars[i] = 0;
            }

            gameData = new GameData
            {
                candyPuzzleLevels = candyPuzzleLevels,
                transportPuzzleLevels = transportPuzzleLevels,
                fruitPuzzleLevels = fruitPuzzleLevels,
                candyPuzzleLevelStars = candyPuzzleLevelStars,
                transportPuzzleLevelStars = transportPuzzleLevelStars,
                fruitPuzzleLevelStars = fruitPuzzleLevelStars,
                isGameStartedForTheFirstTime = isGameStartedForTheFirstTime,
                musicVolume = musicVolume
            };

            SaveGameData();
            LoadGameData();
        }
    }

    public void SaveGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(filePath);

            if (gameData != null)
            {
                gameData.candyPuzzleLevels = candyPuzzleLevels;
                gameData.transportPuzzleLevels = transportPuzzleLevels;
                gameData.fruitPuzzleLevels = fruitPuzzleLevels;

                gameData.candyPuzzleLevelStars = candyPuzzleLevelStars;
                gameData.transportPuzzleLevelStars = transportPuzzleLevelStars;
                gameData.fruitPuzzleLevelStars = fruitPuzzleLevelStars;

                gameData.isGameStartedForTheFirstTime = isGameStartedForTheFirstTime;
                gameData.musicVolume = musicVolume;

                bf.Serialize(file, gameData);
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    void LoadGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(filePath, FileMode.Open);

            gameData = (GameData)bf.Deserialize(file);

            if (gameData != null)
            {
                candyPuzzleLevels = gameData.candyPuzzleLevels;
                transportPuzzleLevels = gameData.transportPuzzleLevels;
                fruitPuzzleLevels = gameData.transportPuzzleLevels;

                candyPuzzleLevelStars = gameData.candyPuzzleLevelStars;
                transportPuzzleLevelStars = gameData.transportPuzzleLevelStars;
                fruitPuzzleLevelStars = gameData.transportPuzzleLevelStars;

                isGameStartedForTheFirstTime = gameData.isGameStartedForTheFirstTime;
                musicVolume = gameData.musicVolume;
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }

    }

    public void Save(int level, string selectedPuzzle, int stars)
    {
        int unlockNextLevel = level + 1;

        switch (selectedPuzzle)
        {
            case "Candy Puzzle":
                candyPuzzleLevelStars[level - 1] = stars;

                if (unlockNextLevel < candyPuzzleLevels.Length)
                {
                    candyPuzzleLevels[unlockNextLevel - 1] = true;
                }
                break;
            case "Transport Puzzle":
                transportPuzzleLevelStars[level - 1] = stars;

                if (unlockNextLevel < transportPuzzleLevels.Length)
                {
                    transportPuzzleLevels[unlockNextLevel - 1] = true;
                }
                break;
            case "Fruit Puzzle":
                fruitPuzzleLevelStars[level - 1] = stars;

                if (unlockNextLevel < fruitPuzzleLevels.Length)
                {
                    fruitPuzzleLevels[unlockNextLevel - 1] = true;
                }
                break;
        }

        SaveGameData();
    }
}
