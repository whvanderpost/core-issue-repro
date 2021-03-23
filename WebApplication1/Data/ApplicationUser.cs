using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
  public class ApplicationUser : IdentityUser
  {
    public virtual string Culture { get; set; }

  }
}
