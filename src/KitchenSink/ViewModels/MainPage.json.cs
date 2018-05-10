using Starcounter;

namespace KitchenSink.ViewModels
{
    partial class MainPage : Json
    {
        public string AppVersion => Program.GetAppVersionFromAssemblyFile();
        public string StarcounterVersion => Starcounter.Internal.CurrentVersion.Version;
    }
}