using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[Serializable]
public class GameData
{
    public bool[] candyPuzzleLevels { get; set; }
    public bool[] transportPuzzleLevels { get; set; }
    public bool[] fruitPuzzleLevels { get; set; }

    public int[] candyPuzzleLevelStars { get; set; }
    public int[] transportPuzzleLevelStars { get; set; }
    public int[] fruitPuzzleLevelStars { get; set; }

    public bool isGameStartedForTheFirstTime { get; set; }

    public float musicVolume { get; set; }

}
