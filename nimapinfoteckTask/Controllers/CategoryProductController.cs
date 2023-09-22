using Microsoft.AspNetCore.Mvc;
using nimapinfoteckTask.Models;
using System.Data.SqlClient;

namespace nimapinfoteckTask.Controllers
{
    public class CategoryProductController : Controller
    {
        IConfiguration _config;
        string connectionString = null;

        public CategoryProductController(IConfiguration config)
        {
            _config = config;
            connectionString = _config["ConnectionStrings:taskdb"];
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber =(page ?? 1);
            List<ProductCategory> List = new List<ProductCategory>();

            SqlConnection con = new SqlConnection(connectionString);
            string query = " SELECT P.ProductId,p.ProductName,c.CategoryId,c.CategoryName\r\n from Product as p\r\n Inner Join Category as c on p.CatId = c.CategoryId";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ProductCategory model = new ProductCategory()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                       CategoryId = (int)reader["CategoryId"],
                       CategoryName = reader["CategoryName"].ToString()
                       
                    };
                    List.Add(model);
                }
            }
            con.Close();

            int totalItems = List.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pageItems = List.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(List);
        }
    }
}
