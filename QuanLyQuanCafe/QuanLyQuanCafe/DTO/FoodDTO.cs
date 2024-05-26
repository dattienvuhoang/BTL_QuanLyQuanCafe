using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class FoodDTO
    {
        private int iD;
        private string foodName;
        private int categoryID;
        private float price;

        public int ID { get => iD; set => iD = value; }
        public string FoodName { get => foodName; set => foodName = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public float Price { get => price; set => price = value; }

        public FoodDTO(int id, string foodName, int categoryID, float price)
        {
            this.ID = id;
            this.FoodName = foodName;
            this.CategoryID = categoryID;
            this.Price = price;
        }

        public FoodDTO(DataRow row)
        {
            this.ID = (int)row["id"];
            this.FoodName = row["food_name"].ToString();
            this.CategoryID = (int)row["category_id"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }
    }
}
