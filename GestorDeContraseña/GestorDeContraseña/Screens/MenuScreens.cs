using GestorDeContraseña.Servers;
using GestorDeContraseña.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeContraseña.Screens
{
    internal class MenuScreens
    {
        private readonly PasswordServers _servers;

        public MenuScreens(PasswordServers servers)
        {
            _servers = servers;
        }

        public async Task ShowMainMenuAsync()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("PassManager")
                        .Centered()
                        .Color(Color.Blue));

                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]Selecciona una opción:[/]")
                        .PageSize(10)
                        .AddChoices(new[] {
                        "1. Ver todas las credenciales",
                        "2. Agregar nueva credencial",
                        "3. Actualizar credencial",
                        "4. Eliminar credencial",
                        "5. Salir"
                        }));

                switch (option)
                {
                    case "1. Ver todas las credenciales":
                        await ListCredentialsAsync();
                        break;
                    case "2. Agregar nueva credencial":
                        await AddCredentialAsync();
                        break;
                    case "3. Actualizar credencial":
                        await UpdateCredentialAsync();
                        break;
                    case "4. Eliminar credencial":
                        await DeleteCredentialAsync();
                        break;
                    case "5. Salir":
                        return;
                }

                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
                Console.ReadKey(true);
            }
        }

        private async Task ListCredentialsAsync()
        {
            var credentials = await _servers.GetCredentialsAsync();

            if (!credentials.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No hay credenciales registradas.[/]");
                return;
            }

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumn("[bold]ID[/]");
            table.AddColumn("[bold]Sitio / Servicio[/]");
            table.AddColumn("[bold]Usuario[/]");
            table.AddColumn("[bold]Contraseña[/]");

            foreach (var c in credentials)
            {
                table.AddRow(c.Id.ToString(), c.SiteName, c.Username, "••••••••");
            }

            AnsiConsole.Write(table);
        }

        private async Task AddCredentialAsync()
        {
            AnsiConsole.MarkupLine("[bold green]=== Agregar Credencial ===[/]");

            var siteName = AnsiConsole.Ask<string>("Nombre del sitio/servicio:");
            var username = AnsiConsole.Ask<string>("Nombre de usuario:");

            // Uso de TextPrompt con Secret()
            // esto es para que la contraseña no se muestre en la consola mientras se escribe
            // y se refleje asi ************
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Contraseña:")
                    .PromptStyle("red")
                    .Secret());

            await _servers.CreateCredencialAsync(siteName, username, password);
            AnsiConsole.MarkupLine("[bold green]¡Credencial guardada correctamente![/]");
        }

        private async Task UpdateCredentialAsync()
        {
            await ListCredentialsAsync();
            AnsiConsole.WriteLine();

            var id = AnsiConsole.Ask<int>("Ingresa el ID de la credencial a actualizar:");
            var existing = await _servers.GetCredentialByIdAsync(id);

            if (existing == null)
            {
                AnsiConsole.MarkupLine("[red]No se encontró ninguna credencial con ese ID.[/]");
                return;
            }

            var siteName = AnsiConsole.Confirm("¿Deseas cambiar el nombre del sitio?")
                ? AnsiConsole.Ask<string>("Nuevo sitio:")
                : existing.SiteName;

            var username = AnsiConsole.Confirm("¿Deseas cambiar el usuario?")
                ? AnsiConsole.Ask<string>("Nuevo usuario:")
                : existing.Username;

            var password = existing.Password;
            if (AnsiConsole.Confirm("¿Deseas cambiar la contraseña?"))
            {
                password = AnsiConsole.Prompt(
                    new TextPrompt<string>("Nueva contraseña:")
                        .PromptStyle("red")
                        .Secret());
            }

            await _servers.UpdateCredentialAsync(id, siteName, username, password);
            AnsiConsole.MarkupLine("[bold green]¡Credencial actualizada con éxito![/]");
        }

        private async Task DeleteCredentialAsync()
        {
            await ListCredentialsAsync();
            AnsiConsole.WriteLine();

            var id = AnsiConsole.Ask<int>("Ingresa el ID de la credencial a eliminar:");

            if (AnsiConsole.Confirm("[bold red]¿Estás seguro de que deseas eliminar esta credencial?[/]"))
            {
                var result = await _servers.DeleteCredentialAsync(id);
                if (result)
                    AnsiConsole.MarkupLine("[bold green]Credencial eliminada correctamente.[/]");
                else
                    AnsiConsole.MarkupLine("[red]No se encontró el registro.[/]");
            }
        }
    }
}
