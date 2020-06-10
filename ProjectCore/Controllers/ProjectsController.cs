using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectCore.DAL.Models;
namespace ProjectCore.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;


        public ProjectsController(UserManager<IdentityUser> userManager)
        {

            _userManager = _userManager;
        }
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Logica.BL.Tenants tenants = new Logica.BL.Tenants();
            var tenant = tenants.GetTenants(user.Id).FirstOrDefault();



            Logica.BL.Projects projects = new Logica.BL.Projects();

            var result = await _userManager.IsInRoleAsync(user, "Admin") ?
            projects.GetProjects(null, tenant.Id) :
            projects.GetProjects(null, tenant.Id, user.Id);

            var listProjects = result.Select(x => new Logica.Models.ViewModel.ProjectsIndexViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Details = x.Details,
                CreatedAt = x.CreatedAt,
                ExpectedCompletionDate = x.ExpectedCompletionDate,
                UpdatedAt = x.UpdatedAt

            });
            listProjects = tenant.Plan.Equals("premiun") ?
                  listProjects :
                  listProjects.Take(1).ToList();


            return View(listProjects);
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(Logica.Models.BindingModels.ProjectsCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {

                IdentityUser user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                Logica.BL.Tenants tenants = new Logica.BL.Tenants();
                var tenant = tenants.GetTenants(user.Id).FirstOrDefault();

                Logica.BL.Projects projects = new Logica.BL.Projects();
                projects.CreatedProjects(model.Title, model.Details, model.ExpectedCompletionDate, tenant.Id);

                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Edt(int? id)
        {
            Logica.BL.Projects Projects = new Logica.BL.Projects();
            var project = Projects.GetProjects(id, null).FirstOrDefault();

            var projectBindingModels = new Logica.Models.BindingModels.ProjectsEditBindingModel
            {
                Id = project.Id,
                Details = project.Details,
                Title = project.Title,
                ExpectedCompletionDate = project.ExpectedCompletionDate

            };


            return View(projectBindingModels);
        }
        [HttpPost]
        public IActionResult Edit(Logica.Models.BindingModels.ProjectsEditBindingModel model)
        {
            if (ModelState.IsValid)
            {
                Logica.BL.Projects projects = new Logica.BL.Projects();

                projects.UpdateProjects(model.Id, model.Title,
                   model.Details,
                   model.ExpectedCompletionDate);

                return RedirectToAction("Index");
            }
            return View(model);

        }
        public IActionResult Details(int? id)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(id, null).FirstOrDefault();

            var projectviewModels = new Logica.Models.ViewModel.ProjectsDetailsViewModel
            {
                Id = project.Id,
                Details = project.Details,
                Title = project.Title,
                ExpectedCompletionDate = project.ExpectedCompletionDate,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            };

            return View(projectviewModels);
        }

        public IActionResult Delete(int? id)
        {
            Logica.BL.Projects projects = new Logica.BL.Projects();
            projects.DeleteProjects(id);

            return RedirectToAction("Index");
        }
    }
}