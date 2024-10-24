using LiteDB;
using Newtonsoft.Json;
using BudgettingApp.Interfaces;
using BudgettingApp.Models;
using System.Linq.Expressions;

namespace BudgettingApp.Services.Cache
{        /// <summary>
         /// Provides caching services
         /// </summary>
    public class LocalDbService<T> : ILocalDbService<T>
    {
        private LiteDatabase _database;
        private ILiteCollection<T> _collection;
        public LocalDbService()
        {
            string appDataPath = FileSystem.AppDataDirectory;

            // Combine the path with your database file name
            string dbPath = Path.Combine(appDataPath, "MyData.db");
            _database = new LiteDatabase(dbPath);
            _collection = _database.GetCollection<T>(typeof(T).Name);
            //_collection.DeleteAll();
            //_collection.EnsureIndex(x => x.Key);
        }
        public IEnumerable<T> GetAll()
        {
            var res = _collection.FindAll().ToList();
            ResetConenction();
            return res;
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            var result = _collection.FindOne(expression);
            ResetConenction();
            return result;
        }

        public bool Add(T obj)
        {
            try
            {
                var m = obj as BaseModel;
                if (string.IsNullOrEmpty(m.Key))
                    m.Key = Guid.NewGuid().ToString();

                var res = _collection.Insert(obj);
                ResetConenction();
                return res;
            }
            catch (Exception ex) 
            {
            }
            return false;
        }

        public bool Update(T person)
        {
            var res = _collection.Update(person);
            ResetConenction();
            return res;
        }

        public bool Delete(string key)
        {
            return _collection.Delete(key);
        }

        private void ResetConenction()
        {
            _database.Dispose();
            string appDataPath = FileSystem.AppDataDirectory;
            string dbPath = Path.Combine(appDataPath, "MyData.db");
            _database = new LiteDatabase(dbPath);
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }
    }
}

