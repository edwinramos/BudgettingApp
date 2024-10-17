using System;
using System.Collections.Generic;
using BudgettingApp.Interfaces;

namespace BudgettingApp.Services.Goals
{
    public class GoalsService
    {
        private static ILocalDbService _cache;
        public GoalsService(ILocalDbService cache) 
        {
            _cache = cache;
        }

        public void Set()
        {
            string key = "";
            _cache.Set(key);

        }
        public void Get()
        {
            string key = "";
            _cache.Get(key);

        }
        public void GetList()
        {
            string key = "";
        }
    }
}
