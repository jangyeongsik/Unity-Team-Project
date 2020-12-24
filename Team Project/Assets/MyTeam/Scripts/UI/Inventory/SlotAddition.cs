using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slot
{
    public class SlotAddition : MonoBehaviour
    {
        [SerializeField]
        private Image countImage;
        private Text countText;

        private void Start()
        {
            countText = countImage.transform.GetChild(0).GetComponent<Text>();
        }
        public void SetCountText(string txt)
        {
            if (!countImage.gameObject.activeSelf)
            {
                countImage.gameObject.SetActive(true);
            }
            countText.text = txt;
        }
    }
}
