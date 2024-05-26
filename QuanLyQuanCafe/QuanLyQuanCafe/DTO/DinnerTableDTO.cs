using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    //Class này lưu các thuộc tính tương ứng vs các trường của bảng dinner_table trong db
    public class DinnerTableDTO
    {
        private int iD;
        private string dinnerTableName;
        private string status;

        public int ID { get => iD; set => iD = value; }
        public string DinnerTableName { get => dinnerTableName; set => dinnerTableName = value; }
        public string Status { get => status; set => status = value; }

        public DinnerTableDTO(int id, string name, string status)
        {
            this.iD = id;
            this.dinnerTableName = name;
            this.status = status;
        }

        public DinnerTableDTO(DataRow row)
        {
            this.iD = (int)row["id"];
            this.dinnerTableName = row["dinner_table_name"].ToString();
            this.status = row["status"].ToString();
        }

    }
}
