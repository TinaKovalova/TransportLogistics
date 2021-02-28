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
    public class RoleService:IService<RoleDTO>
    {
        IUnitOfWork unitOfWork;
        IRepository<Role> repository;
        IMapper mapper;
        public RoleService(IRepository<Role> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                            cfg.CreateMap<Role, RoleDTO>().ReverseMap());
            mapper = new Mapper(config);

        }
        public RoleDTO CreateOrUpdate(RoleDTO entity)
        {
            var user = mapper.Map<Role>(entity);
            repository.CreateOrUpdate(user);
            unitOfWork.Save();
            return mapper.Map<RoleDTO>(user);
        }
        public RoleDTO Get(int id) => mapper.Map<RoleDTO>(repository.Get(id));
        public IEnumerable<RoleDTO> GetAll() =>
                            mapper.Map<IEnumerable<RoleDTO>>(repository.GetAll());


        public void Remove(RoleDTO entity)
        {
            repository.Remove(mapper.Map<Role>(entity));
            unitOfWork.Save();
        }
    }
}
