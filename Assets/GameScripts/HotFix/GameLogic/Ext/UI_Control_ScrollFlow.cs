using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic
{
    public class UI_Control_ScrollFlow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public RectTransform Rect;
        public List<UI_Control_ScrollFlow_Item> Items;
        public GameObject ItemPre;
        public int ItemCount;
        /// <summary>
        /// 宽度
        /// </summary>
        public float Width = 500;
        /// <summary>
        /// 大小
        /// </summary>
        public float MaxScale = 1;
        /// <summary>
        /// StartValue开始坐标值，AddValue间隔坐标值，小于vmian 达到最左，大于vmax达到最右
        /// </summary>
        public int StartValue = 1, AddValue = 20, VMin = 10, VMax = 90;
        /// <summary>
        /// 坐标曲线
        /// </summary>
        public AnimationCurve PositionCurve;
        /// <summary>
        /// 大小曲线
        /// </summary>
        public AnimationCurve ScaleCurve;
        /// <summary>
        /// 透明曲线
        /// </summary>
        public AnimationCurve ApaCurve;
        /// <summary>
        /// 计算值
        /// </summary>
        public Vector2 start_point, add_vect;
        /// <summary>
        /// 动画状态
        /// </summary>
        public bool _anim = false;
        /// <summary>
        /// 动画速度
        /// </summary>
        public int _anim_speed = 1;

        public bool CanUpDrag = true;//判断是否拖拽
        public bool CanDownDrag = true;//判断是否拖拽
        public int currentNum;//当前限制数
        private int v = 0;
        private List<UI_Control_ScrollFlow_Item> GotoFirstItems = new List<UI_Control_ScrollFlow_Item>(), GotoLaserItems = new List<UI_Control_ScrollFlow_Item>();
        //public event CallBack<UI_Control_ScrollFlow_Item> MoveEnd;

        public int minToMiddle = 50;
        public int changeI = 0;

        public UI_Control_ScrollFlow_Item ItemUp;
        public UI_Control_ScrollFlow_Item ItemDown;
        public bool isRepairError = false; //有时候会都堆在一起
        public void Start()
        {
            Init();
        }
        public void Init()
        {
            isRepairError = false;
            if (gameObject.name == "SelectData")
            {
                ItemCount = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            }
            CanUpDrag = true;
            CanDownDrag = true;
            for (int i = 0; i < ItemCount; i++)
            {
                GameObject t = Instantiate(ItemPre, Rect);
                UI_Control_ScrollFlow_Item item = t.GetComponent<UI_Control_ScrollFlow_Item>();
                if (item != null)
                {
                    if (gameObject.name == "SelectMinute")
                    {
                        int current = DateTime.Now.Minute + i - 3;
                        currentNum = DateTime.Now.Minute;
                        if (current > 59)
                        {
                            current -= 60;
                        }
                        else if (current < 0)
                        {
                            current = current + 60;
                        }
                        if (current < 10)
                        {
                            item.GetComponent<Text>().text = "0" + current.ToString();
                        }
                        else
                        {
                            item.GetComponent<Text>().text = current.ToString();
                        }
                        item.name = current.ToString();
                    }
                    else if (gameObject.name == "SelectHour")
                    {
                        int current = DateTime.Now.Hour + i - 3;
                        currentNum = DateTime.Now.Hour;
                        if (current > 12)
                        {
                            current -= 12;
                        }
                        else if (current <= 0)
                        {
                            current = current + 12;
                        }
                        if (current < 10)
                        {
                            item.GetComponent<Text>().text = "0" + current.ToString();
                        }
                        else
                        {
                            item.GetComponent<Text>().text = current.ToString();
                        }
                        item.name = current.ToString();
                    }
                    else if (gameObject.name == "SelectMonth")
                    {
                        int current = DateTime.Now.Month + i - 3;
                        currentNum = DateTime.Now.Month;
                        if (current > 12)
                        {
                            current -= 12;
                        }
                        else if (current < 0)
                        {
                            current = current + 12;
                        }
                        if (current < 10)
                        {
                            item.GetComponent<Text>().text = "0" + current.ToString();
                        }
                        else
                        {
                            item.GetComponent<Text>().text = current.ToString();
                        }
                        item.name = current.ToString();
                    }
                    else if (gameObject.name == "SelectData")
                    {
                        int current = DateTime.Now.Day + i - 3;
                        currentNum = DateTime.Now.Day;
                        if (current > ItemCount)
                        {
                            current -= ItemCount;
                        }
                        else if (current < 0)
                        {
                            current = current + ItemCount;
                        }
                        if (current < 10)
                        {
                            item.GetComponent<Text>().text = "0" + current.ToString();
                        }
                        else
                        {
                            item.GetComponent<Text>().text = current.ToString();
                        }
                        item.name = current.ToString();
                    }
                    else if (gameObject.name == "SelectTime")
                    {
                        int current = 8 + i - 3;
                        currentNum = 8;
                        if (current > ItemCount)
                        {
                            current -= ItemCount;
                        }
                        else if (current < 0)
                        {
                            current = current + ItemCount;
                        }
                        if (current < 10)
                        {
                            item.GetComponent<Text>().text = "0" + current.ToString();
                        }
                        else
                        {
                            item.GetComponent<Text>().text = current.ToString();
                        }
                        item.name = current.ToString();
                    }
                    Items.Add(item);
                    if (gameObject.name == "SelectAmOrPm")
                    {
                        middleV = 30;
                        StartValue = 0;
                        item.Init(this, true);
                        item.Drag(StartValue + (i + 1) * AddValue);
                    }
                    else
                    {
                        middleV = 45;
                        item.Init(this, false);
                        item.Drag(StartValue + i * AddValue);
                    }
                    if (item.ToMiddle < minToMiddle)
                    {
                        minToMiddle = item.ToMiddle;
                        CurrentItem = Items[i];
                        CurrentIndex = i;
                    }
                }
            }
            minToMiddle = 100;
            ItemDown = Items[0];
            ItemUp = GetMaximumItem();
            if (gameObject.name == "SelectAmOrPm")
            {
                Items[1].txt.text = "am";
                Items[0].txt.text = GetOther("am");
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            Items.Clear();
        }
        public string GetOther(string one)
        {
            if (one == "am")
            {
                return "pm";
            }
            else
            {
                return "am";
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            isRepairError = true;
            if (!_anim)
            {
                add_vect = start_point;
                v = (int)(eventData.delta.y * 100f / Width);
                if (gameObject.name == "SelectAmOrPm")
                {
                    if (Items[0].v > 45 || Items[1].v > 45)
                    {
                        CanUpDrag = false;
                    }
                    else if (Items[0].v < 45 && Items[1].v < 45)
                    {
                        CanUpDrag = true;
                    }
                    if (Items[0].v < 15f || Items[1].v < 15f)
                    {
                        CanDownDrag = false;
                    }
                    else if (Items[0].v > 15f && Items[1].v > 15f)
                    {
                        CanDownDrag = true;
                    }
                }
                else
                {
                    CanUpDrag = true;
                    CanDownDrag = true;
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_anim)
            {
                add_vect = eventData.position - start_point;
                start_point = eventData.position;
                v = (int)(eventData.delta.y * 100 / Width);
                if (gameObject.name == "SelectAmOrPm")
                {
                    if (Items[0].v > 45f || Items[1].v > 45f)
                    {
                        CanUpDrag = false;
                    }
                    else if (Items[0].v < 45f && Items[1].v < 45f)
                    {
                        CanUpDrag = true;
                    }
                    if (Items[0].v < 15f || Items[1].v < 15f)
                    {
                        CanDownDrag = false;
                    }
                    else if (Items[0].v > 15f && Items[1].v > 15f)
                    {
                        CanDownDrag = true;
                    }
                }
                else
                {
                    CanUpDrag = true;
                    CanDownDrag = true;
                }
                if ((CanUpDrag && v > 0) || (CanDownDrag && v < 0))
                {
                    changeI += 1;
                    for (int i = 0; i < Items.Count; i++)
                    {
                        Items[i].Drag(v);
                    }
                }
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].ToMiddle < minToMiddle)
                    {
                        minToMiddle = Items[i].ToMiddle;
                        CurrentItem = Items[i];
                        CurrentIndex = i;
                    }
                }
                Check(v);
            }
            minToMiddle = 100;
        }

        public void Check(float _v)
        {
            if (_v < 0)
            {
                //向下运动
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].v < -5f)
                    {
                        index = 0;
                        for (int j = 0; j < GotoLaserItems.Count; j++)
                        {
                            if (Items[i].v >= GotoLaserItems[j].v)
                            {
                                index = j + 1;
                            }
                        }
                        GotoLaserItems.Insert(index, Items[i]);
                    }
                }
                if (GotoLaserItems.Count > 0)
                {
                    GotoLaserItems[0].v = ItemUp.v + AddValue;
                    for (int i = 1; i < GotoLaserItems.Count; i++)
                    {
                        GotoLaserItems[i].v = GotoLaserItems[i - 1].v + AddValue;
                    }
                    ItemDown = GetMinimumItem();
                    ItemUp = GotoLaserItems[GotoLaserItems.Count - 1];

                    GotoLaserItems.Clear();
                }
            }
            else if (_v > 0)
            {
                //向上运动
                for (int i = Items.Count - 1; i >= 0; i--)
                {
                    if (Items[i].v > 95f)
                    {
                        index = 0;
                        for (int j = 0; j < GotoFirstItems.Count; j++)
                        {
                            if (Items[i].v <= GotoFirstItems[j].v)
                            {
                                index = j + 1;
                            }
                        }
                        GotoFirstItems.Insert(index, Items[i]);
                    }
                }
                if (GotoFirstItems.Count > 0)
                {
                    GotoFirstItems[0].v = ItemDown.v - AddValue;
                    for (int i = 1; i < GotoFirstItems.Count; i++)
                    {
                        GotoFirstItems[i].v = GotoFirstItems[i - 1].v - AddValue;
                    }
                    ItemUp = GetMaximumItem();
                    ItemDown = GotoFirstItems[GotoFirstItems.Count - 1];
                    GotoFirstItems.Clear();
                }
            }

        }
        public UI_Control_ScrollFlow_Item GetMaximumItem()
        {
            int max = 0;
            for (int i = 1; i < Items.Count; i++)
            {
                if (Items[i].v >= Items[max].v)
                {
                    max = i;
                }
            }
            return Items[max];
        }
        public UI_Control_ScrollFlow_Item GetMinimumItem()
        {
            int min = 0;
            for (int i = 1; i < Items.Count; i++)
            {
                if (Items[i].v <= Items[min].v)
                {
                    min = i;
                }
            }
            return Items[min];
        }
        public int middleV = 45;
        public void OnEndDrag(PointerEventData eventData)
        {
            int k = 0;
            if (!_anim)
            {
                k = middleV - Items[CurrentIndex].v;
                AnimToEnd(k);
                add_vect = Vector2.zero;
            }
        }
        public int getLatestItem()
        {
            int i = CurrentIndex;
            int iLength = 0;
            while (int.Parse(Items[i].txt.text) < currentNum)
            {
                i--;
                iLength++;
                if (i < 0)
                {
                    i = Items.Count - 1;
                }
            }
            int j = CurrentIndex;
            int jLength = 0;
            while (int.Parse(Items[j].txt.text) < currentNum)
            {
                j++;
                jLength++;
                if (j >= Items.Count)
                {
                    j = 0;
                }
            }
            if (jLength < iLength)
            {
                return j;
            }
            else
            {
                return i;
            }
        }
        /// <summary>
        /// 点击时滑动
        /// </summary>
        /// <param name="eventData"></param>
        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    if (add_vect.sqrMagnitude <= 1)
        //    {
        //        UI_Control_ScrollFlow_Item script = eventData.pointerPressRaycast.gameObject.GetComponent<UI_Control_ScrollFlow_Item>();
        //        Debug.Log("============OnPointerClickOK============");
        //        MoveToPoint(script);
        //    }
        //}
        //public void MoveToPoint(UI_Control_ScrollFlow_Item script)
        //{
        //    if (script != null)
        //    {
        //        if (gameObject.name != "SelectAmOrPm")
        //        {
        //            int k = script.v;
        //            k = 45 - k;
        //            AnimToEnd(k);
        //        }
        //        else
        //        {
        //            int k = script.v;
        //            k = 30 - k;
        //            AnimToEnd(k);
        //        }
        //    }
        //}

        public float GetApa(float v)
        {
            return ApaCurve.Evaluate(v);
        }
        public float GetPosition(float v)
        {
            return PositionCurve.Evaluate(v) * Width;
        }
        public float GetScale(float v)
        {
            return ScaleCurve.Evaluate(v) * MaxScale;
        }

        private List<UI_Control_ScrollFlow_Item> SortValues = new List<UI_Control_ScrollFlow_Item>();
        private int index = 0;
        public void ToLaster(UI_Control_ScrollFlow_Item item)
        {
            item.v = Items[Items.Count - 1].v + AddValue;
            Items.Remove(item);
            Items.Add(item);
        }


        public int AddV = 0, Vk = 0, CurrentV = 0, Vtotal = 0, VT = 0;


        public UI_Control_ScrollFlow_Item CurrentItem;
        public int CurrentIndex;
        //public int CurrentNum;


        public void AnimToEnd(int k)
        {
            AddV = k;
            if (AddV > 0) { Vk = AddValue; }
            else if (AddV < 0) { Vk = -AddValue; }
            else
            {
                return;
            }
            Vtotal = 0;
            _anim = true;
        }

        void Update()
        {
            if (_anim)
            {
                CurrentV = (int)(Time.deltaTime * _anim_speed * Vk);
                VT = Vtotal + CurrentV;
                if ((Vk > 0 && VT >= AddV) || (Vk < 0 && VT <= AddV))
                {
                    _anim = false;
                    CurrentV = AddV - Vtotal;
                }

                changeI += 1;
                for (int i = 0; i < Items.Count; i++)
                {
                    Items[i].Drag(CurrentV);
                    if (Items[i].ToMiddle < minToMiddle)
                    {
                        minToMiddle = Items[i].ToMiddle;
                        CurrentItem = Items[i];
                        CurrentIndex = i;
                        if ((gameObject.name == "SelectMonth" || gameObject.name == "SelectData"))
                        {
                            int t = CurrentIndex + 1;
                            if (t >= Items.Count)
                            {
                                t = 0;
                            }
                            if (int.Parse(Items[t].txt.text) < currentNum)
                            {
                                CanDownDrag = false;
                            }
                            else
                            {
                                CanDownDrag = true;
                            }
                            t = CurrentIndex - 1;
                            if (t < 0)
                            {
                                t = Items.Count - 1;
                            }
                            if (int.Parse(Items[t].txt.text) < currentNum)
                            {
                                CanUpDrag = false;
                            }
                            else
                            {
                                CanUpDrag = true;
                            }
                        }
                    }
                }
                Check(CurrentV);
                Vtotal = VT;
                minToMiddle = 100;
                if (!_anim)
                {
                    // if (MoveEnd != null) { MoveEnd(CurrentItem); }
                }
            }
            if (!isRepairError)
            {
                if (CurrentItem != null && CurrentItem.rect.localPosition.y != GetPosition(CurrentItem.v))
                {
                    isRepairError = true;
                    for (int i = 0; i < Items.Count; i++)
                    {
                        Items[i].Drag(0);
                    }
                }
            }
        }
        public int middleIndex = 0;
        public void LateUpdate()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                index = 0;
                for (int j = 0; j < SortValues.Count; j++)
                {
                    if (Items[i].v <= SortValues[j].v)
                    {
                        index = j + 1;
                    }
                }
                SortValues.Insert(index, Items[i]);
            }

            for (int k = 0; k < SortValues.Count; k++)
            {
                SortValues[k].rect.SetSiblingIndex(k);
                if (SortValues[k].txt.text == CurrentItem.txt.text)
                {
                    middleIndex = k;
                }
            }
            for (int k = 0; k < SortValues.Count; k++)
            {
                if (gameObject.name != "SelectAmOrPm")
                {
                    int current = int.Parse(CurrentItem.txt.text) + (k - middleIndex);
                    if (gameObject.name == "SelectMinute")
                    {
                        if (current > 59)
                        {
                            current -= 60;
                        }
                        else if (current < 0)
                        {
                            current = current + 60;
                        }
                        if (current < 10)
                        {
                            SortValues[k].txt.text = "0" + current.ToString();
                        }
                        else
                        {
                            SortValues[k].txt.text = current.ToString();
                        }
                    }
                    else if (gameObject.name == "SelectHour")
                    {
                        if (current > 12)
                        {
                            current -= 12;
                        }
                        else if (current <= 0)
                        {
                            current = current + 12;
                        }
                        if (current < 10)
                        {
                            SortValues[k].txt.text = "0" + current.ToString();
                        }
                        else
                        {
                            SortValues[k].txt.text = current.ToString();
                        }
                    }
                    else if (gameObject.name == "SelectMonth")
                    {
                        if (current > 12)
                        {
                            current -= 12;
                        }
                        else if (current < 0)
                        {
                            current = current + 12;
                        }
                        if (current < 10)
                        {
                            SortValues[k].txt.text = "0" + current.ToString();
                        }
                        else
                        {
                            SortValues[k].txt.text = current.ToString();
                        }
                    }
                    else if (gameObject.name == "SelectData")
                    {
                        if (current > ItemCount)
                        {
                            current -= ItemCount;
                        }
                        else if (current < 0)
                        {
                            current = current + ItemCount;
                        }
                        if (current < 10)
                        {
                            SortValues[k].txt.text = "0" + current.ToString();
                        }
                        else
                        {
                            SortValues[k].txt.text = current.ToString();
                        }
                    }
                    else if (gameObject.name == "SelectTime")
                    {
                        if (current > ItemCount)
                        {
                            current -= ItemCount;
                        }
                        else if (current < 0)
                        {
                            current = current + ItemCount;
                        }
                        if (current < 10)
                        {
                            SortValues[k].txt.text = "0" + current.ToString();
                        }
                        else
                        {
                            SortValues[k].txt.text = current.ToString();
                        }
                    }
                }
            }
            SortValues.Clear();
        }

    }
}
