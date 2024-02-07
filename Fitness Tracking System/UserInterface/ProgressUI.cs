using Fitness_Tracking_System.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Tracking_System.UserInterface;

public class ProgressUI
{
    private WorkoutService workoutService;
    public ProgressUI(WorkoutService workoutService)
    {
        this.workoutService = workoutService;
    }
    public void Display()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new BreakdownChart()
            .Width(60)
            // Add item is in the order of label, value, then color.
            .AddItem("Done Workouts", workoutService.GetDone(), Color.Blue)
            .AddItem("Not done workouts", workoutService.GetNotDone(), Color.Red));

        AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(10)
                            .AddChoices(new[]
                            {
                                "Continue",
                            }));
    }
}
