using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(
    api => api
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//  Usare este EP de un proyecto tipo Minimal API de MVC para validar la recepcion de datos desde un form
//  Los datos recibidos seran recibidos por un formulario HTML basico con javascript
//  Voy a modificar el EP para q pueda recibir los archivos en base 64
app.MapPost("/save", IResult (person u) => {
    var user = JsonConvert.SerializeObject(u);
    //Console.WriteLine($"user => {user}");
    Console.WriteLine(u.name);
    Console.WriteLine(u.age);
    Console.WriteLine(u.email);
    Console.WriteLine(u.phone);
    Console.WriteLine(u.status);
    //verificamos q no sea nulo este objeto
    //Vamos a hacer un procedimiento para poder almacenar los archivos en local del lado del server
    var pf = $"D:\\temp\\";//esta es la ruta donde se almacenaran
    if (u.filesb64 != null)
    {
        foreach (var f in u.filesb64)
        {
            //de los archivos recibidos solo imprimire el nombre ya q el string base64 es demasiado extenso
            Console.WriteLine($"file name = {f.name}, size = {f.file?.Length}");
            //  Separamos el string base 64 recibido por la primera coma q aparece en el string
            var sp = f.file?.Split(',');
            var b = Convert.FromBase64String(sp[1]);    //Esta variable convierte el string a bytes
            var pathfile = $"{pf}{f.name}";
            using var stream = File.Create(pathfile);
            stream.Write(b, 0, b.Length);
        }
    }
    //   Para regresar un resultado mandaremos solo un mensaje de OK
    //voy a regresar un mensaje
    return Results.Ok(new { message = "Success" });
});

app.Run("https://localhost:5000");

