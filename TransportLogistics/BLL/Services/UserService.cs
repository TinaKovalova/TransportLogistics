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
    public class UserService:IService<UserDTO>
    {
        IUnitOfWork unitOfWork;
        IRepository<User> repository;
        IMapper mapper;
        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<User, UserDTO>().ReverseMap());
            mapper = new Mapper(config);

        }
        public UserDTO CreateOrUpdate(UserDTO entity)
        {
            var user = mapper.Map<User>(entity);
            repository.CreateOrUpdate(user);
            unitOfWork.Save();
            return mapper.Map<UserDTO>(user);
        }
        public UserDTO Get(int id) => mapper.Map<UserDTO>(repository.Get(id));
        public IEnumerable<UserDTO> GetAll() =>
                            mapper.Map<IEnumerable<UserDTO>>(repository.GetAll());


        public void Remove(UserDTO entity)
        {
            repository.Remove(mapper.Map<User>(entity));
            unitOfWork.Save();
        }
    }
}
