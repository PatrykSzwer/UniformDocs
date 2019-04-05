using Starcounter;
using System.Threading;
using System;
using System.Threading.Tasks;
using Starcounter.Async;

namespace UniformDocs.ViewModels.HowTo
{
    partial class LazyLoadingPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
            FillDummyData();
        }

        private void FillDummyData()
        {
            CreatePerson(1, "Alicia", "Alcott");
            CreatePerson(2, "Beatrice", "Black");
            CreatePerson(3, "Claire", "Clancy");
            CreatePerson(4, "Delilah", "Darcy");
            CreatePerson(5, "Ellie", "Earnhart");
            CreatePerson(6, "Faith", "Fahrlander");
            CreatePerson(7, "Grace", "Gather");
        }

        private void CreatePerson(int order, string firstName, string lastName, string favoriteGame = "")
        {
            var person = People.Add();
            person.Order = order;
            person.FirstName = firstName;
            person.LastName = lastName;
            person.FavoriteGame = favoriteGame;
        }

        [LazyLoadingPage_json.People]
        partial class LazyLoadingPagePeople : Json
        {
            protected int MinDataRetrievalDelay = 300;
            protected int MaxDataRetrievalDelay = 1000;

            public LazyLoadingPage ParentPage => this.Parent.Parent as LazyLoadingPage;

            // IsHovered is set on the client side. Every time a person gets hovered, isHovered is set to 1.
            // And the value is set to 0 on blur.
            public void Handle(Input.IsHovered action)
            {
                if (!this.DataIsLoaded && action.Value != 0)
                {
                    Random rnd = new Random();
                    AsyncInputHandlers.Run(() => StartDataRetrieval(rnd.Next(MinDataRetrievalDelay, MaxDataRetrievalDelay)));
                }
            }

            private async Task StartDataRetrieval(int delayMilliseconds)
            {
                // This method is called on Starcounter Scheduler, using Session associated with this view-model

                // Read FirstName property while we still have access to Session
                var firstName = this.FirstName;

                // Schedule data retrieval on Starcounter Scheduler. RetrieveDataFromFakeDataBase will be executed on a Starcounter Scheduler,
                // but without access to the Session associated with this view-model. It will have access to DB, but won't block this view-model's updates
                string data = null;
                await Scheduling.RunTask(() => data = RetrieveDataFromFakeDataBase(TimeSpan.FromMilliseconds(delayMilliseconds), firstName));

                // Use freshly-retrieved data to change the UI
                DataRetrievalUpate(data);
            }

            private void DataRetrievalUpate(string data)
            {
                this.DataIsLoaded = true;
                this.DataToShow = data;
                this.FavoriteGame = data;

                if (this.IsHovered == 1)
                {
                    this.ParentPage.DisplayedData.DataContent = data;
                }
            }

            // Acts as a fake database, and provide each person with a favorite game
            public string RetrieveDataFromFakeDataBase(TimeSpan delay, string firstName)
            {
                // simulate work
                Thread.Sleep(delay);
                switch (firstName)
                {
                    case "Alicia":
                        return this.FavoriteGame = "The Last of Us";

                    case "Beatrice":
                        return this.FavoriteGame = "Dragon Age: Inquisition";

                    case "Claire":
                        return this.FavoriteGame = "Final Fantasy XIII";

                    case "Delilah":
                        return this.FavoriteGame = "World of Warcraft";

                    case "Ellie":
                        return this.FavoriteGame = "Overwatch";

                    case "Faith":
                        return this.FavoriteGame = "Pokemon X";

                    case "Grace":
                        return this.FavoriteGame = "Counter Strike";
                    default:
                        throw new ArgumentException();
                }
            }
        }
    }
}
