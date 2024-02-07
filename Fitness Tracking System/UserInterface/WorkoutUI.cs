using Fitness_Tracking_System.Models;
using Fitness_Tracking_System.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Tracking_System.UserInterface;

public class WorkoutUI
{
    private WorkoutService workoutService;
    private UserService userService;
    public WorkoutUI(WorkoutService workoutService, UserService userService)
    {
        this.workoutService = workoutService;
        this.userService = userService;
    }
    public void Display()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]*** Workout Menu ***[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "My Workouts", "Create a new Workout", "Update Workout", "Note Done Workouts",
                        "Delete Workout",
                        "Go back",
                    }));
            switch (Choice)
            {
                case "Note Done Workouts":
                    var note = AnsiConsole.Ask<int>("[bold yellow]Enter Workout Id:[/]");
                    try
                    {
                        var workout = workoutService.GetById(note);
                        var checkDone = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold green]Is this workout is done?[/]")
                                .PageSize(3)
                                .AddChoices(new[]
                                {
                                    "yes", "no",
                                }));
                        switch (checkDone)
                        {
                            case "yes":
                                workout.IsActive = false;
                                break;
                            default:
                                workout.IsActive = true;
                                break;
                        }
                        workoutService.Update(note, workout);
                        break;
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.Markup($"[slowblink red]{ex.Message}[/]");
                        Thread.Sleep(3500);
                    }
                        break;
                case "My Workouts":
                    ListOfWorkouts();
                    AnsiConsole.WriteLine();
                    AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(10)
                            .AddChoices(new[]
                            {
                                "Continue",
                            }));
                    break;
                case "Create a new Workout":
                    var name = AnsiConsole.Ask<string>("[bold yellow]Enter Workout Name:[/]");
                    try
                    {
                        workoutService.Create(new Workout { Name = name });
                        AnsiConsole.Markup($"[slowblink yellow]The new Workout is created![/]");
                        Thread.Sleep(1500);
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.Markup($"[slowblink red]{ex.Message}[/]");
                        Thread.Sleep(3500);
                    }
                    break;
                case "Update Workout":
                    var id = AnsiConsole.Ask<int>("[bold yellow]Enter Workout Id:[/]");
                    try
                    {
                        var workout = workoutService.GetById(id);
                        string workoutLastName = workout.Name;
                        var updateName = AnsiConsole.Ask<string>("[bold yellow]Enter Workout Name to Update:[/]");
                        workoutService.Update(id, new Workout { Name = updateName });
                        AnsiConsole.MarkupLine($"[bold red]{workoutLastName}[/] => [bold green]{updateName}[/]");
                        Thread.Sleep(1500);
                    }
                    catch ( Exception ex )
                    {
                        AnsiConsole.Markup($"[slowblink red]{ex.Message}[/]");
                        Thread.Sleep(3500);
                    }
                    break;
                case "Delete Workout":
                    var deleteId = AnsiConsole.Ask<int>("[bold yellow]Enter Workout Id:[/]");
                    try
                    {
                        var workout = workoutService.GetById(deleteId);
                        workoutService.Delete(deleteId);
                        AnsiConsole.MarkupLine($"[slowblink red]The wokout with Id {deleteId} is deleted[/]");
                        Thread.Sleep(2000);
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.Markup($"[slowblink red]{ex.Message} [/]");
                        Thread.Sleep(3500);
                    }
                        break;
                case "Go back":
                    return;
            }
        }
    }

    public void ListOfWorkouts()
    {
        var table = new Table().Centered();

        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                table.AddColumn("[slowblink yellow]Id[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn("[slowblink yellow]Name[/]");
                ctx.Refresh();
                Thread.Sleep(300);

                table.AddColumn("[slowblink yellow]IsActive[/]");
                ctx.Refresh();
                Thread.Sleep(300);
            });

        AnsiConsole.Clear();

        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                foreach (var item in workoutService.GetAll())
                {
                    table.AddRow($"[bold green]{item.Id}[/]", $"[bold green]{item.Name}[/]", $"[bold green]{item.IsActive}[/]");
                    ctx.Refresh();
                    Thread.Sleep(100);
                }
            });

    }
}
