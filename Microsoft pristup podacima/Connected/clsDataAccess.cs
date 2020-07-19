using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Connected
{
    class clsDataAccess
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString());
        public DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(); 
        public int Read()
        {
            try
            {
                int RetVal = 0;

                SqlCommand cmRead = new SqlCommand()
                {
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ReadKlijenata"
                };

                cmRead.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, true, 0, 0, "", DataRowVersion.Default, null));

                if (cn.State != ConnectionState.Open) cn.Open();
                cmRead.ExecuteNonQuery();
                cn.Close();

                RetVal = (int)cmRead.Parameters["@RETURN_VALUE"].Value;

                da.SelectCommand = cmRead;
                da.Fill(ds);

                return RetVal;
            }
            catch
            {
                throw new Exception("GRESKA!");
            }
            
        }

        public int Insert(string Naziv, string Kontakt, string Grad, string Zemlja)
        {
            try
            {
                int RetVal = 0;
                SqlCommand cmInsert = new SqlCommand();
                cmInsert.Connection = cn;
                cmInsert.CommandType = CommandType.StoredProcedure;
                cmInsert.CommandText = "InsertKlijenata";

                cmInsert.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, true, 0, 0, "", DataRowVersion.Default, null));

                cmInsert.Parameters.AddWithValue("@Naziv", Naziv);
                cmInsert.Parameters.AddWithValue("@Kontakt", Kontakt);
                cmInsert.Parameters.AddWithValue("@Grad", Grad);
                cmInsert.Parameters.AddWithValue("@Zemlja", Zemlja);


                if (cn.State != ConnectionState.Open) { cn.Open(); }
                cmInsert.ExecuteNonQuery();
                cn.Close();
                RetVal = (int)cmInsert.Parameters["@RETURN_VALUE"].Value;

                return RetVal;
            }
            catch
            {
                throw new Exception("Doslo je do greske!");
            }
            

        }

        public int Update(int KlijentID,string Naziv, string Kontakt, string Grad, string Zemlja)
        {
            try
            {
                int RetVal = 0;
                SqlCommand cmUpdate = new SqlCommand();
                cmUpdate.Connection = cn;
                cmUpdate.CommandType = CommandType.StoredProcedure;
                cmUpdate.CommandText = "UpdateKlijenata";

                cmUpdate.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, true, 0, 0, "", DataRowVersion.Default, null));

                cmUpdate.Parameters.AddWithValue("@KlijentID", KlijentID);
                cmUpdate.Parameters.AddWithValue("@Naziv", Naziv);
                cmUpdate.Parameters.AddWithValue("@Kontakt", Kontakt);
                cmUpdate.Parameters.AddWithValue("@Grad", Grad);
                cmUpdate.Parameters.AddWithValue("@Zemlja", Zemlja);


                if (cn.State != ConnectionState.Open) { cn.Open(); }
                cmUpdate.ExecuteNonQuery();
                cn.Close();
                RetVal = (int)cmUpdate.Parameters["@RETURN_VALUE"].Value;

                return RetVal;
            }
            catch
            {
                throw new Exception("Doslo je do greske!");
            }
            
        }

        public int Delete(int KlijentID)
        {
            try
            {
                int RetVal = 0;
                SqlCommand cmDelete = new SqlCommand();
                cmDelete.Connection = cn;
                cmDelete.CommandType = CommandType.StoredProcedure;
                cmDelete.CommandText = "DeleteKlijenata";

                cmDelete.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, true, 0, 0, "", DataRowVersion.Default, null));

                cmDelete.Parameters.AddWithValue("@KlijentID", KlijentID);


                if (cn.State != ConnectionState.Open) { cn.Open(); }
                cmDelete.ExecuteNonQuery();
                cn.Close();
                RetVal = (int)cmDelete.Parameters["@RETURN_VALUE"].Value;

                return RetVal;
            }
            catch
            {
                throw new Exception("Doslo je do greske!");
            }
            
        }
    }
}
