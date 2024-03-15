import kivy
kivy.require('2.3.0')

from kivy.app import App
from kivy.uix.label import Label

from zeep import Client, exceptions

from datetime import datetime

date = datetime.now()
useragent = {
        'Producer': 'GovTech Hackathon',
        'Name': 'Swissdec Python App',
        'StandardVersion': '1.0',
        'Version': '0.1',
        'Certificate': 'n/A',
    }

client = Client('https://tst.itserve.ch/swissdec/refapps/stable/receiver/services/OrganizationAuthenticationService20230301?WSDL')

result1 = client.service.Ping(
    UserAgent=useragent,
    MonitoringID='stettler',
    SystemDateTime=date
    )

result2 = client.service.CheckInteroperability(
    UserAgent=useragent,
    MonitoringID='stettler',
    SystemDateTime=date,
    UmlautString="ÄËÖÜÁÉÓÚÀÈÒÙÂÊÔÛ",
    FirstOperand=999000000000.00,
    SecondOperand=10.50,
    )



class MyApp(App):

    def build(self):
        return Label(text=str(result1))


if __name__ == '__main__':
    MyApp().run()
