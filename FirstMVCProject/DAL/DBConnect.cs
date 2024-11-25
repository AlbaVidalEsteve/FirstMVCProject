using System.Data.SqlClient;
namespace FirstMVCProject.DAL
{          
    public class DBConnect
    {
        public SqlConnection connection;

        // Constructor que configura la cadena de conexión
        public DBConnect()
        {
            // Configuración de la cadena de conexión
            var connectionString = "Data Source=85.208.21.117,54321;Initial Catalog=IvanAnimales;User ID=sa;Password=Sql#123456789";
            connection = new SqlConnection(connectionString);
        }

        // Método para conectar a la base de datos
        public bool Connect()
        {
            try
            {
                if (!IsConnected())
                {
                    connection.Open();
                    return true; // Conexión exitosa
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar: " + ex.Message);
            }
            return false; // Fallo en la conexión
        }

        // Método para desconectar de la base de datos
        public void Disconnect()
        {
            if (IsConnected())
                connection.Close();
        }

        // Método para verificar el estado de la conexión
        public bool IsConnected()
        {
            return connection.State == System.Data.ConnectionState.Open;
        }

        // Método wrapper que maneja la conexión/desconexión
        public T ExecuteWithConnection<T>(Func<T> operation)
        {
            try
            {
                if (!IsConnected())
                    Connect();

                return operation();
            }
            finally
            {
                if (IsConnected())
                    Disconnect();
            }
        }

        // Sobrecarga para métodos que no devuelven valor
        public void ExecuteWithConnection(Action operation)
        {
            ExecuteWithConnection<object>(() =>
            {
                operation();
                return null;
            });
        }
    }
}
