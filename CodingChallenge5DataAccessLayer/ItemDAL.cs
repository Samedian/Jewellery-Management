using CodingChallenge5Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace CodingChallenge5DataAccessLayer
{
    public class ItemDAL : IItemDAL
    {

        private SqlConnection sqlConnection;

        public IConfiguration GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        private void Connection()
        {
            var configuration = GetConnectionString();
            sqlConnection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DataBaseConnection").Value);
           //sqlConnection = new SqlConnection("Data Source=.;Initial Catalog=CodingChallenge5;Integrated Security=true");
        }
        public List<ItemEntity> GetItemDAL()
        {
            DataTable dataTable = new DataTable();
            List<ItemEntity> itemEntities = new List<ItemEntity>();


            try
            {
                Connection();
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("DisplayItem", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            catch (SqlException ex)
            {
                throw ex.InnerException;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            foreach (DataRow dataRow in dataTable.Rows)
            {
                itemEntities.Add(
                    new ItemEntity
                    {
                        ItemName = Convert.ToString(dataRow["ItemName"]),
                        itemQuantity = Convert.ToInt32(dataRow["quantity"])
                    });
            }
            return itemEntities;

        }

        public bool AddItemDAL(ItemEntity itemEntity)
        {
            int i = 0;

            ItemDAL itemDAL = new ItemDAL();
            List<ItemEntity> itemEntities = itemDAL.GetItemDAL();
            ItemEntity item = itemEntities.Find(options => options.ItemName == itemEntity.ItemName);

            try
            {
                Connection();
                sqlConnection.Open();

                if (item != null)
                {

                    SqlCommand sqlCommand = new SqlCommand("updateItem", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ItemName", itemEntity.ItemName);
                    sqlCommand.Parameters.AddWithValue("@ItemQuantity", itemEntity.itemQuantity);

                    i = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                else
                {
                    SqlCommand sqlCommand = new SqlCommand("AddItem", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ItemName", itemEntity.ItemName);
                    sqlCommand.Parameters.AddWithValue("@quantity", itemEntity.itemQuantity);

                    i = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
            }catch (SqlException ex)
            {
                throw ex.InnerException;
            }catch (Exception ex)
            {
                throw ex.InnerException;
            }
            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool SoldItemDAL(SoldItemDetailEntity soldItemDetailEntity)
        {
            int i = 0;

            ItemDAL itemDAL = new ItemDAL();
            List<ItemEntity> itemEntities = itemDAL.GetItemDAL();
            ItemEntity item = itemEntities.Find(options => options.ItemName == soldItemDetailEntity.ItemName);
            try
            {
                Connection();
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("updateItemSub", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ItemName", soldItemDetailEntity.ItemName);
                sqlCommand.Parameters.AddWithValue("@ItemQuantity", soldItemDetailEntity.Quantity);

                i = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                
                Connection();
                sqlConnection.Open();
                sqlCommand = new SqlCommand("AddSellingDetail", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@itemName", soldItemDetailEntity.ItemName);
                sqlCommand.Parameters.AddWithValue("@quantity", soldItemDetailEntity.Quantity);
                sqlCommand.Parameters.AddWithValue("@amount", soldItemDetailEntity.Amount);


                i = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();


            }
            catch (SqlException ex)
            {
                throw ex.InnerException;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            if (i >= 1)
                return true;
            else
                return false;

        }
    }
}

