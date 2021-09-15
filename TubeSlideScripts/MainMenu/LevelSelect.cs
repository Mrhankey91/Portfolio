using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public GameObject groupPrefab;
    private Transform parent;

    private List<LevelItemsGroup> groups = new List<LevelItemsGroup>();
    private LevelData[] levelsData;
    private int currentGroup = 0;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("ITEMS").transform;

        GetLevelsData();
        CreateGroups();
        SetItems();
    }

    private void GetLevelsData()
    {
        TextAsset[] textAsset = Resources.LoadAll<TextAsset>("Levels");
        levelsData = new LevelData[textAsset.Length];
        for(int i = 0; i < textAsset.Length; ++i)
        {
            levelsData[i] = JsonUtility.FromJson<LevelData>(textAsset[i].text);
        }
        SortLevelsData();
    }

    private void SortLevelsData()
    {
        LevelData[] ld = new LevelData[levelsData.Length];

        foreach (LevelData data in levelsData)
            ld[data.level - 1] = data;

        levelsData = ld;
    }

    private void CreateGroups()
    {
        float itemsPerGroup = groupPrefab.GetComponent<LevelItemsGroup>().levelItems.Length;
        int numberGroups = Mathf.CeilToInt(levelsData.Length / itemsPerGroup);
        for (int i = 0; i < numberGroups; ++i)
        {
            groups.Add(Instantiate(groupPrefab, parent, false).GetComponent<LevelItemsGroup>());
            //if (i > 0)
             //   groups[i].gameObject.SetActive(false);
        }

        ShowGroupItems(groups[0].levelItems, true);
        groups[currentGroup].transform.SetAsLastSibling();
    }

    private void SetItems()
    {
        bool starCompletedPreviousLevel = true;
        int group = 0;
        int index = 0;
        int itemsPerGroup = groups[0].levelItems.Length;
        int levelId = 0;
        int starsCompleted = 0;
        float bestTime = 0;

        for (int i = 0; i < groups.Count * itemsPerGroup; ++i)
        {
            group = Mathf.FloorToInt(i / (float)itemsPerGroup);
            index = i % itemsPerGroup;

            if (i >= levelsData.Length)
                groups[group].levelItems[index].SetItem(false, 0, 0, 0, false);
            else
            {
                levelId = levelsData[i].level;
                bestTime = DataController.instance.times.bestTimes[levelId - 1];
                starsCompleted = GetStarsCompleted(bestTime, levelsData[i].targetTimes);

                groups[group].levelItems[index].SetItem(true, levelId, bestTime, starsCompleted, starCompletedPreviousLevel);

                if (starsCompleted > 0)
                    starCompletedPreviousLevel = true;
                else
                    starCompletedPreviousLevel = false;
            }
        }
    }
    private int GetStarsCompleted(float time, float[] targetTimes)
    {
        //for (int i= 0; i < levelController.levelData.targetScores; ++i)
        if (time == 0)
            return 0;

        int stars = 0;
        foreach (float ts in targetTimes)
        {
            if (time <= ts)
                stars++;
            else
                break;
        }
        return stars;
    }

    public void NextGroup()
    {
        if (currentGroup < groups.Count - 1)
        {
            //groups[currentGroup].gameObject.SetActive(false);
            ShowGroupItems(groups[currentGroup].levelItems, false);
            currentGroup++;
            //groups[currentGroup].gameObject.SetActive(true);
            ShowGroupItems(groups[currentGroup].levelItems, true);
            groups[currentGroup].transform.SetAsLastSibling();
        }
    }

    public void PreviousGroup()
    {
        if(currentGroup > 0)
        {
            //groups[currentGroup].gameObject.SetActive(false);
            ShowGroupItems(groups[currentGroup].levelItems, false);
            currentGroup--;
            //groups[currentGroup].gameObject.SetActive(true);
            ShowGroupItems(groups[currentGroup].levelItems, true);
            groups[currentGroup].transform.SetAsLastSibling();
        }
    }

    private void ShowGroupItems(LevelItem[] items, bool show)
    {
        foreach(LevelItem item in items)
        {
            item.Show(show);
        }
    }
}
