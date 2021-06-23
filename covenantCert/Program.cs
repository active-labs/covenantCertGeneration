using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace covenantCert
{
    class Program
    {

        public static X509Certificate2 CreateSelfSignedCertificate(IPAddress address, string DistinguishedName = "")
        {
            if (DistinguishedName == "") { DistinguishedName = "CN=" + address; }
            using (RSA rsa = RSA.Create(2048))
            {
                var request = new CertificateRequest(new X500DistinguishedName(DistinguishedName), rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, false));
                request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, false));
                SubjectAlternativeNameBuilder subjectAlternativeName = new SubjectAlternativeNameBuilder();
                subjectAlternativeName.AddIpAddress(address);

                request.CertificateExtensions.Add(subjectAlternativeName.Build());
                return request.CreateSelfSigned(new DateTimeOffset(DateTime.UtcNow.AddDays(-1)), new DateTimeOffset(DateTime.UtcNow.AddDays(3650)));
            }
        }
        static void Main(string[] args)
        {
            // Vars
            var CovenantBindUrl = "0.0.0.0";
            var CovenantPort = 7443;
            var CovenantPrivateCertFile = "covenant-dev-private.pfx";
            var CovenantPublicCertFile = "covenant-dev-public.cer";

            IPAddress address = null;
            address = IPAddress.Parse(CovenantBindUrl);
            IPEndPoint CovenantEndpoint = new IPEndPoint(address, CovenantPort);

            Console.WriteLine("Creating cert...");
            X509Certificate2 certificate = CreateSelfSignedCertificate(CovenantEndpoint.Address, "/C=Northern Hemisphere/ST=NorthPole/L=Santas Workshop/O=Toy Makers LTD/OU=Org/CN=127.0.0.1");
            File.WriteAllBytes(CovenantPrivateCertFile, certificate.Export(X509ContentType.Pfx));
            File.WriteAllBytes(CovenantPublicCertFile, certificate.Export(X509ContentType.Cert));
        }
    }
}
