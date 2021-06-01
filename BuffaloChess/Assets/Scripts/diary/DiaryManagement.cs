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
public class Diary
{
    public Diary(string _Type, string _ID, string _Contents, string _Compensate, bool _IsHaving)
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

    public List<Diary> AllDiaryList, MyDiaryList;
    public List<Diary> MyQuestList;
 
    string filePath;
    //string achievementPath;

    public string curDiaryType = "Quest";

    //public Sprite ClickTab, UnClickTab;

    //public Text[] QuestText;

    int CurTypeNum;
    string[] Type = { "Quest", "Achievement" };

    public GameObject[] DiarySlot;
    // Start is called before the first frame update
    public Sprite QuestClickTab, QuestUnClickTab, AcClickTab, AcUnClickTab;

    public Image[] TabImage;

    //같은 숫자 나오는거 방지
    bool isSame;
    void Start()
    {
        string[] questline = QuestDatabase.text.Substring(0, QuestDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < questline.Length; i++)
        {
            string[] row = questline[i].Split('\t');

            AllDiaryList.Add(new Diary(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }

        //파일 위치 지정
        filePath = Application.persistentDataPath + "/AllDairy.txt";
        print(filePath);
        LoadFile();
        
        if (AllDiaryList.FindAll(x => x.IsHaving).Count < 1)
        {
            Debug.Log("들어옴");
            SelectRandomQuest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TabMap(string tabName)
    {
        curDiaryType = tabName;

        MyDiaryList = AllDiaryList.FindAll(x => x.Type == curDiaryType);
        MyDiaryList = MyDiaryList.FindAll(x => x.IsHaving);

        if (tabName == "Quest")
        {
            Debug.Log("퀘스트임");
            for (int i = 0; i < DiarySlot.Length; i++)
            {
                DiarySlot[i].SetActive(i < MyDiaryList.Count);
                //현재 아이템 리스트 수 안이면 내용써줌, 아니면 내용안써줌
                DiarySlot[i].transform.GetChild(0).GetComponent<Text>().text = i < MyDiaryList.Count ? MyDiaryList[i].Contents : "";
                DiarySlot[i].transform.GetChild(1).GetComponent<Text>().text = i < MyDiaryList.Count ? MyDiaryList[i].Compensate : "";
            }
        }

        if (tabName == "Achievement")
        {
            Debug.Log("업적임");
            for (int i = 0; i < DiarySlot.Length; i++)
            {
                DiarySlot[i].SetActive(i < MyDiaryList.Count);
                //현재 아이템 리스트 수 안이면 내용써줌, 아니면 내용안써줌
                DiarySlot[i].transform.GetChild(0).GetComponent<Text>().text = i < MyDiaryList.Count ? MyDiaryList[i].Contents : "";
                DiarySlot[i].transform.GetChild(1).GetComponent<Text>().text = i < MyDiaryList.Count ? MyDiaryList[i].Compensate : "";

                //if (i < MyDiaryList.Count)
                //{
                //    if (MyDiaryList[i].IsHaving)
                //        AnimalImage[i].sprite = OnAnimalSprite[AllAnimalList.FindIndex(x => x.Name == CurAnimalList[i].Name)];
                //    if (!CurAnimalList[i].IsHaving)
                //        AnimalImage[i].sprite = OffAnimalSprite[AllAnimalList.FindIndex(x => x.Name == CurAnimalList[i].Name)];
                //}

            }
        }
        int tabNum = 0;

        switch (tabName)
        {
            case "Quest":
                tabNum = 0;
                break;

            case "Achievement":
                tabNum = 1;
                break;

        }
        TabImage[0].sprite = 0 == tabNum ? QuestClickTab : QuestUnClickTab;
       
        TabImage[1].sprite = 1 == tabNum ? AcClickTab : AcUnClickTab;
    }

    void SaveFile()
    {
        //AllAnimalList 직렬화
        string jdata = JsonUtility.ToJson(new Serialization<Diary>(AllDiaryList));
        File.WriteAllText(filePath, jdata);

        TabMap(curDiaryType);
    }

    void LoadFile()
    {
        if (!File.Exists(filePath))
        {
            ResetItem();
        }
        string jdata = File.ReadAllText(filePath);

        AllDiaryList = JsonUtility.FromJson<Serialization<Diary>>(jdata).target;
        TabMap(curDiaryType);
    }

    public void ResetItem()
    {
        string jdata = JsonUtility.ToJson(new Serialization<Diary>(AllDiaryList));
        File.WriteAllText(filePath, jdata);
        LoadFile();
    }
    void SelectRandomQuest()
    {
        MyQuestList = AllDiaryList.FindAll(x => x.Type == "Quest");

        //랜덤뽑기 3번 반복
        for(int i = 0; i < 3; i++)
        {
            MyQuestList[Random.Range(0, MyQuestList.Count)].IsHaving = true;
            MyQuestList.RemoveAt(Random.Range(0, MyQuestList.Count));
        }

        SaveFile();
    }
}
