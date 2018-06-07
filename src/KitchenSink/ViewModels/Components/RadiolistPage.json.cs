using Starcounter;

namespace KitchenSink.ViewModels.Components
{
    partial class RadiolistPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            MenuOptionsElement a;
            a = this.MenuOptions.Add();
            a.Label = "Dogs";
            a = this.MenuOptions.Add();
            a.Label = "Cats";
            this.SelectOption(0);

            TestUsersElement u;
            u = this.TestUsers.Add();
            u.Avatar = "pic1";
            u.Name = "Tomek W";
            u.Username = "tomalec";
            u = this.TestUsers.Add();
            u.Avatar = "pic2";
            u.Name = "Atomek W";
            u.Username = "atom";
            u = this.TestUsers.Add();
            u.Avatar = "pic3";
            u.Name = "Romek";
            u.Username = "radom";
            this.LoginUser = "tomalec";
        }

        public void SelectOption(int index)
        {
            SelectedItemLabel = MenuOptions[index].Label;
        }

        void Handle(Input.SelectedItemIndex action)
        {
            SelectOption(System.Int32.Parse(action.Value));
        }
    }

    [RadiolistPage_json.MenuOptions]
    partial class MenuOptionsElement : Json
    {
        void Handle(Input.Choose action)
        {
            Label = "!";
        }
    }
    [RadiolistPage_json.TestUsers]
    partial class TestUsersElement : Json
    {
        /*void Handle(Input.Choose action)
        {
            Label = "!";
        }*/
    }
}