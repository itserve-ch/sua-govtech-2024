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

## Testsystems

Service endpoint: https://tst.itserve.ch/swissdec/refapps/stable/receiver/services/OrganizationAuthenticationService20190301

Service endpoint renew: https://tst.itserve.ch/swissdec/refapps/stable/receiver/services/OrganizationAuthenticationRenewService20190301