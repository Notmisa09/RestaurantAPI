namespace Restaurant.Core.Application.Interfaces.IServices
{
    public interface IGenericService <ViewModel, AddViewModel, Entity>
    {
        Task Update(AddViewModel vm, int Id);
        Task<List<ViewModel>> GetAll();
        Task<AddViewModel> Add(AddViewModel Entity);
        Task<AddViewModel> GetById (int id);
        Task Remove(int Id);
    }
}
