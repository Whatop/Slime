using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class Item
{
    // 언더테일 아이템에 필요한것... ID Type Name Explain Num Using
    public Item(string _Id, string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
    { Id = _Id; Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing; }

    public string Id, Type, Name, Explain, Number;
    public bool isUsing;
}

public class GameManager : MonoBehaviour
{

    public static GameManager inst;
    private void Awake()
    {

        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(inst);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static GameManager Instance
    {
        get
        {
            if (null == inst)
            {
                return null;
            }
            return inst;
        }
    }
    public TextAsset ItemDatebase;

    public List<Item> AllItemList, BoxItemList, MyItemList;
    public string curType = "None";

    // Text 프리팹 창 만들기 토크 토크

    //Text
    public GameObject talkPanel;
    public Image talkFace;
    public TextMeshProUGUI talkBox;
    public TalkManager talkManager;
    public GameObject scanObject;
    public int talkIndex;

    //Event

    public bool isAction;
    public bool isEvent = false;
    public bool isPlayerMove = true;

    //World



    Text ItemNameInput;
    Text ItemNumberInput;

    public bool bFlagZ = true;

    void Start()
    {
        string[] line = ItemDatebase.text.Substring(0, ItemDatebase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));
        }
        //print(line.Length);
        Load();

        Init();
        //   GetInventoryBox(); 
        // FightStart();
    }

    private void Init()
    {
    }
    void Update()
    {
    }

    void Save()
    {
    }


    public void Action(GameObject scanObj) // 반응.!
    {
        if (isAction)
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id, objData.isNpc);
        }
        talkPanel.SetActive(isAction);
    }

    public void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        if (isNpc)
        {
            // 위치 49(하) 567(상)
            // 크기 1720 440
            talkFace.gameObject.SetActive(true);
            talkBox.text= (talkData);
        }
        else
        {
            talkFace.gameObject.SetActive(false);
            talkBox.text= (talkData);
        }

    }
    public void Skip(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        if (isNpc)
        {
            // 위치 49(하) 567(상)
            // 크기 1720 440
            talkFace.gameObject.SetActive(true);
            talkBox.text = (talkData);
        }
        else
        {
            talkFace.gameObject.SetActive(false);
            talkBox.text = (talkData);
        }

    }

    public void GetItemClick(int id)
    {
        Item curAllItem = AllItemList.Find(x => x.Id == id.ToString());
        if (curAllItem != null)
        {
            MyItemList.Add(curAllItem);
        }
        ItemSave();
    }

    public void RemoveItemClick(int id)
    {
        Item curItem = MyItemList.Find(x => x.Id == id.ToString());
        if (curItem != null)
        {
            int curNumber = int.Parse(curItem.Number) - int.Parse(ItemNumberInput.text);

            if (curNumber <= 0)
                MyItemList.Remove(curItem);
            else
                curItem.Number = curNumber.ToString();
        }
    }

    public void ResetItemClick()
    {
        MyItemList.Clear();
        ItemSave();
    }

    void ItemSave()
    {
    }

    void Load()
    {
    }


}
