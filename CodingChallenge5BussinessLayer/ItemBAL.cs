using CodingChallenge5DataAccessLayer;
using CodingChallenge5Entities;
using System;
using System.Collections.Generic;

namespace CodingChallenge5BussinessLayer
{
    public class ItemBAL : IItemBAL
    {
        IItemDAL _itemDAL;
        public ItemBAL(IItemDAL itemDAL)
        {
            _itemDAL = itemDAL;
        }
        public List<ItemEntity> GetItemBAL()
        {
            List<ItemEntity> itemEntities = _itemDAL.GetItemDAL();
            return itemEntities;
        }

        public bool AddItemBAL(ItemEntity itemEntity)
        {
            bool result;
            result = _itemDAL.AddItemDAL(itemEntity);
            return result;

        }

        public bool AddSoldItem(SoldItemDetailEntity soldItemDetailEntity)
        {
            bool result;
            result = _itemDAL.SoldItemDAL(soldItemDetailEntity);
            return result;
        }
    }
}
