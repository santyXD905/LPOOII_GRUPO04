using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarProducto
    {

        //TraerProductos que devuelva el Código, Categoría, Color,Descripción, Precio.

        public static ObservableCollection<Producto> TraerProductos()
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT * FROM Producto";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ObservableCollection<Producto> lista = new ObservableCollection<Producto>();

            foreach (DataRow x in dt.Rows)
            {
                lista.Add(new Producto(x["codProducto"].ToString(), x["categoria"].ToString(),
                    x["color"].ToString(), x["descripcion"].ToString(),Convert.ToDecimal(x["precio"].ToString())));
            }

            return lista;
        }

        //Determinar producto existente

        public static bool DeterminarProductoExistente(string codigo) 
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Producto WHERE codProducto LIKE @codigo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@codigo", codigo);

            //ejecutar la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //llenar el data table
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) return false;
            else return true;
        }

        //Agregar nuevo producto

        public static void AgregarProducto(Producto producto)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            // crear query

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.CommandText = "AgregarProducto";
            SqlParameter param = new SqlParameter("@codigo", SqlDbType.VarChar);
            param.Value = producto.CodProducto;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@categoria", SqlDbType.VarChar);
            param.Value = producto.Categoria;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@color", SqlDbType.VarChar);
            param.Value = producto.Color;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@descripcion", SqlDbType.VarChar);
            param.Value = producto.Descripcion;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@precio", SqlDbType.Decimal, 16)
            {
                Precision = 16,
                Scale = 2
            };
            param.Value = producto.Precio;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cnn.Open();//abrir conexion

            cmd.ExecuteNonQuery(); //ejecutar transaccion
            cnn.Close(); // cerrar conexion

        }

        //Modificar producto

        public static void ModificarProducto(Producto producto)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Connection = cnn;

            cmd.CommandText = "ModificarProducto";

            SqlParameter param = new SqlParameter("@codigo", SqlDbType.VarChar);
            param.Value = producto.CodProducto;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@categoria", SqlDbType.VarChar);
            param.Value = producto.Categoria;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@color", SqlDbType.VarChar);
            param.Value = producto.Color;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@descripcion", SqlDbType.VarChar);
            param.Value = producto.Descripcion;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@precio", SqlDbType.Decimal);
            param.Value = producto.Precio;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        //Borrar producto

        public static void EliminarProducto(string codigo)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Connection = cnn;

            cmd.CommandText = "EliminarProducto";
            SqlParameter param = new SqlParameter("@codigo", SqlDbType.VarChar);
            param.Value = codigo;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }

        public static Producto getByCod(string cod)
        {
            // conexion a la base de datos
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            //operaciones
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM Producto WHERE codProducto = " + cod;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Producto v = new Producto(cod,dt.Rows[0]["categoria"].ToString(),dt.Rows[0]["color"].ToString(),dt.Rows[0]["descripcion"].ToString(), Decimal.Parse(dt.Rows[0]["precio"].ToString()));

            return v;

        }
    }
}
