public interface IHotelRepository : IDisposable
{
    Task<List<Hotel>> GetHotelsAsync();

    Task<List<Hotel>> GetHotelAsync(string nameHotel);

    Task<List<Hotel>> GetHotelsAsync(Coordinate coordinate);
    Task<Hotel> GetHotelAsync(int hotelID);

    Task InsertHotelAsync(Hotel hotel);
    
    Task UpdateHotelAsync(Hotel hotel);

    Task DeleteHotelAsync(int hotelID);

    Task SaveAsync();

}