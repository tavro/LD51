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
    [SerializeField] private List<LetterData> sellLetterDataList;
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

    // All letter data into queue
    // Place next letter data into own variable
    // Allow reading letter data if day is gte than letter day
    // Load next letter data into variable
    // Save prev letter data into prev variable
    // Repeat

    public void OnInteraction()
    {
        if (activeLetter.gameObject.activeSelf)
        {
            activeLetter.gameObject.SetActive(false);
            renderer.sprite = closedSprite;
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
