using LogInMVC.Models;
using System.Data.SqlClient;

namespace LogInMVC.DAL
{

    public class UsuarioDAL
    {
        private string _connectionString = "Data Source=85.208.21.117,54321;Initial Catalog=AlbaUsersLogin;User ID=sa;Password=Sql#123456789;";
    
        public UsuarioDAL() { }

        public Usuario GetUsuarioLogin(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"SELECT * FROM  Usuario
                                                WHERE UserName  = @UserName AND Password = @Password", connection);
                command.Parameters.AddWithValue("UserName", username);
                command.Parameters.AddWithValue("Password", password);

                connection.Open();
                using (var reader = command.ExecuteReader()) 
                {
                    if (reader.Read()) 
                    {
                        return new Usuario
                        {
                            IdUsuario = (int)reader["IdUsuario"],
                            UserName = (string)reader["UserName"],
                            Password = (string)reader["Password"],
                            Apellido = (string)reader["Apellido"],
                            Email = (string)reader["Email"],
                            FechaNaciemiento = reader["FechaNacimiento"] as DateTime?,
                            Telefono = reader["Telefono"] as string,
                            Direccion = reader["Direccion"] as string,
                            Ciudad = reader["Ciudad"] as string,
                            FechaRegistro = (DateTime)reader["FechaRegistro"],
                            Activo = (bool)reader["Activo"]
                        };
                    }
                    return null;
                }
            }
        }
    
    }
}
