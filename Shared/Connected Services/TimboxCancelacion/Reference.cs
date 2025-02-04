﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimboxCancelacion
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:WashOut", ConfigurationName="TimboxCancelacion.cancelacion_port")]
    public interface cancelacion_port
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="cancelar_cfdi", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="cancelar_cfdi_result")]
        System.Threading.Tasks.Task<TimboxCancelacion.cancelar_cfdi_result> cancelar_cfdiAsync(string username, string password, string rfc_emisor, TimboxCancelacion.folios folios, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="cancelar_cfdi_externo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="cancelar_cfdi_externo_result")]
        System.Threading.Tasks.Task<TimboxCancelacion.cancelar_cfdi_externo_result> cancelar_cfdi_externoAsync(string username, string password, string rfc_emisor, TimboxCancelacion.folios_externo folios, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="cancelar_cfdi_seguro", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="cancelar_cfdi_seguro_result")]
        System.Threading.Tasks.Task<TimboxCancelacion.cancelar_cfdi_seguro_result> cancelar_cfdi_seguroAsync(string username, string password, string firma_base64);
        
        [System.ServiceModel.OperationContractAttribute(Action="consultar_estatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="consultar_estatus_result")]
        System.Threading.Tasks.Task<TimboxCancelacion.consultar_estatus_result> consultar_estatusAsync(string username, string password, string uuid, string rfc_emisor, string rfc_receptor, string total);
        
        [System.ServiceModel.OperationContractAttribute(Action="consultar_peticiones_pendientes", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="consultar_peticiones_pendientes_result")]
        System.Threading.Tasks.Task<TimboxCancelacion.consultar_peticiones_pendientes_result> consultar_peticiones_pendientesAsync(string username, string password, string rfc_receptor, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="procesar_respuesta", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="procesar_respuesta_result")]
        System.Threading.Tasks.Task<TimboxCancelacion.procesar_respuesta_result> procesar_respuestaAsync(string username, string password, string rfc_receptor, TimboxCancelacion.respuestas respuestas, string cert_pem, string llave_pem);
        
        [System.ServiceModel.OperationContractAttribute(Action="consultar_documento_relacionado", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="consultar_documento_relacionado_result")]
        System.Threading.Tasks.Task<TimboxCancelacion.consultar_documento_relacionado_result> consultar_documento_relacionadoAsync(string username, string password, string uuid, string rfc_emisor, string rfc_receptor, string cert_pem, string llave_pem);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class folios
    {
        
        private folio[] folioField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public folio[] folio
        {
            get
            {
                return this.folioField;
            }
            set
            {
                this.folioField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class folio
    {
        
        private string uuidField;
        
        private string rfc_receptorField;
        
        private string totalField;
        
        private string motivoField;
        
        private string folio_sustitutoField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string uuid
        {
            get
            {
                return this.uuidField;
            }
            set
            {
                this.uuidField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string rfc_receptor
        {
            get
            {
                return this.rfc_receptorField;
            }
            set
            {
                this.rfc_receptorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string motivo
        {
            get
            {
                return this.motivoField;
            }
            set
            {
                this.motivoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string folio_sustituto
        {
            get
            {
                return this.folio_sustitutoField;
            }
            set
            {
                this.folio_sustitutoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class consultar_documento_relacionado_result
    {
        
        private string resultadoField;
        
        private string relacionados_padresField;
        
        private string relacionados_hijosField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string resultado
        {
            get
            {
                return this.resultadoField;
            }
            set
            {
                this.resultadoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string relacionados_padres
        {
            get
            {
                return this.relacionados_padresField;
            }
            set
            {
                this.relacionados_padresField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string relacionados_hijos
        {
            get
            {
                return this.relacionados_hijosField;
            }
            set
            {
                this.relacionados_hijosField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class procesar_respuesta_result
    {
        
        private string foliosField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string folios
        {
            get
            {
                return this.foliosField;
            }
            set
            {
                this.foliosField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class folios_respuestas
    {
        
        private string uuidField;
        
        private string rfc_emisorField;
        
        private string totalField;
        
        private string respuestaField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string uuid
        {
            get
            {
                return this.uuidField;
            }
            set
            {
                this.uuidField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string rfc_emisor
        {
            get
            {
                return this.rfc_emisorField;
            }
            set
            {
                this.rfc_emisorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string respuesta
        {
            get
            {
                return this.respuestaField;
            }
            set
            {
                this.respuestaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class respuestas
    {
        
        private folios_respuestas[] folios_respuestasField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public folios_respuestas[] folios_respuestas
        {
            get
            {
                return this.folios_respuestasField;
            }
            set
            {
                this.folios_respuestasField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class consultar_peticiones_pendientes_result
    {
        
        private string codestatusField;
        
        private string uuidsField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string codestatus
        {
            get
            {
                return this.codestatusField;
            }
            set
            {
                this.codestatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string uuids
        {
            get
            {
                return this.uuidsField;
            }
            set
            {
                this.uuidsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class consultar_estatus_result
    {
        
        private string codigo_estatusField;
        
        private string es_cancelableField;
        
        private string estadoField;
        
        private string estatus_cancelacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string codigo_estatus
        {
            get
            {
                return this.codigo_estatusField;
            }
            set
            {
                this.codigo_estatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string es_cancelable
        {
            get
            {
                return this.es_cancelableField;
            }
            set
            {
                this.es_cancelableField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string estado
        {
            get
            {
                return this.estadoField;
            }
            set
            {
                this.estadoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string estatus_cancelacion
        {
            get
            {
                return this.estatus_cancelacionField;
            }
            set
            {
                this.estatus_cancelacionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class cancelar_cfdi_seguro_result
    {
        
        private string folios_cancelacionField;
        
        private string acuse_cancelacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string folios_cancelacion
        {
            get
            {
                return this.folios_cancelacionField;
            }
            set
            {
                this.folios_cancelacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string acuse_cancelacion
        {
            get
            {
                return this.acuse_cancelacionField;
            }
            set
            {
                this.acuse_cancelacionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class cancelar_cfdi_externo_result
    {
        
        private string folios_cancelacionField;
        
        private string acuse_cancelacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string folios_cancelacion
        {
            get
            {
                return this.folios_cancelacionField;
            }
            set
            {
                this.folios_cancelacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string acuse_cancelacion
        {
            get
            {
                return this.acuse_cancelacionField;
            }
            set
            {
                this.acuse_cancelacionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class folio_externo
    {
        
        private string uuidField;
        
        private string rfc_receptorField;
        
        private string totalField;
        
        private string motivoField;
        
        private string folio_sustitutoField;
        
        private string fecha_timbradoField;
        
        private string tipo_comprobanteField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string uuid
        {
            get
            {
                return this.uuidField;
            }
            set
            {
                this.uuidField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string rfc_receptor
        {
            get
            {
                return this.rfc_receptorField;
            }
            set
            {
                this.rfc_receptorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string motivo
        {
            get
            {
                return this.motivoField;
            }
            set
            {
                this.motivoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string folio_sustituto
        {
            get
            {
                return this.folio_sustitutoField;
            }
            set
            {
                this.folio_sustitutoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string fecha_timbrado
        {
            get
            {
                return this.fecha_timbradoField;
            }
            set
            {
                this.fecha_timbradoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string tipo_comprobante
        {
            get
            {
                return this.tipo_comprobanteField;
            }
            set
            {
                this.tipo_comprobanteField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class folios_externo
    {
        
        private folio_externo[] folio_externoField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public folio_externo[] folio_externo
        {
            get
            {
                return this.folio_externoField;
            }
            set
            {
                this.folio_externoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WashOut")]
    public partial class cancelar_cfdi_result
    {
        
        private string folios_cancelacionField;
        
        private string acuse_cancelacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string folios_cancelacion
        {
            get
            {
                return this.folios_cancelacionField;
            }
            set
            {
                this.folios_cancelacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string acuse_cancelacion
        {
            get
            {
                return this.acuse_cancelacionField;
            }
            set
            {
                this.acuse_cancelacionField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public interface cancelacion_portChannel : TimboxCancelacion.cancelacion_port, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public partial class cancelacion_portClient : System.ServiceModel.ClientBase<TimboxCancelacion.cancelacion_port>, TimboxCancelacion.cancelacion_port
    {
        
        /// <summary>
        /// Implemente este método parcial para configurar el punto de conexión de servicio.
        /// </summary>
        /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
        /// <param name="clientCredentials">Credenciales de cliente</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public cancelacion_portClient() : 
                base(cancelacion_portClient.GetDefaultBinding(), cancelacion_portClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.cancelacion_port.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public cancelacion_portClient(EndpointConfiguration endpointConfiguration) : 
                base(cancelacion_portClient.GetBindingForEndpoint(endpointConfiguration), cancelacion_portClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public cancelacion_portClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(cancelacion_portClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public cancelacion_portClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(cancelacion_portClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public cancelacion_portClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<TimboxCancelacion.cancelar_cfdi_result> cancelar_cfdiAsync(string username, string password, string rfc_emisor, TimboxCancelacion.folios folios, string cert_pem, string llave_pem)
        {
            return base.Channel.cancelar_cfdiAsync(username, password, rfc_emisor, folios, cert_pem, llave_pem);
        }
        
        public System.Threading.Tasks.Task<TimboxCancelacion.cancelar_cfdi_externo_result> cancelar_cfdi_externoAsync(string username, string password, string rfc_emisor, TimboxCancelacion.folios_externo folios, string cert_pem, string llave_pem)
        {
            return base.Channel.cancelar_cfdi_externoAsync(username, password, rfc_emisor, folios, cert_pem, llave_pem);
        }
        
        public System.Threading.Tasks.Task<TimboxCancelacion.cancelar_cfdi_seguro_result> cancelar_cfdi_seguroAsync(string username, string password, string firma_base64)
        {
            return base.Channel.cancelar_cfdi_seguroAsync(username, password, firma_base64);
        }
        
        public System.Threading.Tasks.Task<TimboxCancelacion.consultar_estatus_result> consultar_estatusAsync(string username, string password, string uuid, string rfc_emisor, string rfc_receptor, string total)
        {
            return base.Channel.consultar_estatusAsync(username, password, uuid, rfc_emisor, rfc_receptor, total);
        }
        
        public System.Threading.Tasks.Task<TimboxCancelacion.consultar_peticiones_pendientes_result> consultar_peticiones_pendientesAsync(string username, string password, string rfc_receptor, string cert_pem, string llave_pem)
        {
            return base.Channel.consultar_peticiones_pendientesAsync(username, password, rfc_receptor, cert_pem, llave_pem);
        }
        
        public System.Threading.Tasks.Task<TimboxCancelacion.procesar_respuesta_result> procesar_respuestaAsync(string username, string password, string rfc_receptor, TimboxCancelacion.respuestas respuestas, string cert_pem, string llave_pem)
        {
            return base.Channel.procesar_respuestaAsync(username, password, rfc_receptor, respuestas, cert_pem, llave_pem);
        }
        
        public System.Threading.Tasks.Task<TimboxCancelacion.consultar_documento_relacionado_result> consultar_documento_relacionadoAsync(string username, string password, string uuid, string rfc_emisor, string rfc_receptor, string cert_pem, string llave_pem)
        {
            return base.Channel.consultar_documento_relacionadoAsync(username, password, uuid, rfc_emisor, rfc_receptor, cert_pem, llave_pem);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.cancelacion_port))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.cancelacion_port))
            {
                return new System.ServiceModel.EndpointAddress("https://sistema.timbox.com.mx/cancelacion/action");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return cancelacion_portClient.GetBindingForEndpoint(EndpointConfiguration.cancelacion_port);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return cancelacion_portClient.GetEndpointAddress(EndpointConfiguration.cancelacion_port);
        }
        
        public enum EndpointConfiguration
        {
            
            cancelacion_port,
        }
    }
}
