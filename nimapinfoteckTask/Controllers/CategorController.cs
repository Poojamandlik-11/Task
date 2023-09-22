using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using nimapinfoteckTask.Models;
using System.Data.SqlClient;

namespace nimapinfoteckTask.Controllers
{
    public class CategorController : Controller
    {
        IConfiguration _config;
        string connectionString = null;

        public CategorController(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:taskdb"];
        }
        public IActionResult Index()
        {
            List<Category> categories = new List<Category>();

            SqlConnection con = new SqlConnection(connectionString);
            string query = "select * from Category";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    Category model = new Category()
                    {
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                    categories.Add(model);
                }
            }
            con.Close();



            return View(categories);

        }
        public IActionResult Details(int id)
        { 
            SqlConnection con = new SqlConnection(connectionString);
            string query = $"select * from Category where CategoryId ={id}";
             
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            Category model = null;
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    model = new Category()
                    {
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                    break;
                }
            }
            con.Close();

            return View(model);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(Category category)
        { 
            SqlConnection con = new SqlConnection (connectionString);
            string query = $"insert into Category values ('{category.CategoryName})";

            SqlCommand cmd = new SqlCommand (query, con);
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
            string query = $"select * from Category wher CtegoryId = {id}"; 

            SqlCommand cmd = new SqlCommand (query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            Category model = null;
            if (reader.HasRows)
            { 
                while (reader.Read()) 
                {
                    model = new Category()
                    {
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
                    };
                    break;
                }
            }
            con.Close();

            return View(model);

        }

        [HttpPost]

        public IActionResult Edit(Category model) 
        { 
            SqlConnection con = new SqlConnection( connectionString);
            string query = $"update Category set Name = '{model.CategoryName}' where CategoryId = " + model.CategoryId;

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
            string query = $"select * from Category wher CtegoryId = {id}";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            Category model = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    model = new Category()
                    {
                        CategoryId = (int)reader["CategoryId"],
                        CategoryName = reader["CategoryName"].ToString()
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
            SqlConnection con = new SqlConnection( connectionString);
            string query = $"delete from Category where CategoryId ={id}";

            SqlCommand cmd = new SqlCommand(query,con);
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
