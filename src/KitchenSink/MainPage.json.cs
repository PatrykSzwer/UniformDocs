using Starcounter;

namespace KitchenSink
{
    partial class MainPage : Json
    {
        public string StarcounterVersion => Program.GetAppVersionFromAssemblyFile();
        public string AppVersion => Starcounter.Internal.CurrentVersion.Version;
    }
}