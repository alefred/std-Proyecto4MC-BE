using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
    public class DAOLocal
    {
        private static string cadenaconexion = ConfigurationManager.ConnectionStrings["CnxBDAppBar"].ToString();

        public List<EPub> Listar(string name)
        {
            List<EPub> lista = new List<EPub>();

            string sql = "SELECT P.PubId,P.Name,P.Description,P.Ruc,P.Address,P.PhoneNumber,P.Email,P.Latitude,P.Longitude " +
                        "FROM Pubs P Where((@name = '') OR(UPPER(P.Name) like '%' + UPPER(@name))) ";

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        com.Parameters.Add(new SqlParameter("@name", name));

                        con.Open();
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                EPub ve = new EPub();

                                ve.id = Convert.ToInt32(dr[0]);
                                ve.name = dr[1].ToString();
                                ve.description = dr[2].ToString();
                                ve.ruc = dr[3].ToString();
                                ve.address = dr[4].ToString();
                                ve.phoneNumber = dr[5].ToString();
                                ve.email = dr[6].ToString();
                                ve.latitude = dr[7].ToString();
                                ve.longitude = dr[8].ToString();
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