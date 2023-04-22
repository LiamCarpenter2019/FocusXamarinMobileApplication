namespace FocusXamarinMobileApplication.Models;

internal static class RandomPosition
{
    private static readonly Random Random = new(Environment.TickCount);

    public static Position Next(Position position, double latitudeRange, double longitudeRange)
    {
        return new Position(
            position.Latitude + (Random.NextDouble() * 2 - 1) * latitudeRange,
            position.Longitude + (Random.NextDouble() * 2 - 1) * longitudeRange);
    }
}