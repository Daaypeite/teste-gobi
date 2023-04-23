using Gobi.Test.Jr.Domain;
using Gobi.Test.Jr.Domain.Interfaces;
using System.Data.SQLite;

namespace Gobi.Test.Jr.Infra
{
    public class TodoItemRepository : ITodoItemRepository
    {
        public TodoItemRepository()
        {
            CreateDatabase();
            CreateTable();
        }

        private static SQLiteCommand CreateCommand()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gobi.sqlite");
            var connectionString = $"Data Source={filePath}; Version=3;";
            var connection = new SQLiteConnection(connectionString);

            return new SQLiteCommand(connection);
        }

        private void CreateDatabase()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gobi.sqlite");

            if (File.Exists(filePath) is false)
            {
                SQLiteConnection.CreateFile(filePath);
            }
        }

        private void CreateTable()
        {
            var command = CreateCommand();

            command.CommandText = """
                CREATE TABLE IF NOT EXISTS "TodoItem" 
                (
                    "Id" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                    "Description" TEXT NOT NULL,
                    "Completed" integer NOT NULL
                );
                """;

            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }


        public IEnumerable<TodoItem> GetAll()
        {
            var command = CreateCommand();
            command.CommandText = "SELECT * FROM TodoItem";

            command.Connection.Open();
            var reader = command.ExecuteReader();

            var items = new List<TodoItem>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var item = new TodoItem
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Completed = reader.GetBoolean(reader.GetOrdinal("Completed")),
                        Description = reader.GetString(reader.GetOrdinal("Description"))
                    };

                    items.Add(item);
                }

                reader.Close();
            }

            command.Connection.Close();

            return items;
        }

        public void Add(TodoItem todoItem)
        {
            var command = CreateCommand();
            command.CommandText = "INSERT INTO TodoItem (Description, Completed) VALUES (@Description, @Completed)";

            command.Parameters.AddWithValue("@Description", todoItem.Description);
            command.Parameters.AddWithValue("@Completed", todoItem.Completed);

            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void Put(TodoItem todoItem)
        {
            var command = CreateCommand();
            command.CommandText = "UPDATE TodoItem SET Description=@Description, Completed=@Completed WHERE Id=@Id";
            command.Parameters.AddWithValue("@Id", todoItem.Id);
            command.Parameters.AddWithValue("@Description", todoItem.Description);
            command.Parameters.AddWithValue("@Completed", todoItem.Completed);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public void Delete(TodoItem todoItem)
        {
            var command = CreateCommand();
            command.CommandText = "DELETE FROM TodoItem WHERE Id=@Id";
            command.Parameters.AddWithValue("@Id", todoItem.Id);

            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public TodoItem GetOne(int id)
        {
            var command = CreateCommand();
            command.CommandText = "SELECT * FROM TodoItem WHERE Id=@Id AND Completed=1";
            command.Parameters.AddWithValue("@Id", id);

            command.Connection.Open();
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var todoItem = new TodoItem
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    Completed = reader.GetBoolean(reader.GetOrdinal("Completed"))
                };
                return todoItem;
            }
            else
            {
                return null;
            }

            command.Connection.Close();
        }
    }
}
