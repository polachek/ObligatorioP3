﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppWeb.Cliente_ListaProv {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DtoProveedor", Namespace="http://schemas.datacontract.org/2004/07/WcfListaProv")]
    [System.SerializableAttribute()]
    public partial class DtoProveedor : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FechaRegistroField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreFantasiaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RUTField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TelefonoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TipoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool esInactivoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FechaRegistro {
            get {
                return this.FechaRegistroField;
            }
            set {
                if ((object.ReferenceEquals(this.FechaRegistroField, value) != true)) {
                    this.FechaRegistroField = value;
                    this.RaisePropertyChanged("FechaRegistro");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NombreFantasia {
            get {
                return this.NombreFantasiaField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreFantasiaField, value) != true)) {
                    this.NombreFantasiaField = value;
                    this.RaisePropertyChanged("NombreFantasia");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RUT {
            get {
                return this.RUTField;
            }
            set {
                if ((object.ReferenceEquals(this.RUTField, value) != true)) {
                    this.RUTField = value;
                    this.RaisePropertyChanged("RUT");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Telefono {
            get {
                return this.TelefonoField;
            }
            set {
                if ((object.ReferenceEquals(this.TelefonoField, value) != true)) {
                    this.TelefonoField = value;
                    this.RaisePropertyChanged("Telefono");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Tipo {
            get {
                return this.TipoField;
            }
            set {
                if ((object.ReferenceEquals(this.TipoField, value) != true)) {
                    this.TipoField = value;
                    this.RaisePropertyChanged("Tipo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool esInactivo {
            get {
                return this.esInactivoField;
            }
            set {
                if ((this.esInactivoField.Equals(value) != true)) {
                    this.esInactivoField = value;
                    this.RaisePropertyChanged("esInactivo");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Cliente_ListaProv.IServicioListaProv")]
    public interface IServicioListaProv {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioListaProv/ObtenerProveedores", ReplyAction="http://tempuri.org/IServicioListaProv/ObtenerProveedoresResponse")]
        AppWeb.Cliente_ListaProv.DtoProveedor[] ObtenerProveedores();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioListaProv/ObtenerProveedores", ReplyAction="http://tempuri.org/IServicioListaProv/ObtenerProveedoresResponse")]
        System.Threading.Tasks.Task<AppWeb.Cliente_ListaProv.DtoProveedor[]> ObtenerProveedoresAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicioListaProvChannel : AppWeb.Cliente_ListaProv.IServicioListaProv, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicioListaProvClient : System.ServiceModel.ClientBase<AppWeb.Cliente_ListaProv.IServicioListaProv>, AppWeb.Cliente_ListaProv.IServicioListaProv {
        
        public ServicioListaProvClient() {
        }
        
        public ServicioListaProvClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServicioListaProvClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioListaProvClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioListaProvClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AppWeb.Cliente_ListaProv.DtoProveedor[] ObtenerProveedores() {
            return base.Channel.ObtenerProveedores();
        }
        
        public System.Threading.Tasks.Task<AppWeb.Cliente_ListaProv.DtoProveedor[]> ObtenerProveedoresAsync() {
            return base.Channel.ObtenerProveedoresAsync();
        }
    }
}
