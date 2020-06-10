using System.Collections.Generic;
using System.Linq;

namespace ProjectCore.Logica.BL
{
    public class Tenants
    {
        public List<Models.DB.Tenants>GetTenants(string userId)
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();
            var listTenants=( from _Tenants in _context.Tenants
                            join _aspNetUser in _context.AspNetUsers on _Tenants.Id  equals _aspNetUser.TenantId
                            where _aspNetUser.Id.Equals(userId) 
                            select new Models.DB.Tenants
                            {
                                Id = _Tenants.Id,
                                Name = _Tenants.Name,
                                Plan = _Tenants.Plan,
                                CreatedAt = _Tenants.CreatedAt,
                                UpdatedAt = _Tenants.UpdatedAt
                            }).ToList();
            return listTenants;

        }
    }
}
