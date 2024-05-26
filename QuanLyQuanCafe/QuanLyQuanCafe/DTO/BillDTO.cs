using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class BillDTO
    {
        private int iD;
        private DateTime? checkIn;
        private DateTime? checkOut;
        private int status;
        private int discount;

        public int ID { get => iD; set => iD = value; }
        public DateTime? CheckIn { get => checkIn; set => checkIn = value; }
        public DateTime? CheckOut { get => checkOut; set => checkOut = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }

        public BillDTO(int id, DateTime? checkIn, DateTime? checkOut, int status, int discount)
        {
            this.ID = id;
            this.CheckIn = checkIn;
            this.CheckOut = checkOut;
            this.Status = status;
            this.Discount = discount;
        }

        public BillDTO(DataRow row)
        {
            this.ID = (int)row["id"];
            this.CheckIn = (DateTime?)row["check_in"];
            var checkOutTemp = row["check_out"];
            if(checkOutTemp.ToString() != "")
            {
                this.CheckOut = (DateTime?)checkOutTemp;
            }            
            this.Status = (int)row["status"];
            this.Discount = (int)row["discount"];
        }
    }
}
