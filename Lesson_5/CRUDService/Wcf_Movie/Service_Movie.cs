using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ObjectManager.Model;
using ObjectsManager.Interfaces;
using ObjectsManager.LiteDB;

namespace Wcf_Movie
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service_Movie :  IService_Movie
    {
        private readonly IMovieRepository _movieRepository;

        public Service_Movie()
        {
            _movieRepository = new MovieRepository();
        }

        public int AddMovie(Movie movie)
        {
            return _movieRepository.Add(movie);
        }

        public bool DeleteMovie(int id)
        {
            return _movieRepository.Delete(id);
        }

        public List<Movie> GetAllMovies()
        {
            return _movieRepository.GetAll();
        }

        public Movie GetMovie(int id)
        {
            return _movieRepository.Get(id);
        }

        public Movie UpdateMovie(Movie movie)
        {
            return _movieRepository.Update(movie);
        }
    }
}
