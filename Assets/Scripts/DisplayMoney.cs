using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour
{
        // Start is called before the first frame update
        private TextMeshProUGUI text;
        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            string[] temp = text.text.Split('>');
            //text.text = temp[0] + ": " + "insert money amount";
        }
    }
