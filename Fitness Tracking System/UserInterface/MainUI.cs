using Fitness_Tracking_System.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Tracking_System.UserInterface;

public class MainUI
{
    private WorkoutUI workoutUI;
    private NutritionUI nutritionUI;
    private ProgressUI progressUI;
    private UserInformationUI userInformationUI;

    private WorkoutService workoutService;
    private UserService userService;
    private NutritionService nutritionService;

    public MainUI()
    {
        this.userService = new UserService();
        this.workoutService = new WorkoutService();
        this.nutritionService = new NutritionService();

        workoutUI = new WorkoutUI(workoutService, userService);
        nutritionUI = new NutritionUI(nutritionService);
        progressUI = new ProgressUI(workoutService);
        userInformationUI = new UserInformationUI(userService);

    }
    public void Display()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Fitness Tracking System")
                    .LeftJustified()
                    .Color(Color.Red));
            var Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Workout", "Nutrition", "Progress",
                        "My informations",
                        "Exit",
                    }));
            switch (Choice)
            {
                case "Workout":
                    workoutUI.Display();
                    break;
                case "Nutrition":
                    nutritionUI.Display();
                    break;
                case "Progress":
                    progressUI.Display();
                    break;
                case "My informations":
                    userInformationUI.Display();
                    break;
                case "Exit":
                    return;
            }
        }
    }
}
