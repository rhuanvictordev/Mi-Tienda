using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT IDUSUARIO, NOMBRES, APELLIDOS, CORREO, CLAVE, REESTABLECER, ACTIVO FROM USUARIO";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Usuario>();
            }

            return lista;
        }

        public int Registrar(Usuario obj, out string Message)
        {
            int idGenerado = 0;
            Message = String.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "INSERT INTO USUARIO (NOMBRES, APELLIDOS, CORREO, CLAVE, ACTIVO) VALUES (@nombres, @apellidos, @correo, @clave, @activo); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("@correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@clave", obj.Clave);
                    cmd.Parameters.AddWithValue("@activo", obj.Activo);
                    oconexion.Open();
                    object resultado = cmd.ExecuteScalar(); // 2. Usamos ExecuteScalar() para capturar o resultado do SELECT SCOPE_IDENTITY()
                    idGenerado = int.Parse(resultado.ToString());
                }
            }
            catch (Exception ex)
            {
                idGenerado = 0;
                Message = ex.Message;
            }

            return idGenerado;
        }

        public bool Editar(Usuario obj, out string Message)
        {
            bool sucesso = false;
            Message = String.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "UPDATE USUARIO SET NOMBRES = @nombres, APELLIDOS = @apellidos, CORREO = @correo, CLAVE = @clave, ACTIVO = @activo WHERE IdUsuario = @id";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("@correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@clave", obj.Clave);
                    cmd.Parameters.AddWithValue("@activo", obj.Activo);
                    cmd.Parameters.AddWithValue("@id", obj.IdUsuario);
                    oconexion.Open();
                    int afetado = cmd.ExecuteNonQuery();
                    if (afetado > 0)
                        sucesso = true;
                }
            }
            catch (Exception ex)
            {
                sucesso = false;
                Message = ex.Message;
            }

            return sucesso;
        }


        public bool Eliminar(int id, out string Message)
        {
            bool sucesso = false;
            Message = String.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "DELETE FROM USUARIO WHERE IdUsuario = @id";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    oconexion.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                        sucesso = true;
                }
            }
            catch (Exception ex)
            {
                sucesso = false;
                Message = ex.Message;
            }

            return sucesso;
        }

    }
}
