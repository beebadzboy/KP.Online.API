﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KP.Online.API.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.3.26.32:5000")]
        public string KP_Return_KPC {
            get {
                return ((string)(this["KP_Return_KPC"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("A64803F0A7CEDAC8407538D341BDBE23")]
        public string KP_Return_KPC_Token {
            get {
                return ((string)(this["KP_Return_KPC_Token"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://kpd-api1.kingpower.com/OrderWCF/OtherService.svc")]
        public string KP_Online_API_WebService_OtherService {
            get {
                return ((string)(this["KP_Online_API_WebService_OtherService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://kpd-api1.kingpower.com/OrderWCF/SaleOrderService.svc")]
        public string KP_Online_API_WebService_SaleOrderService {
            get {
                return ((string)(this["KP_Online_API_WebService_SaleOrderService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.1.0.180/OrderWCF/OtherService.svc")]
        public string KP_Online_API_Other_WebService_OtherService {
            get {
                return ((string)(this["KP_Online_API_Other_WebService_OtherService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.1.0.180/OrderWCF/SaleOrderService.svc")]
        public string KP_Online_API_Order_WebService_SaleOrderService {
            get {
                return ((string)(this["KP_Online_API_Order_WebService_SaleOrderService"]));
            }
        }
    }
}
