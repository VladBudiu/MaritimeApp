using MaritimeApp.Domain.Entities;

namespace MaritimeApp.Domain.Interfaces;

public interface IPortRepository : IRepository<Port> {

    Task<IEnumerable<Ship>> GetShipsAtPortAsync(int portId);    

 }
