//Ahora crearemos el modelo de datos q sera recibido desde el post
public class person
{
    public String name { get; set; }
    public int age { get; set; }
    public String email { get; set; }
    public String phone { get; set; }
    public Boolean status { get; set; }
    public List<files_b64>? filesb64 { get; set; }
}
public class files_b64
{
    public string? name { get; set; }
    public string? file { get; set; }
}