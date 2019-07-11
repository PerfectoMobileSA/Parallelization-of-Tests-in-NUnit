using System;
using PerfectoTest.Utils;

namespace PerfectoTest.Attributes
{   
    public sealed class DevicePlatformExpectationAttribute : Attribute
    {
        public DevicePlatformExpectationAttribute(DevicePlatform platform = DevicePlatform.Android)
        {
            this.DeviceOS = platform.ToString();
        }
        public string DeviceOS { get; }
    }
}
