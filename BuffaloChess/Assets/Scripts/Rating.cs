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
    public int Rank;
    public bool Star1;
    public bool Star2;
    public bool Star3;
    
    //승패 기록 리스트
    public WLRecord(int rank, bool star1, bool star2, bool star3)
    {
        Rank = rank;
        Star1 = star1;
        Star2 = star2;
        Star3 = star3;
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
        wlList.Add(new WLRecord(0, true, false, false));
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
        if(wlList[0].Star1 && !wlList[0].Star2 && !wlList[0].Star3)
        {
            wlList[0].Star2 = true;
            SaveFile();
        }

        else if (wlList[0].Star1 && wlList[0].Star2 && !wlList[0].Star3)
        {
            Debug.Log("ddd");
            wlList[0].Star3 = true;
            SaveFile();
        }

        else if (wlList[0].Star1 && wlList[0].Star2 && wlList[0].Star3)
        {
            wlList[0].Rank += 1;
            wlList[0].Star1 = true;
            wlList[0].Star2 = false;
            wlList[0].Star3 = false;
            SaveFile();
        }
    }
}
