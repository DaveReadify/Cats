using System;
using System.Reflection;
using Xamarin.Forms;

namespace Cats.Behaviours
{
    public class ViewModelResolver
    {
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelResolver), false,
                propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        private static void OnAutoWireViewModelChanged(BindableObject d, object oldValue, object newValue)
        {
            if ((bool)newValue)
                AutoWireViewModelChanged(d);
        }

        private static void Bind(object view, object viewModel)
        {
            var element = view as VisualElement;
            if (element != null)
                element.BindingContext = viewModel;
        }


        private static void AutoWireViewModelChanged(object view)
        {
            var viewModelType = ViewToViewModelTypeResolver(view.GetType());
            if (viewModelType == null)
                return;

            var viewModel = (Application.Current as App)?.Resolve(viewModelType);

            Bind(view, viewModel);
        }

        private static Type ViewToViewModelTypeResolver(Type viewType)
        {
            var viewName = viewType.FullName;
            viewName = viewName.Replace(".Views.", ".ViewModels.");
            var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                              "{0}{1}, {2}", viewName, suffix, viewAssemblyName);

            return Type.GetType(viewModelName);
        }
    }
}
