using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}

[System.Serializable]
public class WLRecord
{
    public string Rank;
    public string Rewards;

    //승패 기록 리스트
    public WLRecord(string rank, string rewards)
    {
        Rank = rank;
        Rewards = rewards;
    }
}

public class Rating : MonoBehaviour
{
    public List<WLRecord> wlList = new List<WLRecord>();
    //파일 위치
    string filePath;
    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/Ranking.txt";
        print(filePath);
        LoadFile();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveFile()
    {
        //AllAnimalList 직렬화
        string jdata = JsonUtility.ToJson(new Serialization<WLRecord>(wlList));
        File.WriteAllText(filePath, jdata);
    }

    public void LoadFile()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("새로생성");
            ResetItem();
        }
        Debug.Log("원래생성");
        string jdata = File.ReadAllText(filePath);

        wlList = JsonUtility.FromJson<Serialization<WLRecord>>(jdata).target;
    }

    public void ResetItem()
    {
        string jdata = JsonUtility.ToJson(new Serialization<WLRecord>(wlList));
        File.WriteAllText(filePath, jdata);
        LoadFile();
    }

    public void BtnClick()
    {
        wlList.Add(new WLRecord("랭크", "보상"));
        SaveFile();
    }
}
