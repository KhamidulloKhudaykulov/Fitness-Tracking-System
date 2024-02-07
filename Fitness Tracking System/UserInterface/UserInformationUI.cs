using Fitness_Tracking_System.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Tracking_System.UserInterface;

public class UserInformationUI
{
    private UserService userService;
    public UserInformationUI(UserService userService)
    {
        this.userService = userService;
    }

    public void Display()
    {
        AnsiConsole.Clear();
        var table = new Table().Centered();

        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                table.AddColumn("[slowblink yellow]First Name[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn("[slowblink yellow]Last Name[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn("[slowblink yellow]Age[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn("[slowblink yellow]Email[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn("[slowblink yellow]Password[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn("[slowblink yellow]Phone[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn("[slowblink yellow]Height[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn("[slowblink yellow]Weight[/]");
                ctx.Refresh();
                Thread.Sleep(300);
            });

        AnsiConsole.Clear();

        var user = userService.GetInformation();
        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                table.AddRow($"[yellow]{user.FirstName}[/]",
                    $"[yellow]{user.LastName}[/]", 
                    $"[yellow]{user.Age}[/]",
                    $"[yellow]{user.Email}[/]",
                    $"[yellow]{user.Password}[/]",
                    $"[yellow]{user.Phone}[/]",
                    $"[yellow]{user.Height}[/] m",
                    $"[yellow]{user.Weight}[/] kg");
                ctx.Refresh();
                Thread.Sleep(300);
            });

        AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .PageSize(10)
            .AddChoices(new[]
            {
                "Continue",
            }));
    }
}
