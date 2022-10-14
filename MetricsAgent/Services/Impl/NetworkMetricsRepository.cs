using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;


namespace MetricsAgent.Services.Impl
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";


        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
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
            connection.Execute("DELETE FROM networkmetrics WHERE id=@id",
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

        public IList<NetworkMetric> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics").ToList();

            // using var connection = new SQLiteConnection(ConnectionString);
            // connection.Open();
            // using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на получение всех данных из таблицы
            // cmd.CommandText = "SELECT * FROM cpumetrics";
            // var returnList = new List<NetworkMetric>();
            // using (SQLiteDataReader reader = cmd.ExecuteReader())
            // {
            // Пока есть что читать — читаем
            //   while (reader.Read())
            //   {
            // Добавляем объект в список возврата
            //        returnList.Add(new NetworkMetric
            //      {
            //         Id = reader.GetInt32(0),
            //       Value = reader.GetInt32(1),
            //     Time = reader.GetInt32(2)
            // });
            // }
            //}
            //return returnList;
        }

        public NetworkMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.QuerySingle<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics WHERE id = @id",
            new { id = id });
            //using var connection = new SQLiteConnection(ConnectionString);
            //connection.Open();
            //using var cmd = new SQLiteCommand(connection);
            //cmd.CommandText = "SELECT * FROM cpumetrics WHERE id=@id";
            //using (SQLiteDataReader reader = cmd.ExecuteReader())
            //{
            // Если удалось что-то прочитать
            //  if (reader.Read())
            //  {
            // возвращаем прочитанное
            //      return new NetworkMetric
            //      {
            //         Id = reader.GetInt32(0),
            //        Value = reader.GetInt32(1),
            //      Time = reader.GetInt32(2)
            // };
            // }
            // else
            // {
            // Не нашлась запись по идентификатору, не делаем ничего
            //     return null;
            // }
            //}
        }

        /// <summary>
        /// Получение данных по нагрузке на ЦП за период
        /// </summary>
        /// <param name="timeFrom">Время начала периода</param>
        /// <param name="timeTo">Время окончания периода</param>
        /// <returns></returns>
        public IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
            //using var connection = new SQLiteConnection(ConnectionString);
            //connection.Open();
            //using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на получение всех данных за период из таблицы
            //cmd.CommandText = "SELECT * FROM cpumetrics where time >= @timeFrom and time <= @timeTo";
            //cmd.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds);
            //cmd.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds);
            //var returnList = new List<NetworkMetric>();
            //using (SQLiteDataReader reader = cmd.ExecuteReader())
            //{
            // Пока есть что читать — читаем
            //    while (reader.Read())
            //   {
            // Добавляем объект в список возврата
            //      returnList.Add(new NetworkMetric
            //     {
            //        Id = reader.GetInt32(0),
            //        Value = reader.GetInt32(1),
            //      Time = reader.GetInt32(2)
            //  });
            // }
            //}
            //return returnList;
        }

        public void Update(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id; ",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
            //using var connection = new SQLiteConnection(ConnectionString);
            //using var cmd = new SQLiteCommand(connection);
            // Прописываем в команду SQL-запрос на обновление данных
            //cmd.CommandText = "UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id; ";
            //cmd.Parameters.AddWithValue("@id", item.Id);
            //cmd.Parameters.AddWithValue("@value", item.Value);
            //cmd.Parameters.AddWithValue("@time", item.Time);
            //cmd.Prepare();
            //cmd.ExecuteNonQuery();
        }
        #region Private Fields

        private readonly IOptions<DatabaseOptions> _databaseOptions;

        #endregion

        #region Constructors

        public NetworkMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        #endregion

    }
}
