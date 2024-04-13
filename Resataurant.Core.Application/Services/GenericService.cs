using AutoMapper;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Application.Interfaces.IServices;

namespace Restaurant.Core.Application.Services
{
    public class GenericService<ViewModel, AddViewModel, Entity> : IGenericService<ViewModel, AddViewModel, Entity>
        where ViewModel : class
        where AddViewModel : class
        where Entity : class 
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Entity> _repository;
        public GenericService(IMapper mapper, IBaseRepository<Entity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task Update(AddViewModel vm, int Id)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            await _repository.UpdateAsync(entity, Id);
        }

        public virtual async Task<AddViewModel> Add(AddViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            entity = await _repository.CreateAsync(entity);
            AddViewModel entityVm = _mapper.Map<AddViewModel>(entity);
            return entityVm;
        }

        public virtual async Task Remove(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            await _repository.RemoveAsync(entity);
        }

        public virtual async Task<AddViewModel> GetById(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            return _mapper.Map<AddViewModel>(entity);
        }

        public virtual async Task<List<ViewModel>> GetAll()
        {
            var entityList = await _repository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entityList);
        }

    }
}
