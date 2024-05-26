using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe
{
    static class TruyXuatCSDL
    {
        private static String ConnString = "Data Source=VUTIENDAT\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
        private static SqlConnection TaoKetNoi()
        {
            return new SqlConnection(ConnString);
        }
        public static DataTable LayBang(String sql)
        {
            SqlConnection conn  = TaoKetNoi();
            conn.Open();
            SqlDataAdapter dataAdapter  = new SqlDataAdapter(sql,conn);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            conn.Close();
            dataAdapter.Dispose();
            return dt;
        }
        public static void ThemSuaXoa(String sql)
        {
            SqlConnection conn = TaoKetNoi();
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();
        }
    }
}
