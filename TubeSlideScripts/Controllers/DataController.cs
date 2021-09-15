using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataController : MonoBehaviour
{
    public static DataController instance;
    public LevelTimesData times;
    public int levelID = 1;

    private string path;

    // Start is called before the first frame update
    void Awake()
    {
        if (DataController.instance == null)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            instance = this;
            path = Application.persistentDataPath + "/times.dat";
            DontDestroyOnLoad(gameObject);
            LoadTimes();
            Localization.LocalizationReader.LoadLocalizationData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CreateTimes()
    {
        times = new LevelTimesData();
        times.CreateTimesSave();
        SaveTimes();
    }

    private void LoadTimes()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            times = bf.Deserialize(file) as LevelTimesData;
            file.Close();
        }
        else
        {
            CreateTimes();
        }
    }

    public void SaveTimes()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        bf.Serialize(file, times);
        file.Close();
    }
}
