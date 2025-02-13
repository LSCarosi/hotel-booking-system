using HotelBookingSystem.Domain.Interfaces;
using HotelBookingSystem.Infrastructure.Repositories;
namespace HotelBookingSystem.Application.Services;

public class ReservationCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ReservationCleanupService> _logger;

    public ReservationCleanupService(IServiceProvider serviceProvider, ILogger<ReservationCleanupService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CleanupExpiredReservations();
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Execute each 1 hour
        }
    }

    private async Task CleanupExpiredReservations()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var reservationRepository = scope.ServiceProvider.GetRequiredService<IReservationRepository>();
            var roomRepository = scope.ServiceProvider.GetRequiredService<IRoomRepository>();

            var expiredReservations = await reservationRepository.GetExpiredReservationsAsync();

            foreach (var reservation in expiredReservations)
            {
                var room = await roomRepository.GetRoomByIdAsync(reservation.RoomId);
                if (room != null && !room.IsAvailable)
                {
                    room.MarkAsAvailable();
                    await roomRepository.UpdateRoomAsync(room);
                    _logger.LogInformation($"Quarto {room.Id} liberado após expiração da reserva {reservation.Id}");
                }
            }
        }
    }
}
