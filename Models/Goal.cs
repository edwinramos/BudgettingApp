using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using LiteDB;

namespace BudgettingApp.Models
{
    public class Goal : BaseModel
    {
        public string Title { get; set; }
        public string BackgroundColor { get; set; }
        public int Budget { get; set; }
        public int Expenses { get; set; }

    }
}
