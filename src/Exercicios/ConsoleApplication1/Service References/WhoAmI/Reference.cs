﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApplication1.WhoAmI {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WhoAmI.IWhoAmI")]
    public interface IWhoAmI {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWhoAmI/Get", ReplyAction="http://tempuri.org/IWhoAmI/GetResponse")]
        string Get();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWhoAmIChannel : ConsoleApplication1.WhoAmI.IWhoAmI, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WhoAmIClient : System.ServiceModel.ClientBase<ConsoleApplication1.WhoAmI.IWhoAmI>, ConsoleApplication1.WhoAmI.IWhoAmI {
        
        public WhoAmIClient() {
        }
        
        public WhoAmIClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WhoAmIClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WhoAmIClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WhoAmIClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Get() {
            return base.Channel.Get();
        }
    }
}
