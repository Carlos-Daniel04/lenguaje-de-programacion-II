using Spectre.Console;


AnsiConsole.Write(new FigletText("Tabla de Amortización").Centered().Color(Color.Orange1));

string nombre = AnsiConsole.Ask<string>("¿Cuál es tu [green]nombre[/]");
string apellido = AnsiConsole.Ask<string>("¿Cuál es tu [green]apellido[/]\n");
AnsiConsole.MarkupLine($"[bold blue]Bienvenido a tu[/] [green]Tabla de Amortización[/], {nombre} {apellido}!\n");

decimal monto = AnsiConsole.Ask<decimal>("¿Cuál es el [green]monto del préstamo[/]");
decimal tasaInteres = AnsiConsole.Ask<decimal>("[green]tasa de interés[/] [yellow](en %)[/]");
int plazoMeses = AnsiConsole.Ask<int>("¿Cuál es el [green]plazo del préstamo[/] [yellow](en meses)[/]");

var panel = new Panel($"[yellow]Monto:[/] {monto:C}\n[yellow]Tasa de Interés:[/] {tasaInteres}%\n[yellow]Plazo:[/] {plazoMeses} meses")
    .Header("Detalles")
    .BorderStyle(new Style(Color.Orange1));
AnsiConsole.WriteLine();

AnsiConsole.Write(panel);
AnsiConsole.WriteLine();

if(AnsiConsole.Confirm("[yellow]Deseas generar la tabla de amortización?[/]\n"))
{
    AnsiConsole.MarkupLine("[green]Generando tu tabla de amortización...[/]\n");
    AnsiConsole.Status()
        .Start("Calculando...", ctx =>
        {
            Thread.Sleep(3000);
        } );
    AnsiConsole.WriteLine();
    decimal tasaMensual = (tasaInteres / 100) / 12;

    // Calcular cuota fija
    decimal cuota = monto *
        (tasaMensual * (decimal)Math.Pow((double)(1 + tasaMensual), plazoMeses)) /
        ((decimal)Math.Pow((double)(1 + tasaMensual), plazoMeses) - 1);

    var tabla = new Table()
        .BorderStyle(new Style(Color.Orange1))
        .Border(TableBorder.Rounded)
        .AddColumn("[yellow]Cuota[/]")
        .AddColumn("[yellow]Pago[/]")
        .AddColumn("[yellow]Interés[/]")
        .AddColumn("[yellow]Abono Capital[/]")
        .AddColumn("[yellow]Saldo[/]");

    decimal saldo = monto;

    for (int i = 1; i <= plazoMeses; i++)
    {
        decimal interes = saldo * tasaMensual;
        decimal abonoCapital = cuota - interes;
        saldo -= abonoCapital;

        // Evita saldos negativos por redondeo
        if (saldo < 0)
            saldo = 0;

        tabla.AddRow(
            i.ToString(),
            cuota.ToString("C"),
            interes.ToString("C"),
            abonoCapital.ToString("C"),
            saldo.ToString("C")
        );
    }

    AnsiConsole.Write(tabla);
}
else
{
    AnsiConsole.MarkupLine("[red]Operación cancelada.[/]");
    return;
    
}


Console.ReadKey();
