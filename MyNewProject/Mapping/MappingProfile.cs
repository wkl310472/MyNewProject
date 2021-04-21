﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyNewProject.Models;
using MyNewProject.Controllers.Resources;

namespace MyNewProject.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Game, SaveGameResource>()
                .ForMember(gr => gr.Genres, opt => opt.MapFrom(g => g.Genres.Select(ge => ge.GenreId)))
                .ForMember(gr => gr.Platforms, opt => opt.MapFrom(g => g.Platforms.Select(p => p.PlatformId)));
            CreateMap<Game, GameResource>()
                .ForMember(gr => gr.Genres, opt => opt.MapFrom(g => g.Genres.Select(ge => new KeyValuePairResource { Id = ge.Genre.Id, Name = ge.Genre.Name })))
                .ForMember(gr => gr.Platforms, opt => opt.MapFrom(g => g.Platforms.Select(p => new KeyValuePairResource { Id = p.Platform.Id, Name = p.Platform.Name })));
            CreateMap<Genre, GenreResource>();
            CreateMap<Genre, KeyValuePairResource>();
            CreateMap<Platform, PlatformResource>();
            CreateMap<Platform, KeyValuePairResource>();

            CreateMap<SaveGameResource, Game>()
                .ForMember(g => g.Id, opt => opt.Ignore())
                .ForMember(g => g.Genres, opt => opt.Ignore())
                .ForMember(g => g.Platforms, opt => opt.Ignore())
                .AfterMap((gr,g) => {

                    var removedGenres = g.Genres
                        .Where(g => !gr.Genres.Contains(g.GenreId))
                        .ToList();
                    foreach (var item in removedGenres)
                    {
                        g.Genres.Remove(item);
                    }

                    var addedGenres = gr.Genres
                        .Where(id => !g.Genres.Any(g => g.GenreId == id))
                        .Select(id => new GameGenre { GenreId = id })
                        .ToList();
                    foreach (var item in addedGenres)
                    {
                        g.Genres.Add(item);
                    }

                    var removedPlatforms = g.Platforms
                        .Where(g => !gr.Platforms.Contains(g.PlatformId))
                        .ToList();
                    foreach (var item in removedPlatforms)
                    {
                        g.Platforms.Remove(item);
                    }

                    var addedPlatforms = gr.Platforms
                        .Where(id => !g.Platforms.Any(g => g.PlatformId == id))
                        .Select(id => new GamePlatform { PlatformId = id })
                        .ToList();
                    foreach (var item in addedPlatforms)
                    {
                        g.Platforms.Add(item);
                    }
                });
            CreateMap<GenreResource, Genre>();
            CreateMap<PlatformResource, Platform>();

        }
    }
}
