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
                            UserName = reader["UserName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Apellido = reader["Apellido"] != DBNull.Value ? reader["Apellido"].ToString() : null,
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                            FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null,
                            Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : null,
                            Direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : null,
                            Ciudad = reader["Ciudad"] != DBNull.Value ? reader["Ciudad"].ToString() : null,
                            Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : null,
                            CodigoPostal = reader["CodigoPostal"] != DBNull.Value ? reader["CodigoPostal"].ToString() : null,
                            FechaRegistro = reader["FechaRegistro"] != DBNull.Value ? Convert.ToDateTime(reader["FechaRegistro"]) : DateTime.UtcNow 
                        };
                    }
                    return null;
                }
            }
        }

        public void CreateUsuario(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sqlQuery = @"
            INSERT INTO Usuario (UserName, [Password], Apellido, Email, FechaNacimiento, Telefono, Direccion, Ciudad, Estado, CodigoPostal) 
            VALUES (@UserName, @Password, @Apellido, @Email, @FechaNacimiento, @Telefono, @Direccion, @Ciudad, @Estado, @CodigoPostal)";

                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                cmd.Parameters.AddWithValue("@Password", usuario.Password); // Asegúrate de encriptar la contraseña
                cmd.Parameters.AddWithValue("@Apellido", string.IsNullOrEmpty(usuario.Apellido) ? (object)DBNull.Value : usuario.Apellido);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(usuario.Email) ? (object)DBNull.Value : usuario.Email);
                cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento.HasValue ? (object)usuario.FechaNacimiento.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(usuario.Telefono) ? (object)DBNull.Value : usuario.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", string.IsNullOrEmpty(usuario.Direccion) ? (object)DBNull.Value : usuario.Direccion);
                cmd.Parameters.AddWithValue("@Ciudad", string.IsNullOrEmpty(usuario.Ciudad) ? (object)DBNull.Value : usuario.Ciudad);
                cmd.Parameters.AddWithValue("@Estado", string.IsNullOrEmpty(usuario.Estado) ? (object)DBNull.Value : usuario.Estado);
                cmd.Parameters.AddWithValue("@CodigoPostal", string.IsNullOrEmpty(usuario.CodigoPostal) ? (object)DBNull.Value : usuario.CodigoPostal);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }



    }
}
