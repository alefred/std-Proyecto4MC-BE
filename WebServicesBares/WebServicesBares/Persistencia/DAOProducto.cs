using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
   
    public class DAOProducto
    {
        private static string cadenaconexion = ConfigurationManager.ConnectionStrings["CnxBDAppBar"].ToString();

        public List<EProduct> Listar(int local, string tipo)
        {
            List<EProduct> lista = new List<EProduct>();

            string sql = "SELECT PR.ProductId, PR.Name, PR.Description,PR.Price, PR.type, PR.Image, PR.OverallTime, P.PubId, P.Name as Local " +
                " FROM Products PR INNER JOIN Pubs P ON PR.PubId = P.PubId " +
                " Where P.PubId = @pubId AND type = @type ";

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        com.Parameters.Add(new SqlParameter("@pubId", local));
                        com.Parameters.Add(new SqlParameter("@type", tipo));

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                EProduct ve = new EProduct();

                                ve.id = Convert.ToInt32(dr[0]);
                                ve.name = dr[1].ToString();
                                ve.description = dr[2].ToString();
                                ve.price = Convert.ToDouble(dr[3].ToString());
                                ve.type = dr[4].ToString();
                                ve.image = dr[5].ToString();
                                ve.overallTime = dr.GetTimeSpan(6);

                                EPub oPub = new EPub() {
                                    id = Convert.ToInt32(dr[7]),
                                    name = dr[8].ToString()
                                };

                                ve.pub = oPub;
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


        public List<EProduct> ListarID(int idproducto)
        {
            List<EProduct> lista = new List<EProduct>();

            string sql = "SELECT PR.ProductId, PR.Name, PR.Description,PR.Price, PR.type, PR.Image, PR.OverallTime, P.PubId, P.Name as Local " +
                " FROM Products PR INNER JOIN Pubs P ON PR.PubId = P.PubId " +
                " Where PR.ProductId = @idproducto ";

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        com.Parameters.Add(new SqlParameter("@idproducto", idproducto));

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                EProduct ve = new EProduct();

                                ve.id = Convert.ToInt32(dr[0]);
                                ve.name = dr[1].ToString();
                                ve.description = dr[2].ToString();
                                ve.price = Convert.ToDouble(dr[3].ToString());
                                ve.type = dr[4].ToString();
                                ve.image = dr[5].ToString();
                                ve.overallTime = dr.GetTimeSpan(6);

                                EPub oPub = new EPub()
                                {
                                    id = Convert.ToInt32(dr[7]),
                                    name = dr[8].ToString()
                                };

                                ve.pub = oPub;
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