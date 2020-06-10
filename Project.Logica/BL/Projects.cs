using System.Collections.Generic;
using System.Linq;
using System;

namespace ProjectCore.Logica.BL
{
    public class Projects
    {
        public List<Models.DB.Projects> GetProjects(int? id, int? tenantsId,
            string userId = null)
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();
            var listProjectsEF = (from _projects in _context.Projects
                                  select _projects).ToList();
            if (id != null)
                listProjectsEF = listProjectsEF.Where(x => x.Id == id).ToList();
            if (tenantsId != null)
                listProjectsEF = listProjectsEF.Where(x => x.TenantId == tenantsId).ToList();
            if (!string.IsNullOrEmpty(userId))
                listProjectsEF = (from _projects in listProjectsEF
                                  join _userProjects in _context.UserProjects on _projects.Id equals _userProjects.ProjectId
                                  where _userProjects.UserId.Equals(userId)
                                  select _projects).ToList();

            var listProjects = (from _projects in listProjectsEF
                                select new Models.DB.Projects
                                {
                                    Id = _projects.Id,
                                    Title = _projects.Title,
                                    Details = _projects.Details,
                                    ExpectedCompletionDate = _projects.ExpectedCompletionDate,
                                    TenantId = _projects.TenantId,
                                    CreatedAt = _projects.CreatedAt,
                                    UpdatedAt = _projects.UpdatedAt



                                }


                              ).ToList();
            return listProjects;
        }
        public void CreatedProjects(string title, string details, DateTime? expetedCompletionDate, int? tenantId)
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();
            _context.Projects.Add(new DAL.Models.Projects
            {
                Title = title,
                Details = details,
                ExpectedCompletionDate = expetedCompletionDate,
                TenantId = tenantId,
                CreatedAt = DateTime.Now
            });
            _context.SaveChangesAsync();

        }
        public void UpdateProjects(int id,
            string title,
            string details,
            DateTime? expectedCompletionDate
            )
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();
            var projectEF = _context.Projects.Where(x => x.Id == id).FirstOrDefault();

            projectEF.Title = title;
            projectEF.Details = details;
            projectEF.ExpectedCompletionDate = expectedCompletionDate;
            projectEF.UpdatedAt = DateTime.Now;



            _context.SaveChangesAsync();
        }

        public void DeleteProjects(int? id)
        {

            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();

            if (_context.Artifacts.Any(x => x.ProjectId == id) || _context.UserProjects.Any(x => x.ProjectId == id))
            {
                return;
            }
            var projectEF = _context.Projects.Where(x => x.Id == id).FirstOrDefault();

            _context.Projects.Remove(projectEF);

            _context.SaveChangesAsync();
        }
    }
}
