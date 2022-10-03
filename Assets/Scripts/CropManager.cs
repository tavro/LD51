using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CropManager : MonoBehaviour
{
	[SerializeField] private Crop cropPrefab;
    [SerializeField] private Transform[] cropPlots;
    private Crop[] crops;

    [SerializeField] private int debugMinCrops;

    private void Start()
    {
#if UNITY_EDITOR
        int minCropsToPlace = Mathf.Clamp(debugMinCrops, 0, cropPlots.Length);
#else
        int minCropsToPlace = 0;
#endif
        int cropsToPlace = Mathf.Clamp(GameManager.Instance.DaysSinceInteraction, minCropsToPlace, cropPlots.Length);

        if (cropsToPlace == 0)
            SceneManager.LoadScene("FarmScene"); // Just here in case

        crops = new Crop[cropsToPlace];
        for (int i = 0; i < cropsToPlace; i++)
            crops[i] = Instantiate(cropPrefab, cropPlots[i].position, Quaternion.identity, cropPlots[i]);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            bool cropsAreGone = true;
            foreach (Crop crop in crops)
            {
                if (!crop.IsGone)
                    cropsAreGone = false;
            }

            if (cropsAreGone)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                SceneManager.LoadScene("FarmScene");
            }
                

        }
    }

    public Crop GetRandomCrop()
    {
        int currAttempt = 0;
        Crop crop = null;
        while (currAttempt < 10 && (crop == null || crop.IsPulled) && crops.Length > 0)
        {
            int cropIdx = Random.Range(0, crops.Length);
            crop = crops[cropIdx];
            currAttempt++;
        }
        return crop;
    }
}
