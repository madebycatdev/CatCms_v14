using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Akbati.Models
{
    public class CinemaximumModel
    {
    }

    public class LoginModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    public class GetFilms
    {
        public int TotalCount { get; set; }
        public List<Film> Films { get; set; }
    }


    public class Film
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Synopsis { get; set; }
        public string ImageUrl { get; set; }
        public string DetailUrl { get; set; }
        public string FilmGenreId { get; set; }
        public string FirstAvailableSessionDate { get; set; }
        public string OpeningDate { get; set; }
        public List<Trailer> Trailers { get; set; }
        public List<FilmRating> FilmRatings { get; set; }
        public List<FilmAttribute> FilmAttributes { get; set; }
        public List<Cinema> Cinemas { get; set; }

    }

    public class Trailer
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string FilmId { get; set; }
    }

    public class FilmRating
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconCls { get; set; }
    }

    public class FilmAttribute
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Id { get; set; }
        public string IconCls { get; set; }
    }

    public class SessionAttribute
    {
        public string Name { get; set; }
        public string Tooltip { get; set; }
        public string Slug { get; set; }

    }

    public class Session
    {
        public string CinemaId { get; set; }
        public string SessionId { get; set; }
        public string DateTime { get; set; }
        public string Time { get; set; }
        public string IsNonStop { get; set; }
        public List<SessionAttribute> SessionAttributes { get; set; }
    }

    public class Screen
    {
        public string ScreenName { get; set; }
        public string ScreenNumber { get; set; }
        public List<Session> Sessions { get; set; }
    }

    public class Cinema
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string VistaCinemaId { get; set; }
        public string FilmIds { get; set; }
        public List<Screen> Screens { get; set; }
        
    }

    public class FilmContent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Synopsis { get; set; }
        public string ImageUrl { get; set; }
        public string DetailUrl { get; set; }
        public List<Trailer> Trailers { get; set; }
        public List<Session> Sessions { get; set; }
        public List<string> Types { get; set; }
    }
}