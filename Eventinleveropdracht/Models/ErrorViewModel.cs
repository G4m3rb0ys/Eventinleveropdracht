using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventinleveropdracht.Models
{
    public class ErrorViewModel
    {
        [Key]
        public string? RequestId { get; set; }

        [Required]
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
