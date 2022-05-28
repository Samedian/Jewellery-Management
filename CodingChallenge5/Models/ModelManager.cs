using CodingChallenge5Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge5.Models
{
    public class ModelManager : IModelManager
    {
        public List<Item> ConvertItemEntitiesToItem(List<ItemEntity> itemEntities)
        {
            List<Item> items = new List<Item>();
            foreach (var data in itemEntities)
            {
                Item item = new Item();
                item.ItemName = data.ItemName;
                item.ItemQuantity = data.itemQuantity;
                items.Add(item);
            }

            return items;
        }

        public ItemEntity ConvertItemToItemEntity(Item item)
        {
            ItemEntity itemEntity = new ItemEntity();
            itemEntity.ItemName = item.ItemName;
            itemEntity.itemQuantity = item.ItemQuantity;
            return itemEntity;
        }

        public SoldItemDetailEntity ConvertSoldItemToEntity(SoldItemDetail soldItemDetail)
        {
            SoldItemDetailEntity soldItemDetailEntity = new SoldItemDetailEntity();

            soldItemDetailEntity.ItemName = soldItemDetail.ItemName;
            soldItemDetailEntity.Quantity = soldItemDetail.Quantity;
            soldItemDetailEntity.Amount = soldItemDetail.Amount;

            return soldItemDetailEntity;
            
        }
    }
}
