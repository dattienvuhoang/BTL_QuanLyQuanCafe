using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        //Design patern Singleton
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        //Khai báo chuỗi kết nối
        private string connectionSTR = "Data Source=VUTIENDAT\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        //Trả về các dòng kết quả (dữ liệu)
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            //Dùng using để tự giải phóng dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection); //SqlCommand dùng để thực thi query
                if (parameter != null)
                {
                    string[] listPara = query.Split(' '); //Split: tách chuỗi, ngăn cách bởi khoảng trắng
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@')) //Contains: Kiểm tra sự tồn tại của '@' trong mỗi item thuộc listPara (kết quả trả về true/false), nếu có @ thì item đó là 1 Parameter
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data); //đổ dữ liệu lấy ra vào DataTable
                connection.Close();
            }

            return data;
        }

        //Trả về số dòng được thực thi khi excute (insert, update, delete...)
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        //Trả về số lượng (ví dụ trả về n khi SELECT COUNT(*))
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
                connection.Close();
            }

            return data;
        }
    }
}
