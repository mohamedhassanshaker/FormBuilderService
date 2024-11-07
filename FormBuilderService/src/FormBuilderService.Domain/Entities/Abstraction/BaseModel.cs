namespace FormBuilderService.Domain.Entities.Abstraction
{
    public abstract class BaseModel<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; } = default!;
    }
}