using AutoMapper;
using Restaurant.Core.Application.Dto.Ingridients;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Application.Interfaces.IServices;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services
{
    public class IngridientsService : GenericService<IngridientsDto, IngridientsAddDto, Ingridients>, IingridientsService
    {
        private readonly IMapper _mapper;
        private readonly IingridientsRepository _ingridientsRepository;
        public IngridientsService(IMapper mapper,
        IingridientsRepository ingridientsrepository) : base(mapper, ingridientsrepository)
        {
            _ingridientsRepository = ingridientsrepository;
            _mapper = mapper;
        }
    }
}
