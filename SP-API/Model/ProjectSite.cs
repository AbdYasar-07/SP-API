using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SP_API.Model
{
    public class ProjectSite
    {
       // [Required(ErrorMessage = "Tittle is Required")]
        public string Title { get; set; }
      //  [Required(ErrorMessage = "ReferenceNumber is Required")]
        public string ProjectReferenceNumber  { get; set; }
        //[Required(ErrorMessage = "ProjectOwner is Required")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "E-Mail is not valid")]
        public string ProjectOwner{ get; set; }
        //[Required(ErrorMessage = "ProjectClosedDate is Required")]
        public string ProjectClosedDate { get; set; }
        //[Required(ErrorMessage = "Description is Required")]
        public string ProjectsDescription { get; set; }
    }

}
