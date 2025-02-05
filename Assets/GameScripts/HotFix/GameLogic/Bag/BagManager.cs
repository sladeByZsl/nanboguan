using System.Collections;
using System.Collections.Generic;
using GameBase;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class BagManager : Singleton<BagManager>
    {
        public List<int> itemList = new List<int>();
        public BagManager()
        {
        }

        public void AddItem(int id)
        {
            if (!itemList.Contains(id))
            {
                itemList.Add(id);
                GameEvent.Send(ClientEventID.AddItem,id);
            }
        }
        public List<int> GetItemList()
        {
            return itemList;
        }

        public void UseItem(int itemId)
        {
            if (itemList.Contains(itemId))
            {
                itemList.Remove(itemId);
                GameEvent.Send(ClientEventID.UseItem,itemId);
            }
        }
    }
}
