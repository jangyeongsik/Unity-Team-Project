using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Conversation : MonoBehaviour
{
    public Text npcText;
    private bool startText = false;
    int start;
    int count;
    void Start()
    {
        TextData.Instance.test();
        
    }

    public void onEventText(string name, int textCount)
    {
        for (int i = 0; i < TextData.Instance.TalkBox.Count; i++)   
        {
            if (TextData.Instance.TalkBox[i].name == name)
            {
                start = i;
                startText = true;
                break;
            }
        }

        npcText.text = TextData.Instance.TalkBox[start].T_data[count];
    }

    private void Update()
    {
        if (startText)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (count < TextData.Instance.TalkBox[start].T_data.Count)
                    npcText.text = TextData.Instance.TalkBox[start].T_data[count++];
            }
        }


        //중간 연결 다리 만들어 인겜신 UI신 연결 
        //다리    - 싱글톤, 
        //        - UI쪽에서 해야하는게,~ 매개변수로 태그를 받아서, 그 태그에 맞는 대사, 출력 하는 함수. (LIST로 구현) , 리스트에 타입 = 텍스트 데이터 
        //          list<Type> == TAG 가 매개변수로 받은 태그랑 같으면, 대사 출력. 
        // ui에 있는 함수를 실행 시켜주는 곳은 다리 싱글톤 함수 . 
        //싱글톤 함수는 플레이어가 호출 .

        //
    }

}
