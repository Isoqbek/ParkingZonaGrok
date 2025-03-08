using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Core.Application.Services;

public class ParkingZoneService : IParkingZoneService
{
    private readonly IParkingZoneRepository _zoneRepository;
    private readonly IParkingSpotRepository _spotRepository;

    public ParkingZoneService(IParkingZoneRepository zoneRepository, IParkingSpotRepository spotRepository)
    {
        _zoneRepository = zoneRepository;
        _spotRepository = spotRepository;
    }

    public async Task<ParkingZoneDto?> GetZoneByIdAsync(int id)
    {
        var zone = await _zoneRepository.GetByIdAsync(id);
        return zone == null ? null : new ParkingZoneDto
        {
            Id = zone.Id,
            TotalSpots = zone.TotalSpots,
            AvailableSpots = zone.AvailableSpots,
            HourlyRate = zone.HourlyRate
        };
    }

    public async Task UpdateTotalZoneAsync(ParkingZoneDto zoneDto)
    {
        var zone = await _zoneRepository.GetByIdAsync(zoneDto.Id);
        if (zone == null) return;

        zone.TotalSpots = zoneDto.TotalSpots;
        zone.AvailableSpots = zoneDto.AvailableSpots;
        zone.HourlyRate = zoneDto.HourlyRate;

        await _zoneRepository.UpdateAsync(zone);
    }

    public async Task<bool> UpdateAvailableSpotsAsync(int zoneId, int change)
    {
        var zone = await _zoneRepository.GetByIdAsync(zoneId);
        if (zone == null) return false;

        // Change = +1 bo‘lsa - joy bo‘shagan
        // Change = -1 bo‘lsa - joy band bo‘lgan
        zone.AvailableSpots = Math.Max(0, zone.AvailableSpots + change);

        await _zoneRepository.UpdateAsync(zone);
        return true;
    }


    public async Task UpdateZoneAsync(ParkingZoneDto zoneDto)
    {
        var zone = await _zoneRepository.GetByIdAsync(zoneDto.Id);
        if (zone == null) return;

        int currentSpots = await _spotRepository.GetSpotCountAsync();
        int difference = zoneDto.TotalSpots - currentSpots;

        if (difference > 0)
        {
            await AddSpots(difference);
        }
        else if (difference < 0)
        {
            await RemoveSpots(-difference);
        }

        zone.TotalSpots = zoneDto.TotalSpots;
        await _zoneRepository.UpdateAsync(zone);
    }


private async Task AddSpots(int count)
{
    var existingSpots = await _spotRepository.GetAllAsync();
    string currentLetterCombo = "AA"; // Boshlang‘ich harf kombinatsiyasi
    int spotNumber = 1;

    if (existingSpots.Any())
    {
        var lastSpot = existingSpots
            .OrderByDescending(s => s.SpotNumber.Substring(0, 2)) // Harf kombinatsiyasi bo‘yicha saralash
            .ThenByDescending(s => int.Parse(Regex.Match(s.SpotNumber, @"\d+").Value)) // Raqam bo‘yicha saralash
            .First();

        currentLetterCombo = lastSpot.SpotNumber.Substring(0, 2); // Oxirgi harf kombinatsiyasini olish (masalan, "AB")
        spotNumber = int.Parse(Regex.Match(lastSpot.SpotNumber, @"\d+").Value) + 1; // Oxirgi raqamdan keyingisi

        if (spotNumber > 20) // 20 dan oshsa harf kombinatsiyasi o‘zgaradi
        {
            spotNumber = 1;
            currentLetterCombo = IncrementLetterCombo(currentLetterCombo);
        }
    }

    for (int i = 0; i < count; i++)
    {
        if (spotNumber > 20)
        {
            spotNumber = 1;
            currentLetterCombo = IncrementLetterCombo(currentLetterCombo);
        }

        string spotName = $"{currentLetterCombo}{spotNumber}";
        var spot = new ParkingSpot
        {
            SpotNumber = spotName,
            IsOccupied = false
        };

        await _spotRepository.AddAsync(spot);
        spotNumber++;
    }
}

// Harf kombinatsiyasini keyingi qiymatga oshirish uchun yordamchi metod
private string IncrementLetterCombo(string currentCombo)
{
    char firstLetter = currentCombo[0];
    char secondLetter = currentCombo[1];

    if (secondLetter < 'Z')
    {
        secondLetter++; // Ikkinchi harfni oshirish (masalan, AA → AB)
    }
    else
    {
        secondLetter = 'A'; // Ikkinchi harf Z dan keyin A ga qaytadi
        firstLetter++; // Birinchi harf oshiriladi (masalan, AZ → BA)
    }

    return $"{firstLetter}{secondLetter}";
}

private async Task RemoveSpots(int count)
    {
        var spots = (await _spotRepository.GetAllAsync())
            .OrderByDescending(s => s.SpotNumber[0])
            .ThenByDescending(s => int.Parse(Regex.Match(s.SpotNumber, @"\d+").Value))
            .Take(count)
            .ToList();

        foreach (var spot in spots)
        {
            await _spotRepository.DeleteAsync(spot.Id);
        }
    }

}
