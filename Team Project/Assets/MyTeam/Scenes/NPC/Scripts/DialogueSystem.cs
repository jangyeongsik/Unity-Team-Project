using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [TextArea] public string dialouge; 
    public Sprite Cg; 
}

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite_CG;
    [SerializeField] private SpriteRenderer dialougeBox;
    [SerializeField] private Text text_Dialouge;

    private bool isDialouge = false;

    private int count = 0;

    [SerializeField] private Dialogue[] dialouge;

    public void ShowDialogue()
    {
        Onoff(true);

        count = 0;
        NextDialouge();
    }

    private void NextDialouge()
    {
        text_Dialouge.text = dialouge[count].dialouge;
        sprite_CG.sprite = dialouge[count].Cg;
        count++;
    }

    //private void HideDialouge()
    //{
    //    dialougeBox.gameObject.SetActive(false);
    //    sprite_CG.gameObject.SetActive(false);
    //    text_Dialouge.gameObject.SetActive(false);
    //    isDialouge = false;
    //}

    private void Onoff(bool _flag)
    {
        dialougeBox.gameObject.SetActive(_flag);
        sprite_CG.gameObject.SetActive(_flag);
        text_Dialouge.gameObject.SetActive(_flag);
        isDialouge = _flag;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (count < dialouge.Length)
                NextDialouge();
            else
                Onoff(false);
        }
    }
}
