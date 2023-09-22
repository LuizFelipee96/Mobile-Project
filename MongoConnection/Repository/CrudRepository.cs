using MongoConnection.Configure;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnection.Repository
{
    public class CrudRepository<T>
    {
        private readonly IMongoCollection<T> _workoutCollection;

        public CrudRepository(string databaseName, string colletionName)
        {
            var mongoClient = DataBaseConfigure.ConfigureConnection();
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _workoutCollection = mongoDatabase.GetCollection<T>(colletionName);
        }

        public void InsertWorkout(T workout)
        {
            _workoutCollection.InsertOne(workout);
        }

        public void InsertManyWorkouts(List<T> workout)
        {
            _workoutCollection.InsertMany(workout);
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _workoutCollection.Find(predicate).First();
        }

        public T FindById(Expression<Func<T, bool>> predicate)
        {
            return _workoutCollection.Find(predicate).First();
        }

        public List<T> GetAllXmls()
        {
            List<T> workouts = _workoutCollection.Find(_ => true).ToList();
            return workouts;
        }

        public List<T> GetAllXmls(int? skip, int? take, out long totalResultsCount)
        {
            List<T> workouts = _workoutCollection.Find(_ => true).Skip(skip).Limit(take).ToList();
            totalResultsCount = _workoutCollection.CountDocuments(_ => true);
            return workouts;
        }

        public void UpdateWorkout(Expression<Func<T, bool>> filter, T update) => _workoutCollection.ReplaceOne(filter, update);

        public void RemoveWorkout(Expression<Func<T, bool>> filter) => _workoutCollection.DeleteOne(filter);
    }
}
