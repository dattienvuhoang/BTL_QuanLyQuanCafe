using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set => instance = value;
        }

        private BillDAO() { }


        //Hàm lấy ra IDBill dựa vào IDDinnerTable
        //Thành công trả về: IDBill
        //Thất bại trả về: -1
        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM bill WHERE dinner_table_id = " + id + " AND status = 0");
            if(data.Rows.Count > 0)
            {
                BillDTO bill = new BillDTO(data.Rows[0]);
                return bill.ID;
            }
            return -1; //ko có bản ghi nào
        }

        //Hàm thêm mới hoá đơn
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBill @dinner_table_id", new object[]{id});
        }
        public DataTable GetBillByDate(DateTime checkin, DateTime checkout)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_GetListBillByDate @checkin , @checkout",new object[] {checkin, checkout});
        }
        //Hàm lấy ra IDBill lớn nhất
        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM bill");
            }
            catch
            {
                return 1;
            }
        }

        //Hàm thanh toán hoá đơn
        public void CheckOut(int id, int discount, float toltal)
        {
            string query = "UPDATE bill SET check_out = GETDATE(), status = 1, " + "discount = " + discount +", toltal = " + toltal+ " WHERE id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}
