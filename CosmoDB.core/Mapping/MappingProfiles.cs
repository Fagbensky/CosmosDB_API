using AutoMapper;
using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Mapping
{
    public class MappingProfiles: AutoMapper.Profile
    {

        public MappingProfiles()
        {
            CreateMap<ProgramModel, ProgramDTO>().ReverseMap();

            CreateMap<UploadQuestionModel, QuestionDTO>().ReverseMap();

            CreateMap<UpdateQuestionModel, QuestionDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.QuestionId))
                .ReverseMap()
                .ForMember(dest => dest.QuestionId, src => src.MapFrom(src => src.Id));

            CreateMap<Choice, choice>().ReverseMap();

            CreateMap<UploadStageModel, StageDTO>().ReverseMap();

            CreateMap<UpdateStageModel, StageDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.StageId))
                .ReverseMap()
                .ForMember(dest => dest.StageId, src => src.MapFrom(src => src.Id));

            CreateMap<VideoQuestionModel, VideoQuestionDTO>().ReverseMap();

            CreateMap<UploadVideoQuestionsModel, VideoQuestionDTO>().ReverseMap();

            CreateMap<UpdateVideoQuestionModel, VideoQuestionDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.VideoQuestionId))
                .ReverseMap()
                .ForMember(dest => dest.VideoQuestionId, src => src.MapFrom(src => src.Id));
        }
    }
}
