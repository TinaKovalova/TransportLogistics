using BLL.DTO;
using DAL.Context;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    public class CarSevice : IService<CarDTO>
    {
        IUnitOfWork unitOfWork;
        IRepository<Car> repository;
        IMapper mapper;
        public CarSevice(IRepository<Car> repository,IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<Car, CarDTO>().ReverseMap());
            mapper = new Mapper(config);

        }
        public CarDTO CreateOrUpdate(CarDTO entity)
        {
            var car = mapper.Map<Car>(entity);
            repository.CreateOrUpdate(car);
            unitOfWork.Save();
            return mapper.Map<CarDTO>(car);
        }
        public CarDTO Get(int id) => mapper.Map<CarDTO>(repository.Get(id));
        public IEnumerable<CarDTO> GetAll() =>
                            mapper.Map < IEnumerable<CarDTO>>(repository.GetAll());
       

        public void Remove(CarDTO entity)
        {
            repository.Remove(mapper.Map<Car>(entity));
            unitOfWork.Save();
        }
    }
}
