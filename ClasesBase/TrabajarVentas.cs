using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClasesBase
{
    public class TrabajarVentas
    {
        public static void GuardarVenta(Venta nueva) {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.CommandText = "AgregarVenta";

            #region Parametros
            SqlParameter param = new SqlParameter("@cantidad", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = nueva.Cantidad;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@codProducto", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = nueva.CodProducto;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@dni", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = nueva.Dni;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@fecha", SqlDbType.Date);
            param.Direction = ParameterDirection.Input;
            param.Value = nueva.FechaFactura;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@importe", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = nueva.Importe;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@precio", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = nueva.Precio;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@legajo", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = nueva.Legajo;
            cmd.Parameters.Add(param);
            #endregion 

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
        public static int GetCurrentIndex() {

            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT IDENT_CURRENT('Venta')";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt= new DataTable();
            da.Fill(dt);

            
            return Int32.Parse(dt.Rows[0][0].ToString());
            
        }
    }
}
