using FirstMVCProject.Models;
using FirstMVCProject.Models.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using System.Data.Common;
using System.Data.SqlClient;

namespace FirstMVCProject.DAL
{
    
    public class AnimalDAL
    {
        private string connectionString = "Data Source=85.208.21.117,54321;Initial Catalog=AlbaAnimales;User ID=sa;Password=Sql#123456789;";

        public AnimalDAL()
        {
            
        }

        public List<Animal> GetAll()
        {
            List<Animal> animals = new List<Animal>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"
                SELECT a.IdAnimal, a.NombreAnimal, a.Raza, a.RIdTipoAnimal, a.FechaNacimiento,
                       t.IdTipoAnimal, t.TipoDescripcion 
                FROM Animal a
                LEFT JOIN TipoAnimal t ON a.RIdTipoAnimal = t.IdTipoAnimal";
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Animal animal = new Animal
                        {
                            IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                            NombreAnimal = reader["NombreAnimal"].ToString(),
                            Raza = reader["Raza"]?.ToString(),
                            RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                            FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaNacimiento"]) : null,
                            TipoAnimal = new TipoAnimal
                            {
                                IdTipoAnimal = Convert.ToInt32(reader["IdTipoAnimal"]),
                                TipoDescripcion = reader["TipoDescripcion"].ToString()
                            }
                        };
                        animals.Add(animal);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener la lista de animales.", ex);
            }

            return animals;
        }

        public Animal GetById(int id)
        {
            Animal animal = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                    SELECT a.IdAnimal, a.NombreAnimal, a.Raza, a.RIdTipoAnimal, a.FechaNacimiento, 
                           t.IdTipoAnimal, t.TipoDescripcion 
                    FROM Animal a
                    LEFT JOIN TipoAnimal t ON a.RIdTipoAnimal = t.IdTipoAnimal
                    WHERE a.IdAnimal = @IdAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdAnimal", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    animal = new Animal
                    {
                        IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                        NombreAnimal = reader["NombreAnimal"].ToString(),
                        Raza = reader["Raza"].ToString(),
                        RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                        FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["FechaNacimiento"]) : null,
                        TipoAnimal = new TipoAnimal
                        {
                            IdTipoAnimal = Convert.ToInt32(reader["IdTipoAnimal"]),
                            TipoDescripcion = reader["TipoDescripcion"].ToString()
                        }
                    };
                }
            }

            return animal;
        }

        public void Insert(Animal animal)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                    INSERT INTO Animal (NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento) 
                    VALUES (@NombreAnimal, @Raza, @RIdTipoAnimal, @FechaNacimiento)";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                cmd.Parameters.AddWithValue("@Raza", string.IsNullOrEmpty(animal.Raza) ? (object)DBNull.Value : animal.Raza);
                cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento.HasValue ? (object)animal.FechaNacimiento.Value : DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Animal animal)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
                    UPDATE Animal 
                    SET NombreAnimal = @NombreAnimal, 
                        Raza = @Raza, 
                        RIdTipoAnimal = @RIdTipoAnimal, 
                        FechaNacimiento = @FechaNacimiento 
                    WHERE IdAnimal = @IdAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                cmd.Parameters.AddWithValue("@Raza", animal.Raza);
                cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento.HasValue ? (object)animal.FechaNacimiento.Value : DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdAnimal", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
