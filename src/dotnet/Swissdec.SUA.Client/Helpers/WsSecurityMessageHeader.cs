using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Swissdec.SUA.Client.Helpers
{
    public sealed class WsSecurityMessageHeader(DateTimeOffset created, DateTimeOffset expires) : MessageHeader
    {
        readonly DateTimeOffset created = created;
        readonly DateTimeOffset expires = expires;

        public const string SoapEnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string WsSecurityExtensionNamespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
        public const string WsSecurityUtilityNamespaceUrl = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

        public override bool MustUnderstand => true;

        public override string Name => "Security";

        public override string Namespace => WsSecurityExtensionNamespace;

        protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteStartElement("wsse", Name, Namespace);

            writer.WriteAttributeString("s", "mustUnderstand", SoapEnvelopeNamespace, "1");

            writer.WriteXmlnsAttribute("wsse", Namespace);
            writer.WriteXmlnsAttribute("wsu", WsSecurityUtilityNamespaceUrl);
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {


        }

        static void WriteTimestampElement(XmlDictionaryWriter writer, DateTimeOffset created, DateTimeOffset expires)
        {
            writer.WriteStartElement("wsu", "Timestamp", WsSecurityUtilityNamespaceUrl);

            writer.WriteAttributeString("wsu", "Id", WsSecurityUtilityNamespaceUrl, "ws-security-timestamp");

            WriteTimestampValue(writer, "Created", created);
            WriteTimestampValue(writer, "Expires", expires);

            writer.WriteEndElement();
        }

        static void WriteTimestampValue(XmlDictionaryWriter writer, string localName, DateTimeOffset timeStamp)
        {
            writer.WriteStartElement("wsu", localName, WsSecurityUtilityNamespaceUrl);
            writer.WriteValue(timeStamp);
            writer.WriteEndElement();
        }
    }
}
