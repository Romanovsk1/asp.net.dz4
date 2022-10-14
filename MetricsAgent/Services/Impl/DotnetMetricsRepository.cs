using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.Impl
{
    public class DotnetMetricsRepository : IDotnetMetricsRepository
    {
        

        public void Create(DotnetMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
            //using var connection = new SQLiteConnection(ConnectionString);
            //connection.Open();
            // Создаём команду
            //using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на вставку данных
            //cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";
            // Добавляем параметры в запрос из нашего объекта
            //cmd.Parameters.AddWithValue("@value", item.Value);
            // В таблице будем хранить время в секундах
            //cmd.Parameters.AddWithValue("@time", item.Time);
            // подготовка команды к выполнению
            //cmd.Prepare();
            // Выполнение команды
            //cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
                new
                {
                    Id = id
                });
            //using var connection = new SQLiteConnection(ConnectionString);
            //connection.Open();
            //using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на удаление данных
            //cmd.CommandText = "DELETE FROM cpumetrics WHERE id=@id";
            //cmd.Parameters.AddWithValue("@id", id);
            //cmd.Prepare();
            //cmd.ExecuteNonQuery();
        }

        public IList<DotnetMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<DotnetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();

            //using var connection = new SQLiteConnection(ConnectionString);
            ///connection.Open();
            //using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на получение всех данных из таблицы
            //cmd.CommandText = "SELECT * FROM cpumetrics";
            //var returnList = new List<DotnetMetric>();
            //using (SQLiteDataReader reader = cmd.ExecuteReader())
            //{
            // Пока есть что читать — читаем
            //  while (reader.Read())
            //{
            // Добавляем объект в список возврата
            //  returnList.Add(new DotnetMetric
            //{
            //  Id = reader.GetInt32(0),
            //Value = reader.GetInt32(1),
            //  Time = reader.GetInt32(2)
            //});
            //   }
            //}
            //return returnList;
        }

        public DotnetMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.QuerySingle<DotnetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE id = @id",
            new { id = id });
            //  using var connection = new SQLiteConnection(ConnectionString);
            //connection.Open();
            //using var cmd = new SQLiteCommand(connection);
            //cmd.CommandText = "SELECT * FROM cpumetrics WHERE id=@id";
            //using (SQLiteDataReader reader = cmd.ExecuteReader())
            //{
            // Если удалось что-то прочитать
            //  if (reader.Read())
            //{
            // возвращаем прочитанное
            //  return new DotnetMetric
            //{
            //  Id = reader.GetInt32(0),
            //Value = reader.GetInt32(1),
            //Time = reader.GetInt32(2)
            //};
            //}
            //else
            //{
            // Не нашлась запись по идентификатору, не делаем ничего
            //  return null;
            //}
            //}
        }

        /// <summary>
        /// Получение данных по нагрузке на ЦП за период
        /// </summary>
        /// <param name="timeFrom">Время начала периода</param>
        /// <param name="timeTo">Время окончания периода</param>
        /// <returns></returns>
        public IList<DotnetMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<DotnetMetric>("SELECT * FROM dotnetmetrics where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
            //  using var connection = new SQLiteConnection(ConnectionString);
            //connection.Open();
            //using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на получение всех данных за период из таблицы
            //cmd.CommandText = "SELECT * FROM cpumetrics where time >= @timeFrom and time <= @timeTo";
            //cmd.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds);
            //cmd.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds);
            //var returnList = new List<DotnetMetric>();
            //using (SQLiteDataReader reader = cmd.ExecuteReader())
            //{
            // Пока есть что читать — читаем
            //  while (reader.Read())
            //{
            // Добавляем объект в список возврата
            //  returnList.Add(new DotnetMetric
            //{
            //  Id = reader.GetInt32(0),
            // Value = reader.GetInt32(1),
            //Time = reader.GetInt32(2)
            //});
            //}
            //}
            //return returnList;
        }

        public void Update(DotnetMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id; ",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
            // using var connection = new SQLiteConnection(ConnectionString);
            // using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на обновление данных
            // cmd.CommandText = "UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id; ";
            // cmd.Parameters.AddWithValue("@id", item.Id);
            // cmd.Parameters.AddWithValue("@value", item.Value);
            // cmd.Parameters.AddWithValue("@time", item.Time);
            // cmd.Prepare();
            // cmd.ExecuteNonQuery();
        }
        

        #region Private Fields

        private readonly IOptions<DatabaseOptions> _databaseOptions;

        #endregion

        #region Constructors

        public DotnetMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        #endregion

    }
}
