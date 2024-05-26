using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuanLyQuanCafe.DTO;

namespace QuanLyQuanCafe.DAO
{
    public class DinnerTableDAO
    {
        private static DinnerTableDAO instance; 

        public static int tableWidth = 90;
        public static int tableHeight = 90;

        public static DinnerTableDAO Instance 
        {
            get { if(instance == null) instance = new DinnerTableDAO(); return DinnerTableDAO.instance; }
            private set => instance = value; 
        }

        private DinnerTableDAO() { }

        //Ko dùng return DataTable vì nó chỉ load được trong DataGridView
        //Dùng list để add từng bàn ăn (bằng cách chuyển từng row trong DataTable thành list, xử lý trong DTO)
       public List<DinnerTableDTO> LoadDinnerTableList() 
        {
            List<DinnerTableDTO> tableList = new List<DinnerTableDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetDinnerTableList");
            foreach (DataRow item in data.Rows)
            {
                DinnerTableDTO table = new DinnerTableDTO(item);
                tableList.Add(table);               
            }
            return tableList;
        }
    }
}
