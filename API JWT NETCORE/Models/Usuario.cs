using API_JWT_NETCORE.Helpers;
using API_JWT_NETCORE.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_JWT_NETCORE.Models
{
    public class Usuario : Conexion
    {
        public int Id { get; set; }
        public Rol IdRol { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public bool Activo { get; set; }

        public static async Task<Usuario> Login(LoginRequest usuario)
        {
            Usuario u = null;
            try
            {
                string query = @"SELECT U.*, R.Nombre AS NombreRol, R.Codigo AS CodigoRol, R.Activo AS ActivoRol FROM [dbo].[Usuario] U
	                                INNER JOIN  [dbo].[Rol] R ON R.Id = U.IdRol
                                WHERE U.Correo = @Correo AND U.Activo = 1 AND R.Activo = 1";
                using (SqlConnection con = Conectar()) 
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        command.Parameters.AddWithValue("@Correo", usuario.Correo);
                        con.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            var i = new
                            {
                                Id = reader.GetOrdinal("Id"),
                                Rol = new
                                {
                                    Id = reader.GetOrdinal("IdRol"),
                                    Nombre = reader.GetOrdinal("NombreRol"),
                                    Codigo = reader.GetOrdinal("CodigoRol"),
                                    Activo = reader.GetOrdinal("ActivoRol")
                                },
                                Nombre = reader.GetOrdinal("Nombre"),
                                ApellidoPaterno = reader.GetOrdinal("ApellidoPaterno"),
                                ApellidoMaterno = reader.GetOrdinal("ApellidoMaterno"),
                                Correo = reader.GetOrdinal("Correo"),
                                Password = reader.GetOrdinal("Password"),
                                Activo = reader.GetOrdinal("Activo")
                            };

                            if (reader.Read())
                            {
                                u = new Usuario();
                                u.Id = reader.GetValor<int>(i.Id);
                                u.Nombre = reader.GetValor<string>(i.Nombre);
                                u.ApellidoPaterno = reader.GetValor<string>(i.ApellidoPaterno);
                                u.ApellidoMaterno = reader.GetValor<string>(i.ApellidoMaterno);
                                u.Correo = reader.GetValor<string>(i.Correo);
                                u.Password = reader.GetValor<string>(i.Password);
                                u.Activo = reader.GetValor<bool>(i.Activo);
                                u.IdRol = new Rol()
                                {
                                    Id = reader.GetValor<int>(i.Rol.Id),
                                    Nombre = reader.GetValor<string>(i.Rol.Nombre),
                                    Codigo = reader.GetValor<int>(i.Rol.Codigo),
                                    Activo = reader.GetValor<bool>(i.Rol.Activo)
                                };
                            }
                        }
                        con.Close();
                    }
                }

                if (u == null) throw new Exception("Es posible que el usuario se encuentre desactivado");

                if (!MD5Helper.Comparar(u.Password, usuario.Password)) throw new Exception("Correo o contraseña inválidos");

                u.Password = "";
            }
            catch (Exception ex)
            {
                u = null;
                throw new Exception(ex.Message);
            }

            return u;
        }

        public static async Task<Usuario> ObtenerPorId(int id)
        {
            Usuario u = null;
            try
            {
                string query = @"SELECT U.*, R.Nombre AS NombreRol, R.Codigo AS CodigoRol, R.Activo AS ActivoRol FROM [dbo].[Usuario] U
	                                INNER JOIN  [dbo].[Rol] R ON R.Id = U.IdRol
                                WHERE U.Id = @ID";
                using (SqlConnection con = Conectar())
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        con.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            var i = new
                            {
                                Id = reader.GetOrdinal("Id"),
                                Rol = new
                                {
                                    Id = reader.GetOrdinal("IdRol"),
                                    Nombre = reader.GetOrdinal("NombreRol"),
                                    Codigo = reader.GetOrdinal("CodigoRol"),
                                    Activo = reader.GetOrdinal("ActivoRol")
                                },
                                Nombre = reader.GetOrdinal("Nombre"),
                                ApellidoPaterno = reader.GetOrdinal("ApellidoPaterno"),
                                ApellidoMaterno = reader.GetOrdinal("ApellidoMaterno"),
                                Correo = reader.GetOrdinal("Correo"),
                                Password = reader.GetOrdinal("Password"),
                                Activo = reader.GetOrdinal("Activo")
                            };

                            if (reader.Read())
                            {
                                u = new Usuario();
                                u.Id = reader.GetValor<int>(i.Id);
                                u.Nombre = reader.GetValor<string>(i.Nombre);
                                u.ApellidoPaterno = reader.GetValor<string>(i.ApellidoPaterno);
                                u.ApellidoMaterno = reader.GetValor<string>(i.ApellidoMaterno);
                                u.Correo = reader.GetValor<string>(i.Correo);
                                //u.Password = reader.GetValor<string>(i.Password);
                                u.Activo = reader.GetValor<bool>(i.Activo);
                                u.IdRol = new Rol()
                                {
                                    Id = reader.GetValor<int>(i.Rol.Id),
                                    Nombre = reader.GetValor<string>(i.Rol.Nombre),
                                    Codigo = reader.GetValor<int>(i.Rol.Codigo),
                                    Activo = reader.GetValor<bool>(i.Rol.Activo)
                                };
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return u;
        }

        public static async Task<List<Usuario>> ObtenerTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                string query = @"SELECT U.*, R.Nombre AS NombreRol, R.Codigo AS CodigoRol, R.Activo AS ActivoRol FROM [dbo].[Usuario] U
	                                INNER JOIN  [dbo].[Rol] R ON R.Id = U.IdRol";
                using (SqlConnection con = Conectar())
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        con.Open();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            var i = new
                            {
                                Id = reader.GetOrdinal("Id"),
                                Rol = new
                                {
                                    Id = reader.GetOrdinal("IdRol"),
                                    Nombre = reader.GetOrdinal("NombreRol"),
                                    Codigo = reader.GetOrdinal("CodigoRol"),
                                    Activo = reader.GetOrdinal("ActivoRol")
                                },
                                Nombre = reader.GetOrdinal("Nombre"),
                                ApellidoPaterno = reader.GetOrdinal("ApellidoPaterno"),
                                ApellidoMaterno = reader.GetOrdinal("ApellidoMaterno"),
                                Correo = reader.GetOrdinal("Correo"),
                                Password = reader.GetOrdinal("Password"),
                                Activo = reader.GetOrdinal("Activo")
                            };

                            while (reader.Read())
                            {
                                Usuario u = new Usuario();
                                u.Id = reader.GetValor<int>(i.Id);
                                u.Nombre = reader.GetValor<string>(i.Nombre);
                                u.ApellidoPaterno = reader.GetValor<string>(i.ApellidoPaterno);
                                u.ApellidoMaterno = reader.GetValor<string>(i.ApellidoMaterno);
                                u.Correo = reader.GetValor<string>(i.Correo);
                                //u.Password = reader.GetValor<string>(i.Password);
                                u.Activo = reader.GetValor<bool>(i.Activo);
                                u.IdRol = new Rol()
                                {
                                    Id = reader.GetValor<int>(i.Rol.Id),
                                    Nombre = reader.GetValor<string>(i.Rol.Nombre),
                                    Codigo = reader.GetValor<int>(i.Rol.Codigo),
                                    Activo = reader.GetValor<bool>(i.Rol.Activo)
                                };

                                usuarios.Add(u);
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return usuarios;
        }

        public async Task Guardar()
        {
            try
            {
                string pass = MD5Helper.CalcularHash(Password);
                string query = @"INSERT INTO [dbo].[Usuario] VALUES(@Rol, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Correo, @Password, @Activo)";
                using (SqlConnection con = Conectar())
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        command.Parameters.AddWithValue("@Rol", IdRol.Id);
                        command.Parameters.AddWithValue("@Nombre", Nombre);
                        command.Parameters.AddWithValue("@ApellidoPaterno", ApellidoPaterno.ToDB());
                        command.Parameters.AddWithValue("@ApellidoMaterno", ApellidoMaterno.ToDB());
                        command.Parameters.AddWithValue("@Correo", Correo);
                        command.Parameters.AddWithValue("@Password", pass);
                        command.Parameters.AddWithValue("@Activo", true);
                        con.Open();
                        await command.ExecuteNonQueryAsync();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Desactivar()
        {
            int result = 0;
            try
            {
                string query = @"UPDATE [dbo].[Usuario] SET Activo = @Activo WHERE ID = @ID";
                using (SqlConnection con = Conectar())
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        command.Parameters.AddWithValue("@ID", Id);
                        command.Parameters.AddWithValue("@Activo", false);
                        con.Open();
                        result = await command.ExecuteNonQueryAsync();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task Actualizar()
        {
            try
            {
                string query = @"UPDATE [dbo].[Usuario] 
                                SET 
                                    IdRol = @Rol, 
                                    Nombre = @Nombre, 
                                    ApellidoPaterno = @ApellidoPaterno, 
                                    ApellidoMaterno = @ApellidoMaterno,
                                    Correo = @Correo
                                WHERE ID = @ID";
                using (SqlConnection con = Conectar())
                {
                    using (SqlCommand command = new SqlCommand(query, con)
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 60
                    })
                    {
                        command.Parameters.AddWithValue("@ID", Id);
                        command.Parameters.AddWithValue("@Rol", IdRol.Id);
                        command.Parameters.AddWithValue("@Nombre", Nombre);
                        command.Parameters.AddWithValue("@ApellidoPaterno", ApellidoPaterno.ToDB());
                        command.Parameters.AddWithValue("@ApellidoMaterno", ApellidoMaterno.ToDB());
                        command.Parameters.AddWithValue("@Correo", Correo);
                        con.Open();
                        await command.ExecuteNonQueryAsync();                        
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
