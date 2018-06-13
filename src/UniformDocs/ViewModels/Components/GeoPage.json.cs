using System.Linq;
using UniformDocs.Database;
using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class GeoPage : Json
    {
        //Stockholm coordinates
        public readonly double DefaultLatitude = 59.3319913;
        public readonly double DefaultLongitude = 18.0765409;

        public void Init()
        {
            Position.Data = Db.SQL<MapCoordinates>("SELECT gp FROM UniformDocs.Database.MapCoordinates gp").FirstOrDefault()
                            ?? new MapCoordinates
                            {
                                Latitude = DefaultLatitude,
                                Longitude = DefaultLongitude
                            };
        }
    }

    [GeoPage_json.Position]
    partial class GeoPagePosition : Json, IBound<MapCoordinates>
    {
        static GeoPagePosition()
        {
            DefaultTemplate.Latitude.InstanceType = typeof(double);
            DefaultTemplate.Longitude.InstanceType = typeof(double);
        }

        public void Handle(Input.ResetTrigger action)
        {
            var geoPageParent = (GeoPage)Parent;
            Latitude = geoPageParent.DefaultLatitude;
            Longitude = geoPageParent.DefaultLongitude;
            PushChanges();
        }

        public void Handle(Input.Latitude action)
        {
            Latitude = action.Value;
            PushChanges();
        }

        public void Handle(Input.Longitude action)
        {
            Longitude = action.Value;
            PushChanges();
        }

        protected void PushChanges()
        {
            Transaction.Commit();
            Session.RunTaskForAll((s, sessionId) =>
            {
                var master = s.Store[nameof(MasterPage)] as MasterPage;
                if (!(master?.CurrentPage is GeoPage)) return;
                s.CalculatePatchAndPushOnWebSocket();
            });
        }
    }
}