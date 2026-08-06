using GestorDeContraseña.Database;
using GestorDeContraseña.Repository;
using System;
using GestorDeContraseña.Screens;
using GestorDeContraseña.Servers;
using SQLitePCL;

using var context = new AppDbContext();
var repository = new CredencialRepository(context);
var servers = new PasswordServers(repository);
var menuScreen = new MenuScreens(servers);

await menuScreen.ShowMainMenuAsync();

