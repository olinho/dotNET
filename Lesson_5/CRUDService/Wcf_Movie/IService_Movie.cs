using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using System.ServiceModel;
using ObjectManager.Model;

namespace Wcf_Movie
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService_Movie
    {
        [OperationContract]
        int AddMovie(Movie movie);

        [OperationContract]
        Movie GetMovie(int id);

        [OperationContract]
        Movie UpdateMovie(Movie movie);

        [OperationContract]
        List<Movie> GetAllMovies();

        [OperationContract]
        bool DeleteMovie(int id);

    }
}
