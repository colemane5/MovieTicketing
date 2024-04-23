using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.ViewModels
{
    public interface IMovieSelectable
    {
        void SelectMovie(Movie movie);
    }
}
