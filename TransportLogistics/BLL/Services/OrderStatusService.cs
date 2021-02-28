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
    public class OrderStatusService : IService<OrderStatusDTO>
    {
        IUnitOfWork unitOfWork;
        IRepository<OrderStatus> repository;
        IMapper mapper;
        public OrderStatusService(IRepository<OrderStatus> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap());
            mapper = new Mapper(config);

        }
        public OrderStatusDTO CreateOrUpdate(OrderStatusDTO entity)
        {
            var status = mapper.Map<OrderStatus>(entity);
            repository.CreateOrUpdate(status);
            unitOfWork.Save();
            return mapper.Map<OrderStatusDTO>(status);
        }
        public OrderStatusDTO Get(int id) => mapper.Map<OrderStatusDTO>(repository.Get(id));
        public IEnumerable<OrderStatusDTO> GetAll() =>
                            mapper.Map<IEnumerable<OrderStatusDTO>>(repository.GetAll());


        public void Remove(OrderStatusDTO entity)
        {
            repository.Remove(mapper.Map<OrderStatus>(entity));
            unitOfWork.Save();
        }
    }
}
