using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
  public class Device
  {
    private readonly IStringLocalizer<Device> _localizer;

    public Device(IStringLocalizer<Device> localizer)
    {
      _localizer = localizer;
    }

    private DeviceStates _deviceState;

    [Display(Name = "Device State Since")]
    public DateTime? DeviceStateSince { get; set; }

    [Display(Name = "Device state")]
    public string DeviceStateDisplay
    {
      get
      {
        if (Convert.ToInt64(DeviceState) == 0)
          return _localizer[GetDisplayName(DeviceState)];

        var result = new List<string>();

        foreach (Enum value in Enum.GetValues(typeof(DeviceStates)))
        {
          if (Convert.ToInt64(value) > 0 && DeviceState.HasFlag(value))
          {
            result.Add(_localizer[GetDisplayName(value)]);
          }
        }

        return string.Join(", ", result);
      }
    }

    private static string GetDisplayName(Enum enumValue)
    {
      return enumValue.GetType()?
                      .GetMember(enumValue.ToString())?
                      .First()?
                      .GetCustomAttribute<DisplayAttribute>()?
                      .Name;
    }
    
    public DeviceStates DeviceState
    {
      get
      {
        return _deviceState;
      }
      set
      { //only update on devicestate changes 
        //TODO Thingk about this, if this is ok to only update when a state changes to OK ,Missing or Lampfailure; state changes to overtemp etc. are discarded
        if (value != _deviceState && (value.HasFlag(DeviceStates.Ok) || !IsDeviceStateOk()))
        {
          DeviceStateSince = DateTime.UtcNow;
        }
        _deviceState = value;
      }
    }

    public virtual bool IsDeviceStateOk()
    {
      return !DeviceState.HasFlag(DeviceStates.Disabled) &&
        !DeviceState.HasFlag(DeviceStates.Missing) &&
        !DeviceState.HasFlag(DeviceStates.LampFailure) &&
        !DeviceState.HasFlag(DeviceStates.Faulty);
    }
  }
}
