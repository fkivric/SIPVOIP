using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MlSampleWindowCS
{
    public class DbQuery
    {
        public static DataTable Query(string Sorgu, string Connection)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection);
            SqlDataAdapter da = new SqlDataAdapter(Sorgu, conn);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else { return null; }
        }

        public static void insertquery(string query, string Connection)
        {
            using (SqlConnection conn = new SqlConnection(Connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 0;
                    if (query != null)
                        cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public static string GetValue(string query, string Connection)
        {
            using (SqlConnection sql = new SqlConnection(Connection))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, sql);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public bool InsertCallList(SIPPhoneForm.CallList callList,SqlConnection connections)
        {
            // SQL INSERT komutunu hazırlayalım
            string query = "INSERT INTO dbo.CallList (CallType, CallNumber, CallRecall, CallUser) " +
                           "VALUES (@CallType, @CallNumber, @CallRecall, @CallUser)";

            using (SqlConnection connection = connections)
            {
                // Bağlantıyı aç
                connection.Open();

                // Komut oluştur
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parametreleri ekle
                    command.Parameters.AddWithValue("@CallType", callList.CallType);
                    command.Parameters.AddWithValue("@CallNumber", callList.CallNumber);
                    command.Parameters.AddWithValue("@CallRecall", callList.CallRecall);
                    command.Parameters.AddWithValue("@CallUser", callList.CallUser);

                    // Komutu çalıştır
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
