using System;
using PerfectoTest.Utils;

namespace PerfectoTest.Attributes
{
    public sealed class DeviceExpectationAttribute : Attribute
    {
        public DeviceExpectationAttribute(string deviceId = Device.Device2)
        {
            this.DeviceName = deviceId;
        }
        public string DeviceName { get; }
    }
}
