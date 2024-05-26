using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance 
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; } 
            private set => instance = value; 
        }

        private MenuDAO() { }

        public List<MenuDTO> GetListMenuByDinnerTable(int id)
        {
            string query = "SELECT food.food_name, bill_detail.quantity, food.price, food.price*bill_detail.quantity AS totalPrice FROM bill, bill_detail, food WHERE bill_detail.bill_id = bill.id AND bill_detail.food_id = food.id AND bill.status = 0 AND bill.dinner_table_id = " + id;
            List<MenuDTO> listMenu = new List<MenuDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                MenuDTO menu = new MenuDTO(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}
