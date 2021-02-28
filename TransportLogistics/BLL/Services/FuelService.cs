using AutoMapper;
using BLL.DTO;
using DAL.Context;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FuelService : IService<FuelDTO>
    {
        IRepository<Fuel> repository;
        IUnitOfWork unitOfWork;
        IMapper mapper;
        public FuelService(IRepository<Fuel> repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            var config = new MapperConfiguration(cfg =>
                          cfg.CreateMap<Fuel, FuelDTO>().ReverseMap());
            mapper = new Mapper(config);
        }
        public FuelDTO CreateOrUpdate(FuelDTO entity)
        {
            var fuel = mapper.Map<Fuel>(entity);
            repository.CreateOrUpdate(fuel);
            unitOfWork.Save();
            return mapper.Map<FuelDTO>(fuel);
        }
        public FuelDTO Get(int id) => mapper.Map<FuelDTO>(repository.Get(id));
        public IEnumerable<FuelDTO> GetAll() =>
                     mapper.Map<IEnumerable<FuelDTO>>(repository.GetAll());
        public void Remove(FuelDTO entity)
        {
            repository.Remove(mapper.Map<Fuel>(entity));
            unitOfWork.Save();
        }
    }
}
