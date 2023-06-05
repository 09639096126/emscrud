// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;



using webapidotnet6.Models;
namespace webapidotnet6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public PositionController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
        string query = @"
                    select PositionId, PositionName from dbo.Position";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myReader = myCommand.ExecuteReader();
                table.Load(myReader); ;

                myReader.Close();
                myCon.Close();
            }
        }

        return new JsonResult(table);
    }
    [HttpPost]
    public JsonResult Post(Position pos)
    {
        string query = @"
                    insert into dbo.Position values 
                    ('" + pos.PositionName + @"')
                    ";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myReader = myCommand.ExecuteReader();
                table.Load(myReader); ;

                myReader.Close();
                myCon.Close();
            }
        }

        return new JsonResult("Added Successfully");
    }


    [HttpPut]
    public JsonResult Put(Position pos)
    {
        string query = @"
                    update dbo.Position set 
                    PositionName = '" + pos.PositionName + @"'
                    where PositionId = " + pos.PositionId + @" 
                    ";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myReader = myCommand.ExecuteReader();
                table.Load(myReader); ;

                myReader.Close();
                myCon.Close();
            }
        }

        return new JsonResult("Updated Successfully");
    }


    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
        string query = @"
                    delete from dbo.Position
                    where PositionId = " + id + @" 
                    ";
        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myReader = myCommand.ExecuteReader();
                table.Load(myReader); ;

                myReader.Close();
                myCon.Close();
            }
        }

        return new JsonResult("Deleted Successfully");
    }
}


