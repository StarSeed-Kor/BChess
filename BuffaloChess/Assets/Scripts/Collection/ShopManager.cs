using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Shop
{
    public Shop(string _Type, string _ID, string _Contents, string _Cost, bool _IsHaving)
    {
        Type = _Type; ID = _ID; Contents = _Contents; Cost = _Cost; IsHaving = _IsHaving;
    }
    public string Type, Contents, ID, Cost;
    public bool IsHaving;
}

public class ShopManager : MonoBehaviour
{

    public TextAsset ShopDatabase;

    public List<Shop> AllItemList, MyItemList;
  
    string filePath;
  
    public GameObject[] ItemSlot;

    public GameObject[] HavingItemSlot;
   
    public Sprite[] ItemSprite;

    public GameObject ItemExplainPanel;

    public GameObject PurchasePanel;

    Shop CurItem;
    int curslotNum;
    // Start is called before the first frame update
    void Start()
    {
        string[] itemline = ShopDatabase.text.Substring(0, ShopDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < itemline.Length; i++)
        {
            string[] row = itemline[i].Split('\t');

            AllItemList.Add(new Shop(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }

        //파일 위치 지정
        filePath = Application.persistentDataPath + "/AllItem.txt";
        print(filePath);
        LoadFile();
        if(SceneManager.GetActiveScene().name == "Shop")
        {
            ItemArrange();
        }

        if (SceneManager.GetActiveScene().name == "Collection")
        {
            Debug.Log("comein");
            HavingItemArrange();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveFile()
    {
        //AllAnimalList 직렬화
        string jdata = JsonUtility.ToJson(new Serialization<Shop>(AllItemList));
        File.WriteAllText(filePath, jdata);
    }

    void LoadFile()
    {
        if (!File.Exists(filePath))
        {
            ResetItem();
        }
        string jdata = File.ReadAllText(filePath);

        AllItemList = JsonUtility.FromJson<Serialization<Shop>>(jdata).target;
    }

    public void ResetItem()
    {
        string jdata = JsonUtility.ToJson(new Serialization<Shop>(AllItemList));
        File.WriteAllText(filePath, jdata);
        LoadFile();
    }

    public void ItemArrange()
    {
        for (int i = 0; i < ItemSlot.Length; i++)
        {
            ItemSlot[i].SetActive(i < AllItemList.Count);
            ItemSlot[i].transform.GetChild(1).GetComponent<Text>().text = i < AllItemList.Count ? AllItemList[i].ID : "";
            ItemSlot[i].transform.GetChild(2).GetComponent<Text>().text = i < AllItemList.Count ? AllItemList[i].Cost: "";
        
            if(i < AllItemList.Count)
            {
                ItemSlot[i].transform.GetChild(0).GetComponent<Image>().sprite = ItemSprite[i];
            }
        }
    }

    public void HavingItemArrange()
    {
        MyItemList = AllItemList.FindAll(x => x.IsHaving);

        for (int i = 0; i < HavingItemSlot.Length; i++)
        {
            Debug.Log(MyItemList.Count);
            HavingItemSlot[i].SetActive(i < MyItemList.Count);
            HavingItemSlot[i].transform.GetChild(1).GetComponent<Text>().text = i < MyItemList.Count ? MyItemList[i].ID : "";

            if (i < MyItemList.Count)
            {
                //샵 에서는 몇번째인지 알아야함 추후 고치기
                HavingItemSlot[i].transform.GetChild(0).GetComponent<Image>().sprite = ItemSprite[i];
            }
        }
    }

    public void ClickItem(int slotNum)
    {
        curslotNum = slotNum;
        CurItem = AllItemList[curslotNum];
        ItemExplainPanel.transform.GetChild(0).GetComponent<Image>().sprite = ItemSprite[curslotNum];
        ItemExplainPanel.transform.GetChild(1).GetComponent<Text>().text = CurItem.Contents;
        ItemExplainPanel.transform.GetChild(2).GetComponent<Text>().text = CurItem.Cost;
    }

    public void PurchaseItem()
    {
        PurchasePanel.SetActive(true);
        PurchasePanel.transform.GetChild(0).GetComponent<Text>().text = "상품을 구매하시겠습니까?";
    }

    public void YesBtn()
    {
        if (PurchasePanel.transform.GetChild(0).GetComponent<Text>().text == "상품을 구매하시겠습니까?")
        {
            if (CurItem.IsHaving)
            {
                PurchasePanel.transform.GetChild(0).GetComponent<Text>().text = "이미 가지고 있는 상품입니다";
            }
            else
            {
                CurItem.IsHaving = true;
                SaveFile();
                PurchasePanel.transform.GetChild(0).GetComponent<Text>().text = "구매가 완료되었습니다.";
            }
        }

        else
            PurchasePanel.SetActive(false);
    }

    public void OffPurchasePanel()
    {
        PurchasePanel.SetActive(false);
        PurchasePanel.transform.GetChild(0).GetComponent<Text>().text = "상품을 구매하시겠습니까?";
    }
}
