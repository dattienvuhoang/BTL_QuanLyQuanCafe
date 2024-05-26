using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class CategoryDTO
    {
        private int iD;
        private string catName;

        public int ID { get => iD; set => iD = value; }
        public string CatName { get => catName; set => catName = value; }

        public CategoryDTO(int id, string catName)
        {
            this.ID = id;
            this.CatName = catName;
        }

        public CategoryDTO(DataRow row)
        {
            this.ID = (int)row["id"];
            this.CatName = row["cat_name"].ToString();
        }
    }
}
