using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Categorias
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT IDCATEGORIA, DESCRIPCION, ACTIVO, FECHAREGISTRO FROM CATEGORIA";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Categoria
                            {
                                IdCategoria = Convert.ToInt32(dr["IDCATEGORIA"]),
                                Descripcion = Convert.ToString(dr["DESCRIPCION"]),
                                Activo = Convert.ToBoolean(dr["ACTIVO"])
                            }); ;
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Categoria>();
            }

            return lista;
        }


        public int Registrar(Categoria obj, out string Message)
        {
            int idGenerado = 0;
            Message = String.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "INSERT INTO CATEGORIA (DESCRIPCION, ACTIVO) VALUES (@descipcion, @activo); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@descipcion", obj.Descripcion);
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


        public bool Editar(Categoria obj, out string Message)
        {
            bool sucesso = false;
            Message = String.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "UPDATE CATEGORIA SET DESCRIPCION = @descripcion, ACTIVO = @activo WHERE IDCATEGORIA = @id";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@activo", obj.Activo);
                    cmd.Parameters.AddWithValue("@id", obj.IdCategoria);
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
                    string query = "DELETE FROM CATEGORIA WHERE IDCATEGORIA = @id";
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
