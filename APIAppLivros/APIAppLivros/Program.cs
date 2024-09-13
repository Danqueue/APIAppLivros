using LivrosAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container.
builder.Services.AddControllers();

// Configuração da conexão com o banco de dados
builder.Services.AddSingleton<LivroRepository>(
    provider => new LivroRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Configuração do Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud Livros V1");
        c.RoutePrefix = string.Empty;
    });
}

// Pipeline de requisição HTTP
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();