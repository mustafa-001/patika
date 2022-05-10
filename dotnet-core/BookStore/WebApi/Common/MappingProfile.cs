using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi;

    namespace WebApi.Common
    {
        public class MappingProfile : Profile
        {
            public MappingProfile(){
                CreateMap<CreateBookModel, Book>();

                CreateMap<Book, GetBookByIdModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((Genre)src.GenreId).ToString()));
                CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((Genre)src.GenreId).ToString()));
            }
        }

    }