using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

static X509Certificate2 CreateSelfSignedCertificate(string subjectName)
{
    using var rsaKey = RSA.Create(2048);
    var certificateRequest = new CertificateRequest(subjectName, rsaKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    var notBefore = DateTimeOffset.UtcNow;
    var notAfter = notBefore.AddMonths(1);
    return certificateRequest.CreateSelfSigned(notBefore, notAfter);
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapPost("/certificate/request", async (CertificateRequestModel certificateRequestModel, HttpContext httpContext) =>
{
    var certificate = CreateSelfSignedCertificate($"cn={certificateRequestModel.CommonName}");
    var pfxBytes = certificate.Export(X509ContentType.Pfx, certificateRequestModel.PfxPassword);
    await httpContext.Response.Body.WriteAsync(pfxBytes);
})
.WithName("RequestCertificate").WithOpenApi();

app.Run();

record CertificateRequestModel(string CommonName, string? PfxPassword);
