using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
namespace ClasesBase
{
    public class Venta: IDataErrorInfo
    {
        private int nroFactura;

        public int NroFactura
        {
            get { return nroFactura; }
            set { nroFactura = value; }
        }
        private DateTime fechaFactura;

        public DateTime FechaFactura
        {
            get { return fechaFactura; }
            set { fechaFactura = value; }
        }
        private string legajo;

        public string Legajo
        {
            get { return legajo; }
            set { legajo = value; }
        }
        private string dni;

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        private string codProducto;

        public string CodProducto
        {
            get { return codProducto; }
            set { codProducto = value; }
        }
        private decimal precio;

        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private decimal importe;

        public decimal Importe
        {
            get { return importe; }
            set { importe = value; }
        }

        private string estado;

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }


        public Venta() { }

        public Venta(int nroFactura, DateTime fecha, string legajo, string dni, string cod, decimal precio, int cantidad, decimal importe, string estado)
        {
            this.nroFactura = nroFactura;
            this.fechaFactura = fecha;
            this.legajo = legajo;
            this.dni = dni;
            this.codProducto = cod;
            this.precio = precio;
            this.cantidad = cantidad;
            this.importe = importe;
            this.estado = estado;
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string msg_error = null;
                switch (columnName)
                {
                    case "FechaFactura":
                        msg_error = verificarFechaFactura();
                        break;
                    case "Dni":
                        msg_error = verificarDNI();
                        break;
                    case "CodProducto":
                        msg_error = verificarCodProducto();
                        break;
                    case "Cantidad":
                        msg_error = verificarCantidad();
                        break;
                    case "Legajo":
                        msg_error = verificarLegajo();
                        break;
                }
                return msg_error;
            }
        }
        private string verificarFechaFactura()
        {
            if (fechaFactura.Equals(DateTime.MinValue))
            {
                return "La fecha es obligatoria";
            }
            return null;
        }
        private string verificarDNI()
        {
            if (String.IsNullOrEmpty(dni) || dni == "No seleccionado")
            {
                return "El cliente es obligatorio";
            }
            return null;
        }
        private string verificarCodProducto()
        {
            if (String.IsNullOrEmpty(CodProducto) || CodProducto == "No seleccionado")
            {
                return "El producto es obligatorio";
            }
            return null;
        }
        private string verificarCantidad()
        {
            if (Cantidad<=0)
            {
                return "La cantidad debe ser mayor que 0";
            }
            return null;
        }
        private string verificarLegajo()
        {
            if (String.IsNullOrEmpty(Legajo) || Legajo == "No seleccionado")
            {
                return "El vendedor es obligatorio";
            }
            return null;
        }
    }
}
