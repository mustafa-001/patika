using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.DirectorOperations.CreateDirector
{
    public class CreateDirectorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateDirectorModel Model;

        public CreateDirectorCommand(IMovieStoreDbContext dbContext, IMapper mapper, CreateDirectorModel model)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            Model = model;
        }

        public void Handle()
        {
            if (_dbContext.Directors.SingleOrDefault(m => m.Name == Model.Name) is not null)
            {
                throw new InvalidProgramException("Director with same name already exists.");
            }
            var Director = _mapper.Map<Director>(Model);
            _dbContext.Directors.Add(Director);
            _dbContext.SaveChanges();
        }
    }

    public class CreateDirectorModel
    {
        public string Name {get; set;} = String.Empty;
        public string Surname  {get; set;} = String.Empty;
        public  List<int> MovieIDs  {get; set;} = new List<int>();
        public string BirthDate {get; set;}  = String.Empty;
    }
}