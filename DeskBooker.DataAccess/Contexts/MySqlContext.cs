using System;
using System.Collections.Generic;
using DeskBooker.Core.Domain;
using Microsoft.EntityFrameworkCore;
// using MySql.Data.MySqlClient;

namespace DeskBooker.DataAccess.Contexts
{
    public class MySqlContext: DbContext
    {
        public string ConnectionString { get; set; }    
    
        public MySqlContext() : base()
        {
        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {    
        }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Department>().HasData(
            //     new Department { dept_no = 1, dept_name = "Dept 1" },
            //     new Department { dept_no = 2, dept_name = "Dept 2" }
            // );
        }

        // private MySqlConnection GetConnection()    
        // {    
        //     return new MySqlConnection(ConnectionString);    
        // } 

        // public List<Department> GetDepartments()  
        // {  
        //     List<Department> list = new List<Department>();  
  
        //     using (MySqlConnection conn = GetConnection())  
        //     {  
        //         conn.Open();  
        //         MySqlCommand cmd = new MySqlCommand("select * from departments", conn);  
        
        //         using (var reader = cmd.ExecuteReader())  
        //         {  
        //             while (reader.Read())  
        //             {  
        //                 list.Add(new Department()  
        //                 {
        //                     dept_no =  reader["dept_no"].ToString(),
        //                     dept_name = reader["dept_name"].ToString()
        //                 });  
        //             }  
        //         }  
        //     }  
        //     return list;  
        // } 
    }
}