using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour
{
    [SerializeField]
    private PuzzleGameSaver puzzleGameSaver;

    [SerializeField]
    private Text hitCountText;

    [SerializeField]
    private GameFinished gameFinished;

    private List<Button> puzzleButtons = new List<Button>();

    private List<Animator> puzzleButtonsAnimators = new List<Animator>();

    [SerializeField]
    private List<Sprite> gamePuzzleSprites = new List<Sprite>();

    private int level;

    private string selectedPuzzle;

    private bool firstGuess, secondGuess;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;
    private Sprite puzzleBackgroundImage;

    private int countTryGuess, countCorrectGuess, gameGuess;

    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void PickAPuzzle()
    {
        int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name) - 1;
        string puzzleName = gamePuzzleSprites[index].name;
        if (!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = index;

            firstGuessPuzzle = puzzleName;

            StartCoroutine(TurnPuzzleButtonUp(
                puzzleButtonsAnimators[firstGuessIndex],
                puzzleButtons[firstGuessIndex],
                gamePuzzleSprites[firstGuessIndex]
                ));
        }
        else if (!secondGuess)
        {
            secondGuess = true;

            secondGuessIndex = index;

            secondGuessPuzzle = puzzleName;

            StartCoroutine(TurnPuzzleButtonUp(
                puzzleButtonsAnimators[secondGuessIndex],
                puzzleButtons[secondGuessIndex],
                gamePuzzleSprites[secondGuessIndex]
                ));

            StartCoroutine(CheckIfThePuzzlesMatch());

            countTryGuess++;
        }
    }

    void CheckIfTheGameIsFinised()
    {
        countCorrectGuess++;

        if (countCorrectGuess == gameGuess)
        {
            CheckHowManyGuesses();
        }
    }

    void CheckHowManyGuesses()
    {
        int howManyGuesses = 0;

        switch (level)
        {
            case 1:
                howManyGuesses = 5;
                break;
            case 2:
                howManyGuesses = 10;
                break;
            case 3:
                howManyGuesses = 15;
                break;
            case 4:
                howManyGuesses = 20;
                break;
            case 5:
                howManyGuesses = 25;
                break;
        }

        if (countTryGuess < howManyGuesses)
        {
            gameFinished.ShowGameFinishedPanel(3);
            puzzleGameSaver.Save(level, selectedPuzzle, 3);
        }
        else if (countTryGuess < (howManyGuesses + 5))
        {
            gameFinished.ShowGameFinishedPanel(2);
            puzzleGameSaver.Save(level, selectedPuzzle, 2);
        }
        else
        {
            gameFinished.ShowGameFinishedPanel(1);
            puzzleGameSaver.Save(level, selectedPuzzle, 1);
        }

        hitCountText.text = countTryGuess.ToString();
    }

    public List<Animator> ResetGameplay()
    {
        firstGuess = secondGuess = false;
        countTryGuess = 0;
        countCorrectGuess = 0;

        gameFinished.HideGameFinishedPanel();

        return puzzleButtonsAnimators;
    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(1.7f);
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            puzzleButtonsAnimators[firstGuessIndex].Play("FadeOut");
            puzzleButtonsAnimators[secondGuessIndex].Play("FadeOut");

            //+ Score
            CheckIfTheGameIsFinised();
        }
        else
        {
            StartCoroutine(TurnPuzzleButtonBack(
                puzzleButtonsAnimators[firstGuessIndex],
                puzzleButtons[firstGuessIndex],
                puzzleBackgroundImage
                ));

            StartCoroutine(TurnPuzzleButtonBack(
                puzzleButtonsAnimators[secondGuessIndex],
                puzzleButtons[secondGuessIndex],
                puzzleBackgroundImage
                ));
        }

        yield return new WaitForSeconds(0.7f);

        firstGuess = secondGuess = false;
    }

    IEnumerator TurnPuzzleButtonUp(Animator anim, Button btn, Sprite puzzleImage)
    {
        anim.Play("TurnUp");
        yield return new WaitForSeconds(0.4f);
        btn.image.sprite = puzzleImage;
    }

    IEnumerator TurnPuzzleButtonBack(Animator anim, Button btn, Sprite puzzleImage)
    {
        anim.Play("TurnBack");
        yield return new WaitForSeconds(0.4f);
        btn.image.sprite = puzzleImage;
    }

    void AddListeners()
    {
        foreach (Button btn in puzzleButtons)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void SetUpButtonsAndAnimators(List<Button> buttons, List<Animator> animators)
    {
        this.puzzleButtons = buttons;
        this.puzzleButtonsAnimators = animators;

        gameGuess = puzzleButtons.Count / 2;

        puzzleBackgroundImage = puzzleButtons[0].image.sprite;

        AddListeners();
    }

    public void SetGamePuzzleSprites(List<Sprite> gamePuzzleSprites)
    {
        this.gamePuzzleSprites = gamePuzzleSprites;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void SetSelectedPuzzle(string selectedPuzzle)
    {
        this.selectedPuzzle = selectedPuzzle;
    }
}
