using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProjectCore.Logica.BL
{
  public  class States
    {
        public List<Models.DB.States> GetStates()
        {
            DAL.Models.ProjectCoreContext _context = new DAL.Models.ProjectCoreContext();

            var listStates = (from _state in _context.States
                                  where _state.Active == true
                                  select new Models.DB.States
                                  {
                                      Id = _state.Id,
                                      Name = _state.Name,
                                      Active = _state.Active

                                  }
                                ).ToList();
            return listStates;
        }

    }
}
