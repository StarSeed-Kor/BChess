using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[System.Serializable]
//public class Serialization<T>
//{
//    public Serialization(List<T> _target) => target = _target;
//    public List<T> target;
//}


[System.Serializable]
public class Quest
{
    public Quest(string _Type, string _ID, string _Contents, string _Compensate, bool _IsHaving)
    {
        Type = _Type; ID = _ID; Contents = _Contents; Compensate = _Compensate; IsHaving = _IsHaving;
    }
    public string Type, Contents, ID, Compensate;
    public bool IsHaving;
}

//public class CountryRoad
//{
//    public CountryRoad(int _ID, string _Contents, int _Compensate, bool _IsHaving)
//    {
//        ID = _ID; Contents = _Contents; Compensate = _Compensate; IsHaving = _IsHaving;
//    }
//    public string Contents;
//    public int ID, Compensate;
//    public bool IsHaving;
//}

public class DiaryManagement : MonoBehaviour
{
    public TextAsset QuestDatabase;

    public List<Quest> AllDiaryList, MyDiaryList;
 
    string questPath;
    //string achievementPath;

    public string curDiaryType = "Quest";

    //public Sprite ClickTab, UnClickTab;

    //public Text[] QuestText;

    int CurTypeNum;
    string[] Type = { "Quest", "Achievement" };

    public GameObject[] DiarySlot;
    // Start is called before the first frame update

    void Start()
    {
        string[] questline = QuestDatabase.text.Substring(0, QuestDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < questline.Length; i++)
        {
            string[] row = questline[i].Split('\t');

            AllDiaryList.Add(new Quest(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }

        //파일 위치 지정
        questPath = Application.persistentDataPath + "/AllDairy.txt";
        print(questPath);
        LoadFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void TabMap(string tabName)
    //{
    //    curDiaryType = tabName;

    //    MyDiaryList = AllDiaryList.FindAll(x => x.Type == curDiaryType);
      
    //    for (int i = 0; i < DiarySlot.Length; i++)
    //    {
    //        DiarySlot[i].SetActive(i < CurAnimalList.Count);
    //        //현재 아이템 리스트 수 안이면 이름써줌, 아니면 이름안써줌
    //        DiarySlot[i].transform.GetChild(1).GetComponent<Text>().text = i < CurAnimalList.Count ? CurAnimalList[i].Name : "";

    //        if (i < CurAnimalList.Count)
    //        {
    //            if (CurAnimalList[i].IsHaving)
    //                AnimalImage[i].sprite = OnAnimalSprite[AllAnimalList.FindIndex(x => x.Name == CurAnimalList[i].Name)];
    //            if (!CurAnimalList[i].IsHaving)
    //                AnimalImage[i].sprite = OffAnimalSprite[AllAnimalList.FindIndex(x => x.Name == CurAnimalList[i].Name)];
    //        }

    //    }

    //    int tabNum = 0;

    //    switch (tabName)
    //    {
    //        case "Basic":
    //            tabNum = 0;
    //            break;

    //        case "Jungle":
    //            tabNum = 1;
    //            break;

    //        case "Desert":
    //            tabNum = 2;
    //            break;

    //        case "Antarctica":
    //            tabNum = 3;
    //            break;
    //    }

    //    for (int i = 0; i < TabImage.Length; i++)
    //        TabImage[i].sprite = i == tabNum ? ClickTab : UnClickTab;
    //}

    void SaveFile()
    {
        //AllAnimalList 직렬화
        string jdata = JsonUtility.ToJson(new Serialization<Quest>(AllDiaryList));
        File.WriteAllText(questPath, jdata);

        //TabMap(curDiaryType);
    }

    void LoadFile()
    {
        if (!File.Exists(questPath))
        {
            ResetItem();
        }
        string jdata = File.ReadAllText(questPath);

        AllDiaryList = JsonUtility.FromJson<Serialization<Quest>>(jdata).target;
        //TabMap(curDiaryType);
    }

    public void ResetItem()
    {
        string jdata = JsonUtility.ToJson(new Serialization<Quest>(AllDiaryList));
        File.WriteAllText(questPath, jdata);
        LoadFile();
    }
    //void SelectRandomQuest()
    //{
    //    for(int i =0; i<quest.Length;i++)
    //    {
    //        quest[i] = Random.Range(0, QuestContents.Length);
    //        QuestText[i].text = QuestContents[quest[i]];
    //    }

    //    PlayerPrefs.SetInt("HaveQuest", 1);
    //}
    //public void ClickBookMark(string bookmark)
    //{
    //    switch (bookmark)
    //    {
    //        case "Quest":
    //            {
    //                PlayerPrefs.SetInt("quest", 1);
    //                PlayerPrefs.SetInt("countryroad", 0);
    //                PlayerPrefs.SetInt("achievements", 0);
    //                break;
    //            }

    //        case "CountryRoad":
    //            {
    //                PlayerPrefs.SetInt("quest", 0);
    //                PlayerPrefs.SetInt("countryroad", 1);
    //                PlayerPrefs.SetInt("achievements", 0);
    //                break;
    //            }
    //        case "Achievements":
    //            {
    //                PlayerPrefs.SetInt("quest", 0);
    //                PlayerPrefs.SetInt("countryroad", 0);
    //                PlayerPrefs.SetInt("achievements", 1);
    //                break;
    //            }
    //    }

    //}


}
