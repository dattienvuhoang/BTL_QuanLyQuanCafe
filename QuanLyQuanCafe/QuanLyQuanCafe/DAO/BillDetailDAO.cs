using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDetailDAO
    {
        private static BillDetailDAO instance;

        public static BillDetailDAO Instance
        {
            get { if (instance == null) instance = new BillDetailDAO(); return BillDetailDAO.instance; }
            private set => instance = value;
        }

        private BillDetailDAO() { }

        //Hàm lấy ra danh sách hoá đơn chi tiết
        public List<BillDetailDTO> GetListBillDetail(int id)
        {
            List<BillDetailDTO> listBillDetail = new List<BillDetailDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM bill_detail WHERE bill_id = " + id );
            foreach(DataRow item in data.Rows)
            {
                BillDetailDTO detail = new BillDetailDTO(item);
                listBillDetail.Add(detail);
            }
            return listBillDetail;
        }


        //Hàm thêm hoá đơn chi tiết
        public void InsertBillDetail(int idBill, int idFood, int quantity)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillDetail @bill_id , @food_id , @quantity", new object[] { idBill, idFood, quantity });
        }
    }
}
