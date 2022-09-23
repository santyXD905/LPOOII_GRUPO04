using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarProducto
    {

        //TraerProductos que devuelva el Código, Categoría, Color,Descripción, Precio.
        public DataTable TraerProductos()
        {
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.conection);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText="SELECT * FROM Producto";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return dt;

        }
    }
}
