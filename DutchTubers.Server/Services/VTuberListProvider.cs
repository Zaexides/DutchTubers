namespace DutchTubers.Server.Services
{
    public class VTuberListProvider : IVTuberListProvider
    {
        //TODO should be fetched from persistent layer
        private static string[] VTuberNames = new[]
        {
            "moonmeadow",
            "touma_tengu",
            "projectuglybastard",
            "ninaninin",
            "hinagikunezuchi",
            "mangobtuber",
            "vtubereloiyse",
            "vtubercypress"
        };

        public List<string> GetVTuberList()
        {
            return VTuberNames.ToList();
        }
    }
}
