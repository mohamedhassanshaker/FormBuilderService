using FormBuilderService.Domain.Interfaces.Factory;

namespace FormBuilderService.Application.FactoryAbstraction
{
    public class GenericFactory<TKey, TBaseType> where TKey : notnull
    {
        private readonly IRepositoriesFactory _repositoriesFactory;
        private readonly Dictionary<TKey, TBaseType> _instances = new();

        public GenericFactory(IRepositoriesFactory repositoriesFactory)
        {
            _repositoriesFactory = repositoriesFactory;
        }

        public TDerivedType GetInstance<TDerivedType>(TKey key) where TDerivedType : TBaseType
        {
            if (_instances.ContainsKey(key))
                return (TDerivedType)_instances[key]!;

            TDerivedType instance = Activator.CreateInstance<TDerivedType>();
            _instances.Add(key, instance);

            return instance;
        }
    }
}