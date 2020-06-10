using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectCore.Logica.Models.BindingModels
{
    public  class ProjectsCreateBindingModel
    {
        [Required(ErrorMessage  = " The field Title is required")]
        [Display (Name="Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = " The field Details is required")]
        [Display(Name = "Details")]
        public string Details { get; set; }

        [Required(ErrorMessage = " The field Expcted Completation Date is required")]
        [Display(Name = "ExpectedCompletationDate")]
        public DateTime ExpectedCompletionDate{ get; set; }


    }
    public class ProjectsEditBindingModel
    {
        [Required(ErrorMessage = " The field Id is required")]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Required(ErrorMessage = " The field Title is required")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = " The field Details is required")]
        [Display(Name = "Details")]
        public string Details { get; set; }

        [Required(ErrorMessage = " The field ExpectedCompletionDate is required")]
        [Display(Name = "ExpectedCompletionDate")]

        public DateTime? ExpectedCompletionDate { get; set; }



    }

}
