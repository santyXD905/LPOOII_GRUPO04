using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarVentas
    {
        public static Venta TraerVenta()
        {
            Venta v = new Venta();
            return v;
        }
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

            param = new SqlParameter("@estado", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = nueva.Estado;
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

        public static ObservableCollection<Venta> TraerVentas()
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT * FROM Venta";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ObservableCollection<Venta> lista = new ObservableCollection<Venta>();

            foreach (DataRow x in dt.Rows)
            {
                lista.Add(new Venta(
                    Int32.Parse(x["nroFactura"].ToString()),
                    (DateTime)x["fechaFactura"],
                    x["legajo"].ToString(),
                    Int32.Parse(x["dni"].ToString()),
                    x["codProducto"].ToString(),
                    Decimal.Parse(x["precio"].ToString()),
                    Int32.Parse(x["cantidad"].ToString()),
                    Decimal.Parse(x["importe"].ToString()),
                    x["estado"].ToString()
               ));
            }

            return lista;
        }


        public static void ModificarVenta(Venta venta)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.CommandText = "ModificarVenta";


            #region Parametros
            SqlParameter param = new SqlParameter("@nro", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = venta.Cantidad;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@cantidad", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = venta.Cantidad;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@codprod", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = venta.CodProducto;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@dni", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = venta.Dni;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@fecha", SqlDbType.Date);
            param.Direction = ParameterDirection.Input;
            param.Value = venta.FechaFactura;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@total", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = venta.Importe;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@precio", SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = venta.Precio;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@legajo", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = venta.Legajo;
            cmd.Parameters.Add(param);
            #endregion 

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


        public static void EliminarVenta(int nro)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.CommandText = "EliminarVenta";

            #region Parametros
            SqlParameter param = new SqlParameter("@nro", SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = nro;
            cmd.Parameters.Add(param);
            #endregion

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static bool BuscarDni(int dni)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM Venta WHERE dni = ";
            cmd.CommandText += dni.ToString();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) return false;
            else return true;

        }

        public static bool BuscarLegajo(string legajo)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM Venta WHERE legajo LIKE '" + legajo + "'";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) return false;
            else return true;

        }

        public static bool BuscarCodProducto(string cod)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM Venta WHERE codProducto LIKE '" + cod + "'";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) return false;
            else return true;

        }
    }
}
