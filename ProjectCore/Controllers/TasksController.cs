using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectCore.DAL.Models;
using ProjectCore.Logica.Models.BindingModels;

namespace ProjectCore.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index(int? projectId)
        {
            Logica.BL.Tasks tasks = new Logica.BL.Tasks();
            var listTasks = tasks.GetTasks(projectId, null);
            var listTasksviewModel = listTasks.Select(x => new Logica.Models.ViewModel.TasksIndexViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Details = x.Details,
                ExpirationDate = x.ExpirationDate,
                IsCompleted = x.IsCompleted,
                RemainingWork = x.RemainingWork,
                StateId = x.States.Name,
                ActivityId = x.Activities.Name,
                PriorityId = x.Priorities.Name

            });

            Logica.BL.Projects projects = new Logica.BL.Projects();
            var project = projects.GetProjects(projectId, null).FirstOrDefault();
            ViewBag.Project = project;


            return View(listTasksviewModel);
        }
        public IActionResult Create(int? projectId)
        {


            var tasksBindingModel = new Logica.Models.BindingModels.TasksCreateBindingModel
            {
                ProjectId = projectId

            };


            Logica.BL.Activitiescs activitiescs = new Logica.BL.Activitiescs();
            ViewBag.Activities = activitiescs.GetActivities();

            Logica.BL.Priorities priorities = new Logica.BL.Priorities();
            ViewBag.priorities = priorities.GetPriorities();


            Logica.BL.States state = new Logica.BL.States();
            ViewBag.States = state.GetStates();






            return View(tasksBindingModel);
        }
        [HttpPost]
        public IActionResult Create(Logica.Models.BindingModels.TasksCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {

                Logica.BL.Tasks tasks = new Logica.BL.Tasks();
                tasks.CreateTasks(model.Title,
                   model.Details,
                  model.ExperationDate,
                  model.IsCompleted,
                  model.Effort,
                  model.RemainingWork,
                  model.StateId,
                  model.ActivityId,
                  model.PriorityId,
                  model.ProjectId);

                return RedirectToAction("Index", new { projectId = model.ProjectId });
            }
            Logica.BL.Activitiescs activitiescs = new Logica.BL.Activitiescs();
            ViewBag.Activities = activitiescs.GetActivities();
            Logica.BL.Priorities priorities = new Logica.BL.Priorities();
            ViewBag.priorities = priorities.GetPriorities();
            Logica.BL.States state = new Logica.BL.States();
            ViewBag.States = state.GetStates();

            return View(model);
        }

    }
}