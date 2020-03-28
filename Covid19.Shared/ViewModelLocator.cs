using Covid19.Shared.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Preserve]
[assembly: XmlnsDefinition("https://github.com/waliarubal/schemas", "Covid19.Shared")]
[assembly: XmlnsDefinition("https://github.com/waliarubal/schemas", "Covid19.Shared.Base")]
[assembly: XmlnsDefinition("https://github.com/waliarubal/schemas", "Covid19.Shared.Behaviors")]
[assembly: XmlnsDefinition("https://github.com/waliarubal/schemas", "Covid19.Shared.Commands")]
[assembly: XmlnsDefinition("https://github.com/waliarubal/schemas", "Covid19.Shared.Converters")]

namespace Covid19.Shared
{
    public static class ViewModelLocator
    {
        static Dictionary<string, ViewModelBase> _viewModelCache;

        public static readonly BindableProperty AutoWireViewModelProperty;

        static ViewModelLocator()
        {
            AutoWireViewModelProperty = BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

            _viewModelCache = new Dictionary<string, ViewModelBase>();
        }

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
                return;

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            if (_viewModelCache.ContainsKey(viewModelName))
            {
                view.BindingContext = _viewModelCache[viewModelName];
                return;
            }

            // get VM from cache
            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
                return;
            
            var viewModel = ServiceLocator.Instance.Container.Resolve(viewModelType) as ViewModelBase;
            view.BindingContext = viewModel;
            viewModel.IsLoaded = true;

            // cache VM
            if (viewModel.IsCachable && !_viewModelCache.ContainsKey(viewModelName))
                _viewModelCache.Add(viewModelName, viewModel);
        }
    }
}
