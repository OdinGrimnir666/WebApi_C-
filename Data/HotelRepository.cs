public class HotelRepository : IHotelRepository
{
    private readonly HotelDb _context;

    public HotelRepository(HotelDb context)
    {
        _context = context;
    }
    public Task<List<Hotel>> GetHotelsAsync() =>
    _context.Hotels.ToListAsync();

    public Task<List<Hotel>> GetHotelAsync(string nameHotel) =>
        _context.Hotels.Where(h => h.Name.Contains(nameHotel)).ToListAsync();

    public async Task<List<Hotel>> GetHotelsAsync(Coordinate coordinate) =>
        await _context.Hotels.Where(hotel =>
            hotel.Latitude > coordinate.Latilude - 1 &&
            hotel.Latitude < coordinate.Latilude + 1 &&
            hotel.Longtide > coordinate.Longitude - 1 &&
            hotel.Longtide < coordinate.Longitude + 1
        ).ToListAsync();

    public async Task<Hotel> GetHotelAsync(int hotelID) =>
        await _context.Hotels.FindAsync(new object[] { hotelID });
    public async Task InsertHotelAsync(Hotel hotel) =>
    await _context.Hotels.AddAsync(hotel);

    public async Task UpdateHotelAsync(Hotel hotel)
    {
        var hotelFromdb = await _context.Hotels.FindAsync(new object[] { hotel.Id });
        if (hotelFromdb == null) return;
        hotelFromdb.Longtide = hotel.Longtide;
        hotelFromdb.Latitude = hotel.Latitude;
        hotelFromdb.Name = hotel.Name;
    }
    public async Task DeleteHotelAsync(int hotelID)
    {
        var hotelFromdb = await _context.Hotels.FindAsync(new object[] { hotelID });
        if (hotelFromdb == null) return;
        _context.Hotels.Remove(hotelFromdb);
    }

    public async Task SaveAsync() =>
    await _context.SaveChangesAsync();

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}