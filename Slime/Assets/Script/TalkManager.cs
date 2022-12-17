using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;


    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenrateData();
    }
    void GenrateData()
    {
        talkData.Add(0, new string[] { "* ���� ù ��° ����." });
        talkData.Add(1, new string[] { "* ��...", "�� �� ��° ����." });
        talkData.Add(2, new string[] { "����.", " ���ڰ� ����ִ�." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
