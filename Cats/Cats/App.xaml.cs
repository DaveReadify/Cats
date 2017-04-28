using System;
using Autofac;
using Cats.Modules;
using Cats.Views;
using Xamarin.Forms;

namespace Cats
{
    public partial class App : Application
    {
        private IContainer _container;

        public App()
        {
            InitializeComponent();

            //MainPage = new PeoplePage();
            MainPage = new ContentPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            _container = CreateContainer();

            SetupMainPage();
        }

        private void SetupMainPage()
        {
            MainPage = new NavigationPage(new PeoplePage());
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterDependencies(builder);
            return builder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterModule<ViewModelModule>();
            builder.RegisterModule<ServicesModule>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
