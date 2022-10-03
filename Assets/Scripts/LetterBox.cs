using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: currently GM doesn't save up to which letter you've read 
public class LetterBox : MonoBehaviour, IInteractable
{
    [SerializeField] private Letter activeLetter;

    [SerializeField] private Sprite openSprite;
    private Sprite closedSprite;

    [SerializeField] private List<LetterData> letterDataList;
    private LetterData nextLetterData, currLetterData;

    private new SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        closedSprite = renderer.sprite;
    }

    private void Start()
    {
        // Sort letter data list by day
        letterDataList.Sort(CompareLetterData);
        while (letterDataList.Count > 0)
        {
            LetterData data = letterDataList[0];
            letterDataList.RemoveAt(0);

            if (data.day >= GameManager.Instance.Day)
            {
                nextLetterData = data;
                break;
            }
            currLetterData = data;
        }
    }

    private static int CompareLetterData(LetterData a, LetterData b)
    {
        return a.day - b.day;
    }

    public void OnInteraction()
    {
        if (GameManager.Instance.CurrPauseState != GameManager.PauseState.FULL)
        {
            if (activeLetter.gameObject.activeSelf)
            {
                activeLetter.gameObject.SetActive(false);
                renderer.sprite = closedSprite;
                GameManager.Instance.SetPauseState(GameManager.PauseState.NONE);
            }
            else
            {
                activeLetter.gameObject.SetActive(true);
                renderer.sprite = openSprite;

                if (nextLetterData != null && nextLetterData.day <= GameManager.Instance.Day)
                {
                    currLetterData = nextLetterData;
                    if (letterDataList.Count > 0)
                    {
                        nextLetterData = letterDataList[0];
                        letterDataList.RemoveAt(0);
                    }
                    else
                        nextLetterData = null;
                }

                activeLetter.SetTitle(currLetterData.subject);
                activeLetter.SetContent(currLetterData.body);
                activeLetter.SetAuthor(currLetterData.author);
                GameManager.Instance.SetPauseState(GameManager.PauseState.MENU);
            }
        }
    }

    public string GetInteractionDesc()
    {
        if (activeLetter.gameObject.activeSelf)
            return "Close letter";
        else if (nextLetterData == null || nextLetterData.day > GameManager.Instance.Day)
            return "Read last letter";
        else
            return "Read new letter";
    }
}
