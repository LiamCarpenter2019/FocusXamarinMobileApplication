namespace FocusXamarinMobileApplication.Helpers;

public class NavigationService : INavigationService
{
    private readonly Dictionary<string, Type> _pagesByKey = new();
    private NavigationPage _navigation;

    public string CurrentPageKey
    {
        get
        {
            lock (_pagesByKey)
            {
                if (_navigation.CurrentPage == null) return null;

                var pageType = _navigation.CurrentPage.GetType();

                return _pagesByKey.ContainsValue(pageType)
                    ? _pagesByKey.First(p => p.Value == pageType).Key
                    : null;
            }
        }
    }

    public void GoBack()
    {
        if (CanGoBack()) _navigation.PopAsync();
    }

    public void NavigateTo(string pageKey)
    {
        NavigateTo(pageKey, null);
    }

    // Required for interface
    public void NavigateTo(string pageKey, object parameter)
    {
        NavigateTo(pageKey, parameter, null, null, null, null, null, false);
    }

    public void Initialize(NavigationPage navigationPage)
    {
        _navigation = navigationPage ?? NavigationalParameters.MainPage;
    }

    public bool CanGoBack()
    {
        return _navigation?.Navigation?.NavigationStack?.Count > 1;
    }

    // Two or more parameters
    public void NavigateTo(string pageKey, object parameter1, object parameter2, object parameter3 = null,
        object parameter4 = null, object parameter5 = null, object parameter6 = null)
    {
        NavigateTo(pageKey, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, false);
    }

    private void NavigateTo(string pageKey, object parameter1, object parameter2, object parameter3,
        object parameter4, object parameter5, object parameter6, bool replaceRoot)
    {
        lock (_pagesByKey)
        {
            if (_pagesByKey.ContainsKey(pageKey))
            {
                _navigation = _navigation ?? NavigationalParameters.MainPage;
                var type = _pagesByKey[pageKey];
                ConstructorInfo constructor;
                var p = new List<object>();
                if (parameter1 != null)
                    p.Add(parameter1);
                if (parameter2 != null)
                    p.Add(parameter2);
                if (parameter3 != null)
                    p.Add(parameter3);
                if (parameter4 != null)
                    p.Add(parameter4);
                if (parameter5 != null)
                    p.Add(parameter5);
                if (parameter6 != null)
                    p.Add(parameter6);
                var parameters = p.ToArray();
                constructor = GetConstructor(type, parameters);
                if (constructor == null)
                {
                    var exceptionMessage = $"No suitable constructor found for page {pageKey}";
                    throw new InvalidOperationException(exceptionMessage);
                }

                if (!replaceRoot)
                {
                    var page = constructor.Invoke(parameters) as Page;
                    _navigation.PushAsync(page, false);
                }
                else
                {
                    var page = constructor.Invoke(parameters) as Page;
                    var root = _navigation.Navigation.NavigationStack.First();
                    _navigation.Navigation.InsertPageBefore(page, root);
                    _navigation.PopToRootAsync(false);
                }
            }
            else
            {
                var exceptionMessage = $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?";
                throw new ArgumentException(exceptionMessage, nameof(pageKey));
            }
        }
    }

    private ConstructorInfo GetConstructor(Type type, object[] parameters)
    {
        var parameterCount = parameters.Length;
        ConstructorInfo constructor;
        if (parameterCount > 0)
            constructor = type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(
                c =>
                {
                    var p = c.GetParameters();
                    return p.Count() == parameterCount &&
                           p[parameterCount - 1].ParameterType == parameters[parameterCount - 1].GetType();
                });
        else
            constructor = type.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(c => !c.GetParameters().Any());
        return constructor;
    }

    public void Configure(string pageKey, Type pageType)
    {
        lock (_pagesByKey)
        {
            if (_pagesByKey.ContainsKey(pageKey))
                _pagesByKey[pageKey] = pageType;
            else
                _pagesByKey.Add(pageKey, pageType);
        }
    }

    public void SetNewRoot(string pageKey)
    {
        NavigateTo(pageKey, null, null, null, null, null, null, true);
    }
}