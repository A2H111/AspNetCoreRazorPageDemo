using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NetCoreRazorPageApp.Pages
{
    public class EditPageModel : PageModel
    {
		//Creating dbContext object
        public readonly NetCoreRazorPageApp.Model.EmployeeDbContext _dbContext;

        public EditPageModel(NetCoreRazorPageApp.Model.EmployeeDbContext dbContext)
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
		//Method to save the data back to database
		public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //Updating modified Employee details to database 
                _dbContext.Attach(Employee).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
			//Redirecting back to Index page after successfull save
            return RedirectToPage("./Index");

        }
    }
}