using System;
using System.Configuration;
using System.Data.SqlClient;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
    public class DAOUsuario
    {
        private static string cadenaconexion = ConfigurationManager.ConnectionStrings["CnxBDAppBar"].ToString();

        public EUser Insertar(EUser eusuario)
        {
            EUser usuarioCreado = null;
            int idUsuario = 0;
            string sql;

            sql = " INSERT INTO [Users]([LastName],[FirstName], " +
                    " [Email],[PhoneNunber],[DocumentNumber],[Type],[Password],[PostDate]) " +
                    " values(@lastname, @firstname , @email, @phoneNumber, @documentNumber, " +
                    "@type, @password, Getdate()); Select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        com.Parameters.Add(new SqlParameter("@lastname", eusuario.lastName));
                        com.Parameters.Add(new SqlParameter("@firstname", eusuario.firstName));
                        com.Parameters.Add(new SqlParameter("@email", eusuario.email));
                        com.Parameters.Add(new SqlParameter("@phoneNumber", eusuario.phoneNumber));
                        com.Parameters.Add(new SqlParameter("@documentNumber", eusuario.documentNumber));
                        com.Parameters.Add(new SqlParameter("@type", eusuario.type));
                        com.Parameters.Add(new SqlParameter("@password", eusuario.password));
                        idUsuario  = Convert.ToInt32(com.ExecuteScalar());
                    }

                    usuarioCreado = GetUserById(idUsuario);

                }
                return usuarioCreado;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public EUser GetUserById(int id)
        {
            EUser usuario = null;

            string sql = "SELECT U.UserId, U.LastName, U.FirstName, U.Email, " +
                        " U.PhoneNunber, U.DocumentNumber, U.Type, U.PostDate " +
                        " FROM Users U where UserId = " + id;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        con.Open();
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                usuario = new EUser();

                                usuario.id = Convert.ToInt32(dr[0]);
                                usuario.lastName = dr[1].ToString();
                                usuario.firstName = dr[2].ToString();
                                usuario.email = dr[3].ToString();
                                usuario.phoneNumber = dr[4].ToString();
                                usuario.documentNumber = dr[5].ToString();
                                usuario.type = dr[6].ToString();
                                usuario.postDate = Convert.ToDateTime(dr[7]);
                            }

                            dr.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }

        #region "Login"

        public EUser Login(string usuario, string password, string tipo)
        {
            EUser usuarioLogeado = null;
            string sql = string.Empty;
            string sql1 = string.Empty;
            string getValue = string.Empty;
            int result = 0;

            sql = "select count(1) " +
            " from Users u where UPPER(u.DocumentNumber) = " + usuario.ToUpper() +
            " and UPPER(u.Password) = " + password +
            " and u.type = " + tipo;

            sql1 = "select u.UserId " +
            " from Users u where UPPER(u.DocumentNumber) = " + usuario.ToUpper() +
            " and UPPER(u.Password) = " + password +
            " and u.type = " + tipo;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        con.Open();
                        getValue = com.ExecuteScalar().ToString();

                        if (getValue != null)
                        {
                            result = Convert.ToInt32(getValue.ToString());
                        }
                    }
                }

                if (result == 1) //Se encontro registros
                {
                    using (SqlConnection con = new SqlConnection(cadenaconexion))
                    {
                        using (SqlCommand com = new SqlCommand(sql1, con))
                        {
                            con.Open();
                            getValue = com.ExecuteScalar().ToString();

                            if (getValue != null)
                            {
                                result = Convert.ToInt32(getValue.ToString());
                                usuarioLogeado = GetUserById(result);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuarioLogeado;
        }

        #endregion

    }
}