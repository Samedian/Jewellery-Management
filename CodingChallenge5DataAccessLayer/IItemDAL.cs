using CodingChallenge5Entities;
using System.Collections.Generic;

namespace CodingChallenge5DataAccessLayer
{
    public interface IItemDAL
    {
        bool AddItemDAL(ItemEntity itemEntity);
        List<ItemEntity> GetItemDAL();
        bool SoldItemDAL(SoldItemDetailEntity soldItemDetailEntity);
    }
}