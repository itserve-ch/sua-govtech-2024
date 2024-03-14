# Swissdec Challenge from the GovTech Hackathon 2024

Challenge description: https://hack.opendata.ch/project/1099

SUA specification: https://www.swissdec.ch/sua

# Resources

The resources folder contains the following artifacts:

* security: needed certificates 
  * for signing (client.p12, info.txt contains the password) 
  * for encryption (server.crt)
* wsdl: the soap interface description with XML schemas
  * OrganizationAuthenticationService.wsdl: endpoint description for registration process 
  * OrganizationAuthenticationRenewService.wsdl: endpoint description for reissuing the certificates
  * *.xsd: datastructure description in xml

## Dokumente

[Detailspezifikation Unternehmens-Authentifizierung](https://www.swissdec.ch/document/share/158/c0b9144a-1d7f-4964-96ce-1d2c6d7c9d05)


# Testsystems

Service endpoint: https://tst.itserve.ch/swissdec/refapps/stable/receiver/services/OrganizationAuthenticationService20190301

Service endpoint renew: https://tst.itserve.ch/swissdec/refapps/stable/receiver/services/OrganizationAuthenticationRenewService20190301