using CodingChallenge5.Models;
using CodingChallenge5BussinessLayer;
using CodingChallenge5Entities;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge5.Controllers
{

    public class ItemController : Controller
    {
        IItemBAL _itemBAL;
        IModelManager _modelManager;
        IWrite _write;
        double goldPrice = 24878.98;

        public ItemController(IItemBAL itemBAL, IModelManager modelManager, IWrite write)
        {
            _itemBAL = itemBAL;
            _modelManager = modelManager;
            _write = write;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetItem()
        {
            List<ItemEntity> itemEntities = _itemBAL.GetItemBAL();
            try
            {
                List<Item> items = _modelManager.ConvertItemEntitiesToItem(itemEntities);
                ViewBag.Data = items;
                _write.WriteData("Data Fetched Successfully");

            }
            catch (SqlException ex)
            {
                _write.WriteData("Exception Occured " + ex.Message);
            }
            catch (Exception ex)
            {
                _write.WriteData("Exception Occured " + ex.Message);

            }
            return View();
        }

        [HttpGet]
        public IActionResult SetGoldPrice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetGoldPrice(double GoldPrice, string Submit)
        {
            if (!string.IsNullOrEmpty(Submit))
            {
                goldPrice = GoldPrice;
                _write.WriteData("Gold Price Updated ");

            }

            return View("Index");
        }
        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }


        [HttpPost]
        public JsonResult AddItem(Item item)
        {

            String message = null;

            bool result;
            ItemEntity itemEntity = _modelManager.ConvertItemToItemEntity(item);
            try
            {
                result = _itemBAL.AddItemBAL(itemEntity);
                message = "SUCEESS";
                _write.WriteData("Data Added Successfully for " + item.ItemName);

                return Json(new { Message = message, System.Web.Mvc.JsonRequestBehavior.AllowGet });

            }
            catch (SqlException ex)
            {
                message = ex.Message;
                _write.WriteData("Exception Occured " + ex.Message);

            }
            catch (Exception ex)
            {
                message = ex.Message;
                _write.WriteData("Exception Occured " + ex.Message);

            }

            return Json(new { Message = message, System.Web.Mvc.JsonRequestBehavior.AllowGet });
        }

        [HttpGet]
        public IActionResult SellItem()
        {
            List<ItemEntity> itemEntities = _itemBAL.GetItemBAL();
            try
            {
                List<Item> items = _modelManager.ConvertItemEntitiesToItem(itemEntities);
                HashSet<string> data = new HashSet<string>();

                foreach (var item in items)
                {
                    data.Add(item.ItemName);
                }
                ViewBag.Data = data;
                ViewBag.GoldPrice = goldPrice;
                _write.WriteData("Data Fetched Successfully");

            }
            catch (SqlException ex)
            {
                _write.WriteData("Exception Occured " + ex.Message);
            }
            catch (Exception ex)
            {
                _write.WriteData("Exception Occured " + ex.Message);

            }

            return View();
        }
        [HttpPost]
        public IActionResult SellItem(SoldItemDetail soldItemDetail)
        {
            SoldItemDetailEntity soldItemDetailEntity = _modelManager.ConvertSoldItemToEntity(soldItemDetail);
            bool result;

            try
            {
                result = _itemBAL.AddSoldItem(soldItemDetailEntity);
                _write.WriteData("Data Sold Successfully for " + soldItemDetailEntity.ItemName);

            }
            catch (SqlException ex)
            {
                _write.WriteData("Exception Occured " + ex.Message);

            }
            catch (Exception ex)
            {
                _write.WriteData("Exception Occured " + ex.Message);

            }

            return View("Index");

        }



    }
}
