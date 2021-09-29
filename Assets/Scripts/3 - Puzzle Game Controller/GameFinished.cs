using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinished : MonoBehaviour
{
    [SerializeField]
    private GameObject gameFinishedPanel, star1, star2, star3, continueButton, hitCount, hitCountTitle;

    [SerializeField]
    private Animator gameFinishedAnim, star1Anim, star2Anim, star3Anim;

    public void ShowGameFinishedPanel(int stars)
    {
        StartCoroutine(ShowPanel(stars));
    }

    public void HideGameFinishedPanel()
    {
        if (gameFinishedPanel.activeInHierarchy)
        {
            StartCoroutine(HidePanel());
        }
    }

    IEnumerator ShowPanel(int stars)
    {
        gameFinishedPanel.SetActive(true);
        gameFinishedAnim.Play("FadeIn");

        yield return new WaitForSeconds(1.7f);

        continueButton.SetActive(true);
        hitCount.SetActive(true);
        hitCountTitle.SetActive(true);

        switch (stars)
        {
            case 1:
                star1.SetActive(true);
                star1Anim.Play("FadeIn");
                break;
            case 2:
                star1.SetActive(true);
                star1Anim.Play("FadeIn");
                yield return new WaitForSeconds(0.25f);
                star2.SetActive(true);
                star2Anim.Play("FadeIn");
                break;
            case 3:
                star1.SetActive(true);
                star1Anim.Play("FadeIn");
                yield return new WaitForSeconds(0.25f);
                star2.SetActive(true);
                star2Anim.Play("FadeIn");
                yield return new WaitForSeconds(0.25f);
                star3.SetActive(true);
                star3Anim.Play("FadeIn");
                break;
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator HidePanel()
    {
        continueButton.SetActive(false);
        hitCount.SetActive(false);
        hitCountTitle.SetActive(false);

        gameFinishedAnim.Play("FadeOut");

        star1Anim.Play("FadeOut");
        star2Anim.Play("FadeOut");
        star3Anim.Play("FadeOut");

        yield return new WaitForSeconds(1.5f);

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        gameFinishedPanel.SetActive(false);
    }
}
