using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.ActorOperations.CreateActor
{
    public class CreateActorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateActorModel Model;

        public CreateActorCommand(IMovieStoreDbContext dbContext, IMapper mapper, CreateActorModel model)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            Model = model;
        }

        public void Handle()
        {
            if (_dbContext.Actors.SingleOrDefault(m => m.Name == Model.Name) is not null)
            {
                throw new InvalidProgramException("Actor with same name already exists.");
            }
            var Actor = _mapper.Map<Actor>(Model);
            _dbContext.Actors.Add(Actor);
            _dbContext.SaveChanges();
        }
    }

    public class CreateActorModel
    {
        public string Name = String.Empty;
        public string Surname = String.Empty;
        public  List<int> MovieIDs = new List<int>();
        public string BirthDate = String.Empty;
    }
}