using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
   
    public class DAOProducto
    {
        //private string cadenaconexion = "Data Source=a1d03a2d-9c0f-4408-a40e-a6c90072de9e.sqlserver.sequelizer.com;Initial Catalog=dba1d03a2d9c0f4408a40ea6c90072de9e;User Id=tvmfwzevhnftvnla;Password=2Ccx6Hj4f3x55DK6Quii6SixnKvTrFciBYEozCMmQGhaFo3U5qeDwtdiuMkumxKF;";
        private string cadenaconexion = "Data Source=LAPTOP-C3204AHJ\\SQLEXPRESS;Initial Catalog=CFFLORESDB;Integrated Security=True";

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