// PortDto.cs
namespace MaritimeApp.Application.DTOs.Port;
public record PortDto(
    int Id,
    string Name,
    string? CountryName,
    int VoyagesCount);   // quick stat



