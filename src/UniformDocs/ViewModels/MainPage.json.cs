using Starcounter;

namespace UniformDocs.ViewModels
{
    partial class MainPage : Json
    {
        public string AppVersion => Program.GetAppVersionFromAssemblyFile();
        public string StarcounterVersion => Starcounter.Internal.CurrentVersion.Version;
    }
}