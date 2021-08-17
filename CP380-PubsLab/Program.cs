using System;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            {
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connect");
                }

                // 1:Many practice
                //
                // TODO: - Loop through each employee
                //       - For each employee, list their job description (job_desc, in the jobs table)

                var employeeJob = dbcontext.Employees.Select(e => new { EmpId = e.emp_id, FName = e.fname, LName = e.lname ,Job_Desc = e.Jobs.job_desc }).ToList();

                Console.WriteLine("1) 1:Many practice\nFor each employee, list their job description");
                Console.WriteLine("\n#Employee ID | Employee Name | Job");
                foreach (var i in employeeJob)
                {
                    Console.WriteLine($"{i.EmpId} | {i.FName} {i.LName} | {i.Job_Desc}");
                }

                // TODO: - Loop through all of the jobs
                //       - For each job, list the employees (first name, last name) that have that job

                var jobEmployees = dbcontext.Jobs.Select(j => new { JobId = j.job_id, Job_Desc = j.job_desc, Employees = j.Employee.Select(e => new { FirstName = e.fname, LastName = e.lname }) }).ToList();

                Console.WriteLine("\n2)1:Many practice\nFor each job, list the employees (first name, last name) that have that job");
                Console.WriteLine("\n#Job ID | Job ");
                foreach (var job in jobEmployees)
                {
                    Console.WriteLine($"*** | {job.JobId} | {job.Job_Desc} | ***");
                    foreach (var employee in job.Employees)
                    {
                        Console.WriteLine($"  {employee.FirstName},{employee.LastName}");
                    }
                }

                // Many:many practice
                //
                // TODO: - Loop through each Store
                //       - For each store, list all the titles sold at that store
                //
                // e.g.
                //  Bookbeat -> The Gourmet Microwave, The Busy Executive's Database Guide, Cooking with Computers: Surreptitious Balance Sheets, But Is It User Friendly?
                
                var soldTitles = dbcontext.Stores.Select(s => new { Store = s.stor_name, Titles = s.Sales.Select(sl => sl.Titles.title) }).ToList();

                Console.WriteLine("\n3) Many:many practice\nFor each store, list all the titles sold at that store");
                Console.WriteLine("\n#Stores | Titles ");
                foreach (var store in soldTitles)
                {
                    var strJoin = String.Join(",",store.Titles);
                    Console.WriteLine($"\n{store.Store} -> {strJoin}");
                }

                // TODO: - Loop through each Title
                //       - For each title, list all the stores it was sold at
                //
                // e.g.
                //  The Gourmet Microwave -> Doc-U-Mat: Quality Laundry and Books, Bookbeat
                 var soldAtStores = dbcontext.Titles.Select(t => new { TitleName = t.title, StoreList = t.Sales.Select(sl => sl.Stores.stor_name) }).ToList();

                Console.WriteLine("\n4) Many:many practice\nFor each title, list all the stores it was sold at");
                Console.WriteLine("\n#Titles | Stores ");
                foreach (var title in soldAtStores)
                {
                    var strJoin = String.Join(",", title.StoreList);
                    Console.WriteLine($"\n{title.TitleName} -> {strJoin}");
                }
                Console.ReadLine();
            }
        }
    }
}
