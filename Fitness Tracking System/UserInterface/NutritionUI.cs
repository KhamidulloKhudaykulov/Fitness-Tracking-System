using Fitness_Tracking_System.Models;
using Fitness_Tracking_System.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Tracking_System.UserInterface;

public class NutritionUI
{
    private NutritionService nutritionService;
    public NutritionUI(NutritionService nutritionService)
    {
        this.nutritionService = nutritionService;
    }
    public void Display()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]*** Nutrition Menu ***[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "My Nutritions", "Create a new Nutrition", "Update Nutrition",
                        "Delete Nutrition",
                        "Go back",
                    }));
            switch (Choice)
            {
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
                    var name = AnsiConsole.Ask<string>("[bold yellow]Enter Nutrition Name:[/]");
                    try
                    {
                        nutritionService.Create(new Nutrition { Name = name });
                        AnsiConsole.Markup($"[slowblink yellow]The new nutrition is created![/]");
                        Thread.Sleep(1500);
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.Markup($"[slowblink red]{ex.Message}[/]");
                        Thread.Sleep(3500);
                    }
                    break;
                case "Update Workout":
                    var id = AnsiConsole.Ask<int>("[bold yellow]Enter nutrition Id:[/]");
                    try
                    {
                        var workout = nutritionService.GetById(id);
                        string workoutLastName = workout.Name;
                        var updateName = AnsiConsole.Ask<string>("[bold yellow]Enter nutrition Name to Update:[/]");
                        nutritionService.Update(id, new Nutrition { Name = updateName });
                        AnsiConsole.MarkupLine($"[bold red]{workoutLastName}[/] => [bold green]{updateName}[/]");
                        Thread.Sleep(1500);
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.Markup($"[slowblink red]{ex.Message}[/]");
                        Thread.Sleep(3500);
                    }
                    break;
                case "Delete Workout":
                    var deleteId = AnsiConsole.Ask<int>("[bold yellow]Enter nutrition Id:[/]");
                    try
                    {
                        var workout = nutritionService.GetById(deleteId);
                        nutritionService.Delete(deleteId);
                        AnsiConsole.MarkupLine($"[slowblink red]The nutrition with Id {deleteId} is deleted[/]");
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
        var table = new Table();
        table.Border = TableBorder.DoubleEdge;
        table.Title("[bold green]My Nutritions[/]");
        table.AddColumn("[slowblink yellow]Id[/]");
        table.AddColumn("[slowblink yellow]Name[/]");
        foreach (var item in nutritionService.GetAll())
            table.AddRow($"{item.Id}", $"{item.Name}");

        AnsiConsole.Write(table);
    }
}
