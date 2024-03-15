using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Protocols.WsTrust;

namespace Swissdec.SUA.Client.Helpers
{
    public class CryptographyBehavior(X509Certificate2? certificate) : IClientMessageInspector, IEndpointBehavior
    {
        static readonly Transform defaultTransform = new XmlDsigExcC14NTransform();

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void AfterReceiveReply(ref Message reply, object correlationState) { }
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }
        public void Validate(ServiceEndpoint endpoint) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(this);
        }

        public object? BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var created = DateTimeOffset.Now;
            var expires = created.AddHours(2);

            //var messageBuffer = request.CreateBufferedCopy(int.MaxValue);
            var wsSecurityMessageHeader = new WsSecurityMessageHeader(created, expires);


            var xmlHeader = """
                <wsse:Security s:mustUnderstand="1" xmlns:wsse="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd" xmlns:wsu="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd" xmlns:s="http://schemas.xmlsoap.org/soap/envelope/">
                  <wsu:Timestamp wsu:Id="TSC75045A5-3677-4260-8FE1-86387E54C028">
                    <wsu:Created>2024-03-14T21:46:00.5825956+01:00</wsu:Created>
                    <wsu:Expires>2024-03-14T23:46:00.5825956+01:00</wsu:Expires>
                  </wsu:Timestamp>
                </wsse:Security>
                """;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlHeader);

            var signedXml = new SignedXml(xmlDocument)
            {
                SigningKey = certificate?.GetRSAPrivateKey()
            };

            AddSignatureReference(signedXml, "#C75045A5-3677-4260-8FE1-86387E54C028");

            signedXml.ComputeSignature();

            var xml = signedXml.GetXml();

            request.Headers.Clear();
            request.Headers.Add(wsSecurityMessageHeader);

            return null;
        }

        void AddSignatureReference(SignedXml signedXml, string uri)
        {
            var reference = new Reference(uri);
            reference.AddTransform(defaultTransform);
            signedXml.AddReference(reference);
        }

        private Message CreateMessageFromXmlDocument(Message message, XmlDocument doc)
        {
            MemoryStream ms = new MemoryStream();
            using (XmlWriter xmlWriter = XmlWriter.Create(ms, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false }))
            {
                doc.WriteTo(xmlWriter);
                xmlWriter.Flush();
                xmlWriter.Close();
                ms.Position = 0;
            }
            XmlDictionaryReader xdr = XmlDictionaryReader.CreateTextReader(ms, new XmlDictionaryReaderQuotas());

            var newMessage = Message.CreateMessage(xdr, int.MaxValue, message.Version);

            newMessage.Properties.CopyProperties(message.Properties);

            return newMessage;
        }

    }
}
