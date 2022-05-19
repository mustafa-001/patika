using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.MovieOperations.CreateMovie
{
    public class CreateMovieCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateMovieModel Model;

        public CreateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper, CreateMovieModel model)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            Model = model;
        }

        public void Handle()
        {
            if (_dbContext.Movies.SingleOrDefault(m => m.Name == Model.Name) is not null)
            {
                throw new InvalidProgramException("Movie with same name already exists.");
            }
            var movie = _mapper.Map<Movie>(Model);
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
        }
    }

    public class CreateMovieModel
    {
        public string Name {get; set;} = String.Empty;
        public string Date {get; set;} = String.Empty;
        public string Genre {get; set;} = String.Empty;
        public int DirectorId {get; set;} = 0;
        public  List<int> Actors {get; set;} = new List<int>();
        public string Price {get; set;}  = 0.ToString();
    }
}