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
        talkData.Add(0, new string[] { "* 나는 첫 번째 상자." });
        talkData.Add(1, new string[] { "* 흠...", "난 두 번째 상자." });
        talkData.Add(2, new string[] { "덜컥.", " 상자가 잠겨있다." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}
