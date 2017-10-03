using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRest
{
    public class EOrder
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int pubId { get; set; }
        public DateTime orderDate { get; set; }
        public string status { get; set; }
        public string waitTime { get; set; }
        public string attentionTime { get; set; }
    }

    public class EOrderDetail
    {
        public int id { get; set; }
        public int orderId { get; set; }        
        public int productId { get; set; }
        public double unitPrice { get; set; }
        public int quantity { get; set; }
    }

    public class EProduct
    {
        public int id { get; set; }
        public EPub pub { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public double price { get; set; }
        public TimeSpan overallTime { get; set; }
    }

    public class EPub
    {
        public int id { get; set; }        
        public string name { get; set; }
        public string ruc { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class EUser
    {

        private string _fullname;
        public int id { get; set; }        
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string fullName { get { return string.Concat(lastName, ", ", firstName); } set { _fullname = value; } }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DateTime postDate { get; set; }
        public string documentNumber { get; set; }
        public string password { get; set; }
        public string type { get; set; }
    }

}
