// esto es un programa basico en c# que pide al usuario su nombre, apellido, edad y altura, y luego muestra un mensaje con esa información.
//


Console.WriteLine("llene los siguientes datos:");
Console.WriteLine("===============================================");
Console.Write("Nombre: ");
string nombre = Console.ReadLine();
Console.Write("Apellido:");
string apellido = Console.ReadLine();
Console.Write("Edad: ");
int edad = int.Parse(Console.ReadLine());
Console.Write("Altura: ");
float altura = float.Parse(Console.ReadLine());
Console.WriteLine("===============================================");

Console.WriteLine($"Hola mi es {nombre} {apellido}, tengo {edad} años y mido {altura} metros.");




Console. ReadKey();