using CodingChallenge5Entities;
using System.Collections.Generic;

namespace CodingChallenge5.Models
{
    public interface IModelManager
    {
        List<Item> ConvertItemEntitiesToItem(List<ItemEntity> itemEntities);
        ItemEntity ConvertItemToItemEntity(Item item);
        SoldItemDetailEntity ConvertSoldItemToEntity(SoldItemDetail soldItemDetail);
    }
}