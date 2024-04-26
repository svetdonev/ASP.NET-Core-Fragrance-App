using AutoMapper;
using Fragrance_Web_App.Data.Models;
using Fragrance_Web_App.Models;

namespace Fragrance_Web_App.Mapping
{
    public class FragranceMappingProfile : Profile
    {
        public FragranceMappingProfile()
        {
            this.CreateMap<FragranceCreateRequest, Fragrance>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FragranceNotes, opt => opt.Ignore())
                .AfterMap((source, dest) =>
                {
                    foreach (var noteId in source.NoteIds)
                    {
                        var fragranceNote = new FragranceNote
                        {
                            NoteId = noteId,
                        };

                        dest.FragranceNotes.Add(fragranceNote);
                    }
                });

            this.CreateMap<CategoryDto, Category>();
            this.CreateMap<Note, NoteDto>();
        }
    }
}