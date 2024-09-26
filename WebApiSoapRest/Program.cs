using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using WebApplication3;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar controladores REST al proyecto (si ya tienes controladores REST)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Add WSDL support
builder.Services.AddServiceModelServices().AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// 3. Mapear los controladores REST a las rutas que ya tienes
app.MapControllers();

// 4. Configurar el servicio SOAP usando CoreWCF
app.UseServiceModel(builder =>
{
    builder.AddService<MySoapService>((serviceOptions) => { })
    // Add a BasicHttpBinding at a specific endpoint
    .AddServiceEndpoint<MySoapService, IMySoapService>(new BasicHttpBinding(), "/Soap.svc")
    // Add a WSHttpBinding with Transport Security for TLS
    .AddServiceEndpoint<MySoapService, IMySoapService>(new WSHttpBinding(SecurityMode.Transport), "/Soaps.svc");

});
var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;

// 6. Iniciar la aplicación
app.Run();
