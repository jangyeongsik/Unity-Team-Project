using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slot
{
    public class SlotAddition : MonoBehaviour
    {
        public Image countImage;
        public Text countText;
        
        public void SetCountText(string txt)
        {
            countImage.gameObject.SetActive(true);
            countText.text = txt;
        }
    }
}
