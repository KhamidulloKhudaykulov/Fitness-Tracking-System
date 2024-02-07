using Fitness_Tracking_System.Models;
using Fitness_Tracking_System.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Tracking_System.UserInterface;

public class RegisterUI
{
    private UserService userService;
    public RegisterUI()
    {
        userService = new UserService();
    }
    public void Display()
    {
        AnsiConsole.Progress()
            .Start(context =>
            {
                var progress = context.AddTask("[bold red]Checking your authentication. Please Wait[/]");

                progress.MaxValue = 100;
                for (var i = 0; i <= progress.MaxValue; i++)
                {
                    progress.IsIndeterminate = i == 0;
                    progress.Value = i;
                    Thread.Sleep(i == 0 ? 1500 : 20);
                }
            });
        if (userService.GetInformation() ==  null) 
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[slowblink yellow]You are not Registered. Please Sign UP!!![/]\n");
            var email = AnsiConsole.Ask<string>("[bold yellow]Enter your email:[/]");
            var password = AnsiConsole.Ask<string>("[bold yellow]Enter password:[/]");

            AnsiConsole.Clear();
            AnsiConsole.Markup("[slowblink yellow]Great. Now you can enter your proporties[/]\n");
            var firstName = AnsiConsole.Ask<string>("[bold yellow]Enter your FirstName:[/]");
            var lastName = AnsiConsole.Ask<string>("[bold yellow]Enter your LastName:[/]");
            var age = AnsiConsole.Ask<int>("[bold yellow]Enter your age:[/]");
            var phone = AnsiConsole.Ask<string>("[bold yellow]Enter your phone number:[/]");
            var height = AnsiConsole.Ask<decimal>("[bold yellow]Enter your height:[/]");
            var weight = AnsiConsole.Ask<decimal>("[bold yellow]Enter your weight:[/]");

            var user = new User() 
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Email = email,
                Password = password,
                Phone = phone,
                Height = height,
                Weight = weight
            };
            userService.Create(user);
            AnsiConsole.Status()
            .Start("Creating new User...", ctx =>
            {
                // Simulate some work
                AnsiConsole.MarkupLine($"{firstName}...");
                Thread.Sleep(1000);

                AnsiConsole.MarkupLine($"{lastName}...");
                Thread.Sleep(1000);

                AnsiConsole.MarkupLine($"{age}...");
                Thread.Sleep(1000);

                AnsiConsole.MarkupLine($"{email}...");
                Thread.Sleep(1000);

                AnsiConsole.MarkupLine($"{password}...");
                Thread.Sleep(1000);

                AnsiConsole.MarkupLine($"{phone}...");
                Thread.Sleep(1000);

                AnsiConsole.MarkupLine($"{height}...");
                Thread.Sleep(1000);

                AnsiConsole.MarkupLine($"{weight}...");
                Thread.Sleep(1000);

                // Update the status and spinner
                ctx.Status("Great");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));

                // Simulate some work
                Thread.Sleep(2000);
            });
        }
        else
        {
            AnsiConsole.Status()
            .Start("Thinking...", ctx =>
            {
                // Simulate some work
                AnsiConsole.MarkupLine($"[bold green]{userService.GetInformation().FirstName}[/]");
                Thread.Sleep(1000);

                // Update the status and spinner
                ctx.Status("[bold red]Uploading...[/]");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));

                // Simulate some work
                AnsiConsole.MarkupLine($"[bold green]{userService.GetInformation().LastName}[/]");
                Thread.Sleep(2000);
            });
        }

        MainUI mainUI = new MainUI();
        mainUI.Display();
    }
}
