using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using LiteDB;

namespace BudgettingApp.Models
{
    public class BaseModel : ObservableObject
    {
        [BsonId]
        public string Key { get; set; }

    }
}
