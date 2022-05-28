using CodingChallenge5Entities;
using System.Collections.Generic;

namespace CodingChallenge5BussinessLayer
{
    public interface IItemBAL
    {
        bool AddItemBAL(ItemEntity itemEntity);
        List<ItemEntity> GetItemBAL();
        bool AddSoldItem(SoldItemDetailEntity soldItemDetailEntity);
    }
}