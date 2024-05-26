using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class BillDetailDTO
    {
        private int iD;
        private int billID;
        private int foodID;
        private int quantity;

        public int ID { get => iD; set => iD = value; }
        public int BillID { get => billID; set => billID = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        public BillDetailDTO(int id, int billID, int foodID, int quantity)
        {
            this.ID = id;
            this.BillID = billID;
            this.FoodID = foodID;
            this.Quantity = quantity;
        }

        public BillDetailDTO(DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillID = (int)row["bill_id"];
            this.FoodID = (int)row["food_id"];
            this.Quantity = (int)row["quantity"];
        }
    }
}
