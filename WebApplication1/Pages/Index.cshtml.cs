using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Pages
{
  public class IndexModel : PageModel
  {
    public List<Device> Devices = new();

    public IndexModel(IStringLocalizer<Device> localizer)
    {
      Devices.Add(new Device(localizer) { DeviceState = DeviceStates.Missing });
      Devices.Add(new Device(localizer) { DeviceState = DeviceStates.LampFailure });
      Devices.Add(new Device(localizer) { DeviceState = DeviceStates.LampFailure | DeviceStates.Missing });
    }

    public void OnGet()
    {

    }
  }
}
