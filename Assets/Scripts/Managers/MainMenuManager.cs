using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    RectTransform titleRect;
    [SerializeField]
    RectTransform playButtonRect;
    [SerializeField]
    RectTransform creditsButtonsRect;
    [SerializeField]
    GameObject creditsPanel;

    private void Start()
    {
        DOTween.Sequence().AppendInterval(2f).AppendCallback(() => titleRect.DOAnchorPosY(-250f, 1f)).
            AppendInterval(.7f).Append(titleRect.DOPunchScale(new Vector3(.7f, .7f, 0), .5f, 1, 1)).AppendInterval(.5f).AppendCallback(() => 
            {
                playButtonRect.gameObject.SetActive(true);
                playButtonRect.DORotate(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.OutBack);
                playButtonRect.DOAnchorPosY(-400f, 1f).SetEase(Ease.OutBack);
            }).AppendInterval(1.5f).AppendCallback(() => 
            {
                creditsButtonsRect.gameObject.SetActive(true);
                creditsButtonsRect.DORotate(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.OutBack);
                creditsButtonsRect.DOAnchorPosY(-670, 1f).SetEase(Ease.OutBack);
            });
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnCreditsButtonClicked()
    {
        creditsPanel.SetActive(true);
    }

    public void OnAnyButtonClicked()
    {
        creditsPanel.SetActive(false);
    }
}
