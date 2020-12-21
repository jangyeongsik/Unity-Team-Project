using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TalkData
{
    public class Talk
    {
        public string name;
        public List<string> T_data;
    }
}

public class TextData : SingletonMonobehaviour<TextData>
{
    public List<TalkData.Talk> TalkBox;
    public void test()
    {
        TalkBox = new List<TalkData.Talk>();
        TalkData.Talk a = new TalkData.Talk();
        a.name = "밍";
        a.T_data = new List<string>();
        a.T_data.Add("안녕");
        a.T_data.Add("어디가니?");
        a.T_data.Add("저쪽이야");
        TalkBox.Add(a);
    }
}
