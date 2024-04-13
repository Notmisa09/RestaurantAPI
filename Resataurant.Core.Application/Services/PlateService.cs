using AutoMapper;
using Restaurant.Core.Application.Dto.Plates;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Application.Interfaces.IServices;
using Restaurant.Core.Domain.Entities;
using System.Runtime.CompilerServices;

namespace Restaurant.Core.Application.Services
{
    public class PlateService : GenericService<PlateDto, PlatesAddDto, Plates>, IPlateService
    {
        private readonly IMapper _mapper;
        private readonly IPlatesRepository _platesRepository;
        private readonly IPlateIngridientsRepository _plateIngridientsRep;
        public PlateService(IMapper mapper,
                            //
                            IPlatesRepository platesRepostiry,
                            //
                            IPlateIngridientsRepository plateIngridientsRep) : base(mapper, platesRepostiry)
        {
            _platesRepository = platesRepostiry;
            _mapper = mapper;
            _plateIngridientsRep = plateIngridientsRep;
        }

        public async Task<List<PlateDto>> GetAllWithIncludePlateInfo()
        {
            var result = await _platesRepository.GetAllWithInclude(new List<string> { "PlatesCategories" });
            return result.Select(x => new PlateDto
            {
                Id = x.Id,
                Name = x.Name,
                PeopleAmount = x.PeopleAmount,
                PlateCategoriesId = x.PlateCategoriesId,
                PlateCategory = x.PlatesCategories.Description,
                Price = x.Price,
                PlateIngridients = _plateIngridientsRep.GetIngridients(x.Id)

            }).ToList();
        }


        public async Task UpdateWithIngridients(PlatesAddDto Dto)
        {
            await Update(Dto,Dto.Id);
            await _plateIngridientsRep.RemoveAsyncRelations(Dto.Id);
            foreach (var item in Dto.PlateIngridients)
            {
                PlatesIngridients platesIngridients = new()
                {
                    IngridientsId = item,
                    PlateId = Dto.Id,
                };
                await _plateIngridientsRep.CreateAsync(platesIngridients);
            }
        }

        public async Task<List<PlateDto>> GetByIdWithInfo(int Id)
        {
            var result = await _platesRepository.GetAllWithInclude(new List<string> { "PlatesCategories" });
            return result.Where(x => x.Id == Id).Select(x => new PlateDto
            {
                Id = x.Id,
                Name = x.Name,
                PeopleAmount = x.PeopleAmount,
                PlateCategoriesId = x.PlateCategoriesId,
                PlateCategory = x.PlatesCategories.Description,
                Price = x.Price,
                PlateIngridients = _plateIngridientsRep.GetIngridients(x.Id)
            }).ToList();
        }

        public override async Task<PlatesAddDto> Add(PlatesAddDto Dto)
        {
            var  plates = _mapper.Map<PlatesAddDto>(Dto);
            var platewithId = await base.Add(plates);
            foreach (var item in Dto.PlateIngridients)
            {
                PlatesIngridients platesIngridients = new()
                {
                    IngridientsId = item,
                    PlateId = platewithId.Id,
                };
                await _plateIngridientsRep.CreateAsync(platesIngridients);
            }
            return Dto;
        }
    }
}
