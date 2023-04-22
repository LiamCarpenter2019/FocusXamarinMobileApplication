#region

using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Location = FocusXamarinMobileApplication.Models.Location;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class PinItemsSourcePageViewModel
{
    private readonly ObservableCollection<Location> _locations;
    private int _pinCreatedCount;

    public PinItemsSourcePageViewModel()
    {
        _locations = new ObservableCollection<Location>
        {
            new(), //"New York, USA", "The City That Never Sleeps", new Position(40.67, -73.94)),
            new(), //"Los Angeles, USA", "City of Angels", new Position(34.11, -118.41)),
            new() //"San Francisco, USA", "Bay City", new Position(37.77, -122.45))
        };

        AddLocationCommand = new RelayCommand(AddLocation);
        RemoveLocationCommand = new RelayCommand(RemoveLocation);
        ClearLocationsCommand = new RelayCommand(() => _locations.Clear());
        UpdateLocationsCommand = new RelayCommand(UpdateLocations);
        ReplaceLocationCommand = new RelayCommand(ReplaceLocation);
    }

    public IEnumerable Locations => _locations;

    public ICommand AddLocationCommand { get; }
    public ICommand RemoveLocationCommand { get; }
    public ICommand ClearLocationsCommand { get; }
    public ICommand UpdateLocationsCommand { get; }
    public ICommand ReplaceLocationCommand { get; }

    private void AddLocation()
    {
        _locations.Add(NewLocation());
    }

    private void RemoveLocation()
    {
        if (_locations.Any()) _locations.Remove(_locations.First());
    }

    private void UpdateLocations()
    {
        if (!_locations.Any()) return;

        var lastLatitude = _locations.Last().Position.Latitude;
        foreach (Location location in Locations)
            location.Position = new Position(lastLatitude, location.Position.Longitude);
    }

    private void ReplaceLocation()
    {
        if (!_locations.Any()) return;

        _locations[_locations.Count - 1] = NewLocation();
    }

    private Location NewLocation()
    {
        _pinCreatedCount++;
        return new Location();
        //$"Pin {_pinCreatedCount}", 
        //$"Desc {_pinCreatedCount}", 
        //RandomPosition.Next(new Position(39.8283459, -98.5794797), 8, 19));
    }
}