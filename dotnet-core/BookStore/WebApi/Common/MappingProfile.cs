using AutoMapper;
using WebApi.Entities;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.UserOperations.Commands.CreateUser;

namespace WebApi.Common
    {
        public class MappingProfile : Profile
        {
            public MappingProfile(){
                CreateMap<CreateBookModel, Book>();

                CreateMap<Book, GetBookByIdModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => (src.Genre.Name))).ForMember(d => d.Author, opt => opt.MapFrom(src => src.Author.Name));
                CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => (src.Genre.Name))).ForMember(d => d.Author, opt => opt.MapFrom(src => src.Author.Name));
                CreateMap<UpdateBookModel, Book>();
                CreateMap<Genre, GenresViewModel>();
                CreateMap<Genre, GenreDetailViewModel>();
                CreateMap<Genre, UpdateGenreModel>();
                CreateMap<Author, AuthorsViewModel>();
                CreateMap<Author, AuthorDetailViewModel>();
                CreateMap<Author, UpdateAuthorModel>();
                CreateMap<CreateUserModel, User>();
            }
        }

    }