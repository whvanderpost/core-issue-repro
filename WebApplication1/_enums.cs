using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
  [Flags]
  public enum DeviceStates
  {
    [Display(Name = "Ok")]
    Ok = 0,
    [Display(Name = "Disabled")]
    Disabled = 1,
    [Display(Name = "Lamp Failure")]
    LampFailure = 2,
    [Display(Name = "Missing")]
    Missing = 4,
    [Display(Name = "Faulty")]
    Faulty = 8,
    [Display(Name = "Refreshing")]
    Refreshing = 16,
    [Display(Name = "Needs Refreshing")]
    NeedsRefreshing = 32,
    [Display(Name = "Unexpected")]
    Unexpected = 64,
    [Display(Name = "Unsynchronised")]
    Unsynchronised = 128,
    [Display(Name = "EM Resting")]
    EM_Resting = 256,
    [Display(Name = "EM Reserved")]
    EM_Reserved = 512,
    [Display(Name = "EM In Emergency")]
    EM_InEmergency = 1024,
    [Display(Name = "EM In Prolong")]
    EM_InProlong = 2048,
    [Display(Name = "EM FT In Progress")]
    EM_FTInProgress = 4096,
    [Display(Name = "EM DT In Progress")]
    EM_DTInProgress = 8192,
    [Display(Name = "EM Hard Resting")]
    EM_HardResting = 16384,
    [Display(Name = "EM Hard Mains")]
    EM_HardMains = 32768,
    [Display(Name = "EM DT Pending")]
    EM_DTPending = 65536,
    [Display(Name = "EM FT Pending")]
    EM_FTPending = 131072,
    [Display(Name = "EM Battery Fail")]
    EM_BatteryFail = 262144,
    [Display(Name = "Test Mode")]
    TestMode = 524288,
    [Display(Name = "I2C Problems")]
    I2CProblems = 1048576,
    [Display(Name = "EM Inhibit")]
    EM_Inhibit = 2097152,
    [Display(Name = "EM FT Requested")]
    EM_FTRequested = 4194304,
    [Display(Name = "EM DT Requested")]
    EM_DTRequested = 8388608,
    [Display(Name = "EM Uknown")]
    EM_Unknown = 16777216,
    [Display(Name = "Non Compliant")]
    NonCompliant = 33554432,
    [Display(Name = "Partially Supported")]
    PartiallySupported = 67108864,
    [Display(Name = "No Fascia")]
    NoFascia = 134217728,
    [Display(Name = "Too Many Devices")]
    TooManyDevices = 268435456,
    [Display(Name = "Unused 4")]
    Unused4 = 536870912,
    [Display(Name = "Virtual")]
    Virtual = 1073741824
  }
}