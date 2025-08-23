using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_Control_ScrollFlow_Item : MonoBehaviour
    {
        private UI_Control_ScrollFlow parent;
        [HideInInspector]
        public RectTransform rect;
        public Text txt;
        public int v = 0;
        private Vector3 p, s;
        public int ToMiddle = 0;
        /// <summary>
        /// 缩放值
        /// </summary>
        public float sv;
        // public float index = 0,index_value;
        private Color color;
        public int middleV = 0;
        public void Init(UI_Control_ScrollFlow _parent, bool isam)
        {
            rect = this.GetComponent<RectTransform>();
            txt = this.GetComponent<Text>();
            parent = _parent;
            color = txt.color;
            if (isam)
                middleV = 30;
            else
                middleV = 45;
        }
        public void Drag(int value)
        {
            v += value;
            p = rect.localPosition;
            p.y = parent.GetPosition((float)v / 100);
            rect.localPosition = p;
            color.a = parent.GetApa((float)v / 100);
            txt.color = color;
            sv = parent.GetScale((float)v / 100);
            txt.fontSize = (int)(sv * 55);

            if (v > 100 || v < -10)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
            ToMiddle = Mathf.Abs(v - middleV);
        }
    }
}
