#nullable disable
using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class PostModel : RecordBase
    {
        #region Entity Properties


        [Required]
        [StringLength(40)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }

        #endregion

        #region Extra Properties
        [DisplayName("Post")]
        public int PostOutput { get; set; }


        [DisplayName("Posted On")]
        public string PostedOnOutput { get; set; }


        #endregion


    }
}
