using Microsoft.AspNetCore.Mvc;
using nimapinfoteckTask.Models;
using System.Data.SqlClient;

namespace nimapinfoteckTask.Controllers
{
    public class ProductController : Controller
    {
        IConfiguration _config;
        string connectionString = null;

        public ProductController(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:taskdb"];
        }
        public IActionResult Index()
        {
            List<Product> Products = new List<Product>();

            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Product";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Product model = new Product()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        UnitPrice = (int)reader["UnitPrice"]


                    };
                    Products.Add(model);
                }
            }
            con.Close();
            return View(Products);
        }

        public IActionResult Details(int? id)
        {

            SqlConnection con = new SqlConnection(connectionString);
            string query = $"select * from Product where ProductId ={id}";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            Product model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new Product()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        UnitPrice = (int)reader["UnitPrice"]
                    };
                    break;
                }
            }
            con.Close();

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = $"insert into Product values ('{product.ProductName}',{product.UnitPrice},{product.CatId})";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            int records = cmd.ExecuteNonQuery();
            if (records > 0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]

        public IActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = $"select * from Product where ProductId = {id}";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            Product model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new Product()
                    {
                       ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        UnitPrice = (int)reader["UnitPrice"]
                    };
                    break;
                }
            }
            con.Close();

            return View(model);

        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = $"update Category set ProductName = '{model.ProductName}',UnitPrice ='{model.UnitPrice}' where ProductId = " + model.ProductId;

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            int records = cmd.ExecuteNonQuery();
            if (records > 0)
            {
                return RedirectToAction("Index");


            }
            return View();

        }
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult Delete_Get(int? id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = $"select * from Product where ProductId = {id}";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            Product model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new Product()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        UnitPrice = (int)reader["UnitPrice"],
                        CatId = (int)reader["CatId"]
                    };
                    break;
                }
            }
            con.Close();
            return View(model);


        }
        [HttpPost]
        [ActionName("Delete")]

        public IActionResult Delete_Confirmed(int? id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = $"delete from Product where ProductId ={id}";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            int records = cmd.ExecuteNonQuery();
            if (records > 0)
            {
                return RedirectToAction("Index");

            }
            con.Close();

            return View();
        }


    }
}
