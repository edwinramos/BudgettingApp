using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using LiteDB;

namespace BudgettingApp.Models
{
    public enum TimePeriods
    {
        Weekly = 1,
        BiWeekly = 2,
        Monthly = 3,
        Yearly = 4
    }

    public class Goal : BaseModel
    {
        public string Title { get; set; }
        public string BackgroundColor { get; set; }
        public int Budget { get; set; }
        public int Expenses { get; set; }
        public int TimePeriod { get; set; }
        [BsonIgnore]
        public TimePeriods DisplayTimePeriod { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        [BsonIgnore]
        public DateTime EndDate => CalculateEndDate();

        private DateTime CalculateEndDate()
        {
            switch (DisplayTimePeriod)
            {
                case TimePeriods.Weekly:
                    return StartDate.AddDays(7);
                case TimePeriods.BiWeekly:
                    return StartDate.AddDays(14);
                case TimePeriods.Monthly:
                    return StartDate.AddMonths(1);
                case TimePeriods.Yearly:
                    return StartDate.AddYears(1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ChangeTimePeriod(TimePeriods newPeriod)
        {
            TimePeriod = (int)newPeriod;
        }
    }
}
