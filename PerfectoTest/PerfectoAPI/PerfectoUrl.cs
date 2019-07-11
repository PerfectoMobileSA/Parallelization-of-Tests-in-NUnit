namespace PerfectoTest.PerfectoAPI
{
    internal class PerfectoUrl
    {
        //Enter your cloud name here e.g. https://demo.perfectomobile.com and add /services/handsets/:
        public const string PrivateCloud = "https://demo.perfectomobile.com/services/handsets/";
        public const string DeviceInfo = "?operation=info&responseFormat=json&securityToken=" + Access.SecurityToken;
    }
}
