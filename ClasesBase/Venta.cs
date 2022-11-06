using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Venta
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
        private int dni;

        public int Dni
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

        public Venta(int nroFactura, DateTime fecha, string legajo, int dni, string cod, decimal precio, int cantidad, decimal importe, string estado)
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
    }
}
