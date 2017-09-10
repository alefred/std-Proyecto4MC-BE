﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
    public class DAOUsuario
    {
        private static string cadenaconexion = ConfigurationManager.ConnectionStrings["CnxBDAppBar"].ToString();

        public List<EUsuario> Listar(string busqueda)
        {
            List<EUsuario> lista = new List<EUsuario>();

            string sql = "SELECT * FROM Usuario where  IdUsuario = " + busqueda;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                EUsuario ve = new EUsuario();

                                ve.idUsuario = Convert.ToInt32(dr[0]);
                                ve.nombreCompleto = dr[1].ToString();
                                ve.email = dr[2].ToString();
                                ve.telefono = dr[3].ToString();
                                ve.fechaRegistro = Convert.ToDateTime(dr[4]);
                                ve.nroDocumento = dr[5].ToString();
                                ve.password = dr[6].ToString();
                                ve.tipoUsuario = dr[7].ToString();
                                lista.Add(ve);
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

            return lista;
        }


        public int Insertar(EUsuario eusuario)
        {

            string sql = "insert into Usuario ([nombreCompleto],[email],[telefono],[fechaRegistro],[nroDocumento],[password],[tipoUsuario])  values (@nombreCompleto, @email, @telefono, @fechaRegistro, @nroDocumento, @password, @tipoUsuario)";

            int idusuario = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        com.Parameters.Add(new SqlParameter("@nombreCompleto", eusuario.nombreCompleto));
                        com.Parameters.Add(new SqlParameter("@email", eusuario.email));
                        com.Parameters.Add(new SqlParameter("@telefono", eusuario.telefono));
                        com.Parameters.Add(new SqlParameter("@fechaRegistro", eusuario.fechaRegistro));
                        com.Parameters.Add(new SqlParameter("@nroDocumento", eusuario.nroDocumento));
                        com.Parameters.Add(new SqlParameter("@password", eusuario.password));
                        com.Parameters.Add(new SqlParameter("@tipoUsuario", eusuario.tipoUsuario));
                        com.ExecuteNonQuery();

                    }

                    using (SqlCommand com = new SqlCommand("select max(idventa) from Usuario", con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                idusuario = Convert.ToInt32(dr[0]);
                            }
                            dr.Close();
                        }

                    }

                }
                return idusuario;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}