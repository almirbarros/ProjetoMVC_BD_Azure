using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public enum EnumStatusTarefa
    {
        [Display(Name = "Fazer")]
        Fazer = 1,
        [Display(Name = "Fazendo")]
        Fazendo = 2,
        [Display(Name = "Feito")]
        Feito = 3,
    }
}
