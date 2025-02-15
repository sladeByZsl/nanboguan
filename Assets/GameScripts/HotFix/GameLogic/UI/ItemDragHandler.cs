using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameLogic
{
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private LevelCommon m_parent;
        private int m_itemIndex;
        private RectTransform m_rectTransform;
        private Canvas m_canvas;

        public void Init(LevelCommon parent, int itemIndex)
        {
            m_parent = parent;
            m_itemIndex = itemIndex;
            m_rectTransform = GetComponent<RectTransform>();
            m_canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // 可以在这里添加开始拖拽的视觉效果
            //m_canvas.sortingLayerName = Global.UI_TOP;
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_rectTransform.anchoredPosition += eventData.delta / m_canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //m_canvas.sortingLayerName = Global.UI;
            m_parent.OnItemDragEnd(m_itemIndex, eventData);
        }
    }
}
