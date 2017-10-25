using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NetCoreRazorPageApp.Pages.Employee
{
    public class DeletePageModel : PageModel
    {
        public readonly NetCoreRazorPageApp.Model.EmployeeDbContext _dbContext;

        public DeletePageModel(NetCoreRazorPageApp.Model.EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public NetCoreRazorPageApp.Model.Employee Employee { get; set; }

		//Method to retreive the selected record details
		public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
			//Get the value from database based on ID
			Employee = await _dbContext.Employee.FirstOrDefaultAsync(i => i.EmployeeID == id);

            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }

		//Method to Delete record from database
		public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
			//Delete Employee details to database 
			Employee = await _dbContext.Employee.FindAsync(id);

            if (Employee != null)
            {
                _dbContext.Employee.Remove(Employee);
                await _dbContext.SaveChangesAsync();
            }
			//Redirecting back to Index page after successfull Delete operation
			return RedirectToPage("/Index");
        }
    }
}