using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using LiteDB;

namespace BudgettingApp.Models
{
    public class Expense : BaseModel
    {
        public string GoalKey { get; set; }
        public string Comment { get; set; }
        public int ExpenseAmount { get; set; }
        public DateTime ExpenseDateTime { get; set; } = DateTime.Today;
    }
}
