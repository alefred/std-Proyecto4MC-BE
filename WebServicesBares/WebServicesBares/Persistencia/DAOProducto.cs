using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
   
    public class DAOProducto
    {
        private static string cadenaconexion = ConfigurationManager.ConnectionStrings["CnxBDAppBar"].ToString();

        public List<EProducto> Listar(string local)
        {
            List<EProducto> lista = new List<EProducto>();

            string sql = "SELECT * FROM Producto where  idLocal = " + local;

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
                                EProducto ve = new EProducto();

                                ve.idProducto = Convert.ToInt32(dr[0]);
                                ve.idLocal = Convert.ToInt32(dr[1]);
                                ve.nombreProducto = dr[2].ToString();
                                ve.descripcionProducto = dr[3].ToString();
                                ve.tipoProducto = dr[4].ToString();
                                ve.imagen = dr[5].ToString();
                                ve.precio = Convert.ToDouble(dr[6].ToString());
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
    }
}