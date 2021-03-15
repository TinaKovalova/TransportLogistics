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
    public class OrderService:IService<OrderDTO>
    {
        IUnitOfWork unitOfWork;
        IRepository<Order> repository;
        IMapper mapper;
        public OrderService(IRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDTO>().ReverseMap();
                cfg.CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap();
                cfg.CreateMap<User, UserDTO>().ReverseMap();

            });
            mapper = new Mapper(config);
        }
        public OrderDTO CreateOrUpdate(OrderDTO entity)
        {
            var order = mapper.Map<Order>(entity);
            repository.CreateOrUpdate(order);
            unitOfWork.Save();
            return mapper.Map<OrderDTO>(order);
        }
        public OrderDTO Get(int id) => mapper.Map<OrderDTO>(repository.Get(id));
        public IEnumerable<OrderDTO> GetAll() =>
                            mapper.Map<IEnumerable<OrderDTO>>(repository.GetAll());


        public void Remove(OrderDTO entity)
        {
            repository.Remove(mapper.Map<Order>(entity));
            unitOfWork.Save();
        }
    }
}
