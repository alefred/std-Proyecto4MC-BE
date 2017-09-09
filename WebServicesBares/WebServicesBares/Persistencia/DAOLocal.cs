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
        //private string cadenaconexion = "Data Source=a1d03a2d-9c0f-4408-a40e-a6c90072de9e.sqlserver.sequelizer.com;Initial Catalog=dba1d03a2d9c0f4408a40ea6c90072de9e;User Id=tvmfwzevhnftvnla;Password=2Ccx6Hj4f3x55DK6Quii6SixnKvTrFciBYEozCMmQGhaFo3U5qeDwtdiuMkumxKF;";
        private string cadenaconexion = "Data Source=LAPTOP-C3204AHJ\\SQLEXPRESS;Initial Catalog=CFFLORESDB;Integrated Security=True";

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