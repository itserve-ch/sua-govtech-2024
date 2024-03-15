Python project for Swissdec App
---

Exploring the service at GovTech Hackathon 2024

https://docs.python-zeep.org/en/master/wsse.html



# Usage

Install using Poetry: `poetry install --no-root`

Run with `python app.py`

# Converting certifiate

```
openssl pkcs12 -in path.p12 -out newfile.crt.pem -clcerts -nokeys
openssl pkcs12 -in path.p12 -out newfile.key.pem -nocerts -nodes
```

Via https://stackoverflow.com/a/15144560
