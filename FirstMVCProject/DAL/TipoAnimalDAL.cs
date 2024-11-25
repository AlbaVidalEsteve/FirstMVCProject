using FirstMVCProject.Models;
using System.Data.SqlClient;

namespace FirstMVCProject.DAL
{
    public class TipoAnimalDAL
    {
        private string connectionString = "Data Source=85.208.21.117,54321;Initial Catalog=AlbaAnimales;User ID=sa;Password=Sql#123456789;";
        public List<TipoAnimal> GetAll()
        {
            List<TipoAnimal> tipos = new List<TipoAnimal>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM TipoAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipoAnimal tipo = new TipoAnimal
                    {
                        IdTipoAnimal = Convert.ToInt32(reader["IdTipoAnimal"]),
                        TipoDescripcion = reader["TipoDescripcion"].ToString()
                    };
                    tipos.Add(tipo);
                }
            }

            return tipos;
        }

        public TipoAnimal GetById(int id)
        {
            TipoAnimal tipo = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM TipoAnimal WHERE IdTipoAnimal = @IdTipoAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdTipoAnimal", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    tipo = new TipoAnimal
                    {
                        IdTipoAnimal = Convert.ToInt32(reader["IdTipoAnimal"]),
                        TipoDescripcion = reader["TipoDescripcion"].ToString()
                    };
                }
            }

            return tipo;
        }

        public void Insert(TipoAnimal tipoAnimal)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO TipoAnimal (TipoDescripcion) VALUES (@TipoDescripcion)";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@TipoDescripcion", tipoAnimal.TipoDescripcion);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(TipoAnimal tipoAnimal)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "UPDATE TipoAnimal SET TipoDescripcion = @TipoDescripcion WHERE IdTipoAnimal = @IdTipoAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdTipoAnimal", tipoAnimal.IdTipoAnimal);
                cmd.Parameters.AddWithValue("@TipoDescripcion", tipoAnimal.TipoDescripcion);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM TipoAnimal WHERE IdTipoAnimal = @IdTipoAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdTipoAnimal", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
