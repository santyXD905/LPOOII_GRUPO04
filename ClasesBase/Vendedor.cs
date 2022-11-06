using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Vendedor : IDataErrorInfo
    {
        public Vendedor(string leg, string ape, string nom)
        {
            // TODO: Complete member initialization
            this.legajo = leg;
            this.apellido = ape;
            this.nombre = nom;
        }

        private string legajo;

        public string Legajo
        {
            get { return legajo; }
            set { legajo = value; }
        }
        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
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
                    case "Legajo":
                        msg_error = verificarLegajo();
                        break;
                    case "Nombre":
                        msg_error = verificarNombre();
                        break;
                    case "Apellido":
                        msg_error = verificarApellido();
                        break;
                }
                return msg_error;
            }
        }
        private string verificarLegajo()
        {
            if (String.IsNullOrEmpty(Legajo))
            {
                return "El legajo es obligatorio";
            }
            return null;
        }
        private string verificarNombre()
        {
            if (String.IsNullOrEmpty(Nombre))
            {
                return "El nombre es obligatorio";
            }
            return null;
        }
        private string verificarApellido()
        {
            if (String.IsNullOrEmpty(Apellido))
            {
                return "El apellido es obligatorio";
            }
            return null;
        }

        public Vendedor() { }
    }
}
