using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    //implemento la interfaz para realizar la colecciona datos más eficiente
    public class Proveedor:INotifyPropertyChanged
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
    }
}
