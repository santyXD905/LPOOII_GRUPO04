using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarProveedor
    {
        //trae todos los proveedores
        public static ObservableCollection<Proveedor> TraerProveedores()
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT * FROM Proveedor";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            //se crea el objeto a retornar
            ObservableCollection<Proveedor> lista = new ObservableCollection<Proveedor>();

            //se insertan los objetos de la consulta adentro del objeto a retonar
            foreach (DataRow x in dt.Rows)
            {
                lista.Add(new Proveedor(x["cuit"].ToString(), x["razonSocial"].ToString(),
                    x["domicilio"].ToString(), x["telefono"].ToString()));
            }

            return lista;
        }


        #region Alta_Baja_Modificacion

        public static void GuardarProveedor(Proveedor Proveedor)
        {

            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.CommandText = "AgregarProveedor";

            SqlParameter param = new SqlParameter("@cuit", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Proveedor.CUIT;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@razon", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Proveedor.RazonSocial;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@domicilio", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Proveedor.Domicilio;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@telefono", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Proveedor.Telefono;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }

        public static void EliminarProveedor(string ciut)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.CommandText = "EliminarProveedor";

            SqlParameter param = new SqlParameter("@cuit", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ciut;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }

        public static void ModificarProveedor(Proveedor Proveedor)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.CommandText = "ModificarProveedor";

            SqlParameter param = new SqlParameter("@cuit", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Proveedor.CUIT;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@razon", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Proveedor.RazonSocial;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@domicilio", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Proveedor.Domicilio;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@telefono", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Proveedor.Telefono;
            cmd.Parameters.Add(param);


            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        #endregion
       


    }

    
}
