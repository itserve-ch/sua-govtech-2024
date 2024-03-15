# Swissdec Challenge from the GovTech Hackathon 2024

Challenge description: https://hack.opendata.ch/project/1099/challenge

Challenge outcome: https://hack.opendata.ch/project/1099

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

Service endpoint: https://tst.itserve.ch/swissdec/refapps/stable/receiver/services/OrganizationAuthenticationService20230301

Service endpoint renew: https://tst.itserve.ch/swissdec/refapps/stable/receiver/services/OrganizationAuthenticationRenewService20230301

---
# The Challenge: Feedback Tag 1

Interesse der Mentoren und Organisatoren, aber schwierig, Teilnehmer für das "unsexy" Thema der grundlegenden Sozialversicherungsleistungen zu rekrutieren. Jeder möchte (offene) Daten erhalten - nur wenige wissen, wie man effektiv (sensible) Daten sendet, insbesondere über einen langen Zeitraum (vertraglicher/rechtlicher Rahmen).

- Besseres Verständnis dafür, wie man bei einem Hackathon pitcht
- Bessere Außenperspektive auf die Situation gewinnen
- Erfahren Sie mehr über technologische Entwicklungen und "Life Hacks".
  ![1710446987654](https://hackmd.io/_uploads/rkii9o-Rp.jpg)

---
## The Check-in: Projekt am Tag 2

* Eine alte App-Client-Version gefunden, die nicht mehr brauchbar, aber hilfreich ist
* Wir haben viel am Code herumgeschraubt, um einen neuen .NET-Client zum Laufen zu bringen
* SOAP 1.2-Update, um zu sehen, ob wir die Zugriffsanforderungen vereinfachen können
* Einen einfachen Python Client steht ebenfalls zur Verüfgung die der Anmeldung zeigt
* Wir empfehlen auch ein paar weitere Tools (Postman, Workspaces) zu vorkonfigurieren
* Hinterfragen des Workflow-Designs bei der Übertragung einer großen Menge an 1 Tag


![IMG_20240314_115035](https://hackmd.io/_uploads/B1GwcoWAp.jpg)

# Story

AHV: "Zentrale Kontrolle, dezentrale [Durchführung](https://www.ahv-iv.ch/de/Sozialversicherungen/Alters-und-Hinterlassenenversicherung-AHV/Allgemeines#qa-734)"

Swiss Government PKI: "Kernstück der [Bundes-Trust-Services](https://www.bit.admin.ch/bit/en/home/themes/swiss-government-pki/die-sg-pki.html)"


# Demo

- [x] Samples security for .NET community - issue with WCF deprecation
- [x] Oxygen https://www.oxygenxml.com/
- [x] Python / Zeep https://docs.python-zeep.org/ + https://pypi.org/search/?q=pkcs
- [ ] Set up [Codespace](https://github.com/features/codespaces) or [Gitpod](https://www.gitpod.io/pricing)
- [ ] Postman https://blog.postman.com/postman-now-supports-wsdl/
- [ ] Python client app using Kivy https://kivy.org/doc/

# Modern data sharing design

All systems are green today at [itServe](https://tst.itserve.ch/swissdec/) - how does the roadmap look tomorrow?

The current solution is based on SOAP / WSDL. We discussed at the hackathon what developers are using today for the problem of secure and reliable transfers.

Streaming protocols (MQTT) have low packet sizes issue, so do not work well in the business context. As an alternative we discussed [gRPC - MSDN](https://learn.microsoft.com/de-de/aspnet/core/grpc/protobuf?view=aspnetcore-8.0) based on [Protobuf](https://protobuf.dev/), or a different stack with [Apache Avro](https://avro.apache.org/docs/1.11.1/).

See also [Aviary](https://apiary.io/how-apiary-works), [StackOverflow](https://stackoverflow.com/a/4633833), [Aklivity](https://www.aklivity.io/post/streaming-apis-and-protocols-sse-websocket-mqtt-amqp-grpc) if you are interested in this topic. There is a comparison of [Data Serialization Formats on Wikipedia](https://en.wikipedia.org/wiki/Comparison_of_data-serialization_formats).

Let's Encrypt [Chain of Trust](https://letsencrypt.org/certificates/) as inspiration for the certificate issuing and distribution.

# Modern open source ERP

Because not everyone at a hackathon is familiar with ERP (that's [Enterprise-Resource-Planning](https://de.wikipedia.org/wiki/Enterprise-Resource-Planning) for you), we quickly installed an instance of [Odoo community edition](https://odoo-community.org/) ERP system from the official [Docker image](https://hub.docker.com/_/odoo).

It is interesting as a case study, because you can see how the standard [Payroll module](https://www.odoo.com/documentation/master/applications/hr/payroll.html) ([Source](https://github.com/OCA/payroll)) does not match Swiss legal requirements. Several vendors have provided implementations: [Giordano](https://www.giordano.ch/odoo/l%C3%B6sungen/lohnprogramm-ch-d) ([Wiki](https://www.odoo-wiki.org/gio-payroll-custom.html)), [Braintec](https://braintec.com/en/swissdec-payroll-module), [Open Net](https://apps.odoo.com/apps/modules/11.0/l10n_ch_hr_payroll/).

See a [conference talk](https://www.odoo.com/event/odoo-experience-2021-2847/track/odoo-payroll-compliance-the-case-of-switzerland-4261) and think about some [predictive features](https://www.odoo.com/documentation/master/applications/sales/crm/track_leads/lead_scoring.html?highlight=predictive) that could generate interest in going down this route.

