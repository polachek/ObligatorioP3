﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppWeb.Cliente_ModificarParametros {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Cliente_ModificarParametros.IServicioModificarParametros")]
    public interface IServicioModificarParametros {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioModificarParametros/ModificarArancel", ReplyAction="http://tempuri.org/IServicioModificarParametros/ModificarArancelResponse")]
        string ModificarArancel(double value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioModificarParametros/ModificarArancel", ReplyAction="http://tempuri.org/IServicioModificarParametros/ModificarArancelResponse")]
        System.Threading.Tasks.Task<string> ModificarArancelAsync(double value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioModificarParametros/ModificarPorcentajeExtra", ReplyAction="http://tempuri.org/IServicioModificarParametros/ModificarPorcentajeExtraResponse")]
        string ModificarPorcentajeExtra(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioModificarParametros/ModificarPorcentajeExtra", ReplyAction="http://tempuri.org/IServicioModificarParametros/ModificarPorcentajeExtraResponse")]
        System.Threading.Tasks.Task<string> ModificarPorcentajeExtraAsync(int value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicioModificarParametrosChannel : AppWeb.Cliente_ModificarParametros.IServicioModificarParametros, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicioModificarParametrosClient : System.ServiceModel.ClientBase<AppWeb.Cliente_ModificarParametros.IServicioModificarParametros>, AppWeb.Cliente_ModificarParametros.IServicioModificarParametros {
        
        public ServicioModificarParametrosClient() {
        }
        
        public ServicioModificarParametrosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServicioModificarParametrosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioModificarParametrosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioModificarParametrosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ModificarArancel(double value) {
            return base.Channel.ModificarArancel(value);
        }
        
        public System.Threading.Tasks.Task<string> ModificarArancelAsync(double value) {
            return base.Channel.ModificarArancelAsync(value);
        }
        
        public string ModificarPorcentajeExtra(int value) {
            return base.Channel.ModificarPorcentajeExtra(value);
        }
        
        public System.Threading.Tasks.Task<string> ModificarPorcentajeExtraAsync(int value) {
            return base.Channel.ModificarPorcentajeExtraAsync(value);
        }
    }
}
