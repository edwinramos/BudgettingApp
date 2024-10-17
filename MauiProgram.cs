﻿using BudgettingApp.ViewModels;
using BudgettingApp.Views;
using BudgettingApp.Views.Goal;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using BudgettingApp.Services.Cache;
using BudgettingApp.Interfaces;
using BudgettingApp.Models;
using Controls.UserDialogs.Maui;

namespace BudgettingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews()
                .UseUserDialogs()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            //mauiAppBuilder.Services.AddTransient<ILoggingService, LoggingService>();
            //mauiAppBuilder.Services.AddTransient<ISettingsService, SettingsService>();
            mauiAppBuilder.Services.AddTransient<ILocalDbService<Goal>, LocalDbService<Goal>>();
            // More services registered here.

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<DashboardViewModel>();
            mauiAppBuilder.Services.AddSingleton<GoalDetailViewModel>();

            // More view-models registered here.

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainPage>();
            mauiAppBuilder.Services.AddSingleton<Dashboard>();
            mauiAppBuilder.Services.AddSingleton<GoalDetail>();

            // More views registered here.

            return mauiAppBuilder;
        }
    }
}