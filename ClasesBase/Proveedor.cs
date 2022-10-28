using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    //implemento la interfaz para realizar la colecciona datos más eficiente
    public class Proveedor:INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //constructor
        public Proveedor(string ct, string rs, string dm, string tel)
        {
            // TODO: Complete member initialization
            this.cuit = ct;
            this.razonSocial = rs;
            this.domicilio = dm;
            this.telefono = tel;
        }

        private string cuit;

        public string CUIT
        {
            get { return cuit; }
            set { cuit = value;
            OnPropetyChanged("cuit");
            }
        }
        private string razonSocial;

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value;
            OnPropetyChanged("razonSocial");
            }
        }
        private string domicilio;

        public string Domicilio
        {
            get { return domicilio; }
            set { domicilio = value;
            OnPropetyChanged("domicilio");
            }
        }
        private string telefono;

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value;
            OnPropetyChanged("telefono");
            }
        }


        private void OnPropetyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public override string ToString()
        {
            return CUIT.ToString() + ", " + Telefono.ToString();
        }

        //implementacion de validacion de datos 

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
                    case "CUIT":
                        msg_error = verificarCUIT();
                        break;
                    case "RazonSocial":
                        msg_error = verificarRazonSocial();
                        break;
                    case "Domicilio":
                        msg_error = verificarDomicilio();
                        break;
                    case "Telefono":
                        msg_error = verificarTelefono();
                        break;
                }
                return msg_error;
            }
        }

        //AÑADIR MÁS VALIDACIONES

        private string verificarCUIT()
        {
            if (String.IsNullOrEmpty(CUIT))
            {
                return "El CUIT es obligatorio";
            }
            return null;
        }

        private string verificarRazonSocial()
        {
            if (String.IsNullOrEmpty(RazonSocial))
            {
                return "La Razon Social es obligatoria";
            }
            return null;
        }

        private string verificarDomicilio()
        {
            if (String.IsNullOrEmpty(Domicilio))
            {
                return "El Domicilio es obligatorio";
            }
            return null;
        }

        private string verificarTelefono()
        {
            if (String.IsNullOrEmpty(Telefono))
            {
                return "El Telefono es obligatorio";
            }
            return null;
        }


    }
}
