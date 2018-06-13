using System;
using System.Linq;
using UniformDocs.Helpers;
using UniformDocs.ViewModels;
using UniformDocs.ViewModels.Components;
using UniformDocs.ViewModels.Design;
using UniformDocs.ViewModels.HowTo;
using Starcounter;

namespace UniformDocs
{
    internal class Program
    {
        private static void Main()
        {
            var app = Application.Current;
            app.Use(new HtmlFromJsonProvider());
            app.Use(new PartialToStandaloneHtmlProvider());

            DummyData.Create();

            Blender.MapUri("/UniformDocs/menu", string.Empty, new[] { "menu" });
            Blender.MapUri("/UniformDocs/app-name", string.Empty, new[] { "app", "icon" });

            // just to offer a REST endpoint that gives the app version, usable for diagnostics
            // (currently used in github-source-links)
            Handle.GET("/UniformDocs/uniformdocs-app-version", () => GetAppVersionFromAssemblyFile());

            Handle.GET("/UniformDocs/partial/mainpage", () => new MainPage());

            Handle.GET("/UniformDocs/mainpage", () => WrapPage<MainPage>("/UniformDocs/partial/mainpage"));

            Handle.GET("/UniformDocs", () => Self.GET("/UniformDocs/mainpage"));

            Handle.GET("/UniformDocs/nav", () => new NavPage(), new HandlerOptions() { SelfOnly = true });

            Handle.GET("/UniformDocs/app-name", () => new AppName());

            Handle.GET("/UniformDocs/menu", () => new AppMenuPage());

            #region Design

            Handle.GET("/UniformDocs/partial/sections", () => new SectionsPage());
            Handle.GET("/UniformDocs/sections", () => WrapPage<SectionsPage>("/UniformDocs/partial/sections"));

            Handle.GET("/UniformDocs/partial/card", () => new CardPage());
            Handle.GET("/UniformDocs/card", () => WrapPage<CardPage>("/UniformDocs/partial/card"));

            Handle.GET("/UniformDocs/partial/title", () => new TitlePage());
            Handle.GET("/UniformDocs/title", () => WrapPage<TitlePage>("/UniformDocs/partial/title"));

            Handle.GET("/UniformDocs/partial/alerts", () => new AlertsPage());
            Handle.GET("/UniformDocs/alerts", () => WrapPage<AlertsPage>("/UniformDocs/partial/alerts"));

            Handle.GET("/UniformDocs/partial/leftnavlayout", () => new LeftNavLayoutPage());
            Handle.GET("/UniformDocs/leftnavlayout", () => WrapPage<LeftNavLayoutPage>("/UniformDocs/partial/leftnavlayout"));

            Handle.GET("/UniformDocs/partial/native", () => new NativePage());
            Handle.GET("/UniformDocs/native", () => WrapPage<NativePage>("/UniformDocs/partial/native"));

            #endregion

            #region Components

            Handle.GET("/UniformDocs/partial/autocomplete", () => Db.Scope(() => new AutocompletePage()));
            Handle.GET("/UniformDocs/autocomplete", () => WrapPage<AutocompletePage>("/UniformDocs/partial/autocomplete"));

            Handle.GET("/UniformDocs/partial/breadcrumb",
                () => { return Db.Scope(() => new BreadcrumbPage()); });
            Handle.GET("/UniformDocs/breadcrumb", () => WrapPage<BreadcrumbPage>("/UniformDocs/partial/breadcrumb"));

            Handle.GET("/UniformDocs/partial/button", () => new ButtonPage());
            Handle.GET("/UniformDocs/button", () => WrapPage<ButtonPage>("/UniformDocs/partial/button"));

            Handle.GET("/UniformDocs/partial/chart", () => new ChartPage());
            Handle.GET("/UniformDocs/chart", () => WrapPage<ChartPage>("/UniformDocs/partial/chart"));

            Handle.GET("/UniformDocs/partial/checkbox", () => new CheckboxPage());
            Handle.GET("/UniformDocs/checkbox", () => WrapPage<CheckboxPage>("/UniformDocs/partial/checkbox"));

            Handle.GET("/UniformDocs/partial/datagrid", () => new DatagridPage());
            Handle.GET("/UniformDocs/datagrid", () => WrapPage<DatagridPage>("/UniformDocs/partial/datagrid"));

            Handle.GET("/UniformDocs/partial/datepicker", () => new DatepickerPage());
            Handle.GET("/UniformDocs/datepicker", () => WrapPage<DatepickerPage>("/UniformDocs/partial/datepicker"));

            Handle.GET("/UniformDocs/partial/dropdown", () => new DropdownPage());
            Handle.GET("/UniformDocs/dropdown", () => WrapPage<DropdownPage>("/UniformDocs/partial/dropdown"));

            Handle.GET("/UniformDocs/partial/decimal", () => new DecimalPage());
            Handle.GET("/UniformDocs/decimal", () => WrapPage<DecimalPage>("/UniformDocs/partial/decimal"));

            Handle.GET("/UniformDocs/partial/html", () => new HtmlPage());
            Handle.GET("/UniformDocs/html", () => WrapPage<HtmlPage>("/UniformDocs/partial/html"));

            Handle.GET("/UniformDocs/partial/integer", () => new IntegerPage());
            Handle.GET("/UniformDocs/integer", () => WrapPage<IntegerPage>("/UniformDocs/partial/integer"));

            Handle.GET("/UniformDocs/partial/url", () => new UrlPage());
            Handle.GET("/UniformDocs/url", () => WrapPage<UrlPage>("/UniformDocs/partial/url"));

            Handle.GET("/UniformDocs/partial/Geo", () =>
            {
                return Db.Scope(() =>
                {
                    var geoPage = new GeoPage();
                    geoPage.Init();
                    return geoPage;
                });
            });
            Handle.GET("/UniformDocs/Geo", () => WrapPage<GeoPage>("/UniformDocs/partial/Geo"));

            Handle.GET("/UniformDocs/partial/markdown", () => new MarkdownPage());
            Handle.GET("/UniformDocs/markdown", () => WrapPage<MarkdownPage>("/UniformDocs/partial/markdown"));

            Handle.GET("/UniformDocs/partial/multiselect", () => new MultiselectPage());
            Handle.GET("/UniformDocs/multiselect", () => WrapPage<MultiselectPage>("/UniformDocs/partial/multiselect"));

            Handle.GET("/UniformDocs/partial/pagination", () => new PaginationPage());
            Handle.GET("/Uniformdocs/pagination", () => WrapPage<PaginationPage>("/UniformDocs/partial/pagination"));

            Handle.GET("/UniformDocs/partial/password", () => new PasswordPage());
            Handle.GET("/UniformDocs/password", () => WrapPage<PasswordPage>("/UniformDocs/partial/password"));

            Handle.GET("/UniformDocs/partial/radio", () => new RadioPage());
            Handle.GET("/UniformDocs/radio", () => WrapPage<RadioPage>("/UniformDocs/partial/radio"));

            Handle.GET("/UniformDocs/partial/radiolist", () => new RadiolistPage());
            Handle.GET("/UniformDocs/radiolist", () => WrapPage<RadiolistPage>("/UniformDocs/partial/radiolist"));

            Handle.GET("/UniformDocs/partial/table", () => new TablePage());
            Handle.GET("/UniformDocs/table", () => WrapPage<TablePage>("/UniformDocs/partial/table"));

            Handle.GET("/UniformDocs/partial/text", () => new TextPage());
            Handle.GET("/UniformDocs/text", () => WrapPage<TextPage>("/UniformDocs/partial/text"));

            Handle.GET("/UniformDocs/partial/textarea", () => new TextareaPage());
            Handle.GET("/UniformDocs/textarea", () => WrapPage<TextareaPage>("/UniformDocs/partial/textarea"));

            Handle.GET("/UniformDocs/partial/togglebutton", () => new ToggleButtonPage());
            Handle.GET("/UniformDocs/togglebutton",
                () => WrapPage<ToggleButtonPage>("/UniformDocs/partial/togglebutton"));

            #endregion

            #region How to

            Handle.GET("/UniformDocs/partial/callback", () => new CallbackPage());
            Handle.GET("/UniformDocs/callback", () => WrapPage<CallbackPage>("/UniformDocs/partial/callback"));

            Handle.GET("/UniformDocs/partial/clientlocalstate", () => new ClientLocalStatePage());
            Handle.GET("/UniformDocs/clientlocalstate", () => WrapPage<ClientLocalStatePage>("/UniformDocs/partial/clientlocalstate"));

            Handle.GET("/UniformDocs/partial/cookie", (Request request) =>
            {
                string name = "UniformDocs";
                CookiePage page = new CookiePage();
                Cookie cookie = request.Cookies.Select(x => new Cookie(x)).FirstOrDefault(x => x.Name == name);

                if (cookie != null)
                {
                    page.RequestCookie = cookie.Value;
                }

                cookie = new Cookie
                {
                    Name = name,
                    Value = $"UniformDocsCookie-{DateTime.Now.ToString()}",
                    Expires = DateTime.Now.AddDays(1)
                };

                Handle.AddOutgoingCookie(name, cookie.GetFullValueString());

                return page;
            });
            Handle.GET("/UniformDocs/cookie", () => WrapPage<CookiePage>("/UniformDocs/partial/cookie"));

            Handle.GET("/UniformDocs/partial/dialog", () => new DialogPage());
            Handle.GET("/UniformDocs/dialog", () => WrapPage<DialogPage>("/UniformDocs/partial/dialog"));

            Handle.GET("/UniformDocs/partial/fileupload", () => new FileUploadPage());
            Handle.GET("/UniformDocs/fileupload", () => WrapPage<FileUploadPage>("/UniformDocs/partial/fileupload"));
            HandleFile.GET("/UniformDocs/fileupload/upload", task =>
            {
                Session.RunTask(task.SessionId, (session, id) =>
                {
                    var master = session.Store[nameof(MasterPage)] as MasterPage;

                    var page = master?.CurrentPage as FileUploadPage;

                    if (page == null)
                    {
                        return;
                    }

                    var item = page.Tasks.FirstOrDefault(x => x.FileName == task.FileName);

                    switch (task.State)
                    {
                        case HandleFile.UploadTaskState.Error:
                            if (item != null)
                            {
                                page.Tasks.Remove(item);
                            }
                            break;
                        case HandleFile.UploadTaskState.Completed:
                            if (item != null)
                            {
                                page.Tasks.Remove(item);
                            }

                            var file = page.Files.FirstOrDefault(x => x.FileName == task.FileName) ?? page.Files.Add();

                            file.FileName = task.FileName;
                            file.FileSize = task.FileSize;
                            file.FilePath = task.FilePath;
                            break;
                        default:
                            if (item == null)
                            {
                                item = page.Tasks.Add();
                            }

                            item.FileName = task.FileName;
                            item.FileSize = task.FileSize;
                            item.Progress = task.Progress;
                            break;
                    }

                    session.CalculatePatchAndPushOnWebSocket();
                });
            });

            Handle.GET("/UniformDocs/partial/flashmessage", () => new FlashMessagePage());
            Handle.GET("/Uniformdocs/flashmessage", () => WrapPage<FlashMessagePage>("/UniformDocs/partial/flashmessage"));

            Handle.GET("/UniformDocs/partial/lazyloading", () => new LazyLoadingPage());
            Handle.GET("/Uniformdocs/lazyloading", () => WrapPage<LazyLoadingPage>("/UniformDocs/partial/lazyloading"));

            Handle.GET("/UniformDocs/partial/nested", () => new NestedPartial
            {
                Data = new AnyData()
            });
            Handle.GET("/UniformDocs/nested", () => WrapPage<NestedPartial>("/UniformDocs/partial/nested"));

            Handle.GET("/UniformDocs/partial/progressbar", () => new ProgressBarPage());
            Handle.GET("/Uniformdocs/progressbar", () => WrapPage<ProgressBarPage>("/UniformDocs/partial/progressbar"));

            Handle.GET("/UniformDocs/partial/Redirect", () => new RedirectPage());
            Handle.GET("/UniformDocs/Redirect", () => WrapPage<RedirectPage>("/UniformDocs/partial/Redirect"));
            Handle.GET("/UniformDocs/Redirect/{?}", (string param) =>
            {
                var master = WrapPage<RedirectPage>("/UniformDocs/partial/Redirect") as MasterPage;
                var page = master.CurrentPage as RedirectPage;
                page.YourFavoriteFood = "You've got some tasty " + param;
                return master;
            });

            Handle.GET("/UniformDocs/partial/Validation", () => new ValidationPage());
            Handle.GET("/UniformDocs/Validation", () => WrapPage<ValidationPage>("/UniformDocs/partial/Validation"));

            #endregion
        }

        public static string GetAppVersionFromAssemblyFile()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        private static Json WrapPage<T>(string partialPath) where T : Json
        {
            var master = GetMasterPageFromSession();

            if (master.CurrentPage != null && master.CurrentPage.GetType() == typeof(T))
            {
                return master;
            }

            master.CurrentPage = Self.GET(partialPath);

            if (master.CurrentPage.Data == null)
            {
                master.CurrentPage.Data = null; //trick to invoke OnData in partial
            }

            return master;
        }

        private static MasterPage GetMasterPageFromSession()
        {
            var master = Session.Ensure().Store[nameof(MasterPage)] as MasterPage;

            if (master == null)
            {
                master = new MasterPage
                {
                    NavPage = Self.GET("/uniformdocs/nav")
                };
                Session.Current.Store[nameof(MasterPage)] = master;
            }

            return master;
        }
    }
}