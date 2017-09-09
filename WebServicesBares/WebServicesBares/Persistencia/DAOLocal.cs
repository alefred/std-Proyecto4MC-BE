using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
    public class DAOLocal
    {
        private string cadenaconexion = "Data Source=azrdb03.database.windows.net;Initial Catalog=arsDB03;Persist Security Info=True;User ID=alefred;Password=Pa$$w0rd";

        public List<ELocal> Listar()
        {
            List<ELocal> lista = new List<ELocal>();

            string sql = "SELECT * FROM Local ";

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
                                ELocal ve = new ELocal();

                                ve.idLocal = Convert.ToInt32(dr[0]);
                                ve.nombreLocal = dr[1].ToString();
                                ve.ruc = dr[2].ToString();
                                ve.direccion = dr[3].ToString();
                                ve.telefono = dr[4].ToString();
                                ve.descripcion = dr[5].ToString();
                                ve.UbiLonguitud = dr[6].ToString();
                                ve.UbiLatitud = dr[7].ToString();
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