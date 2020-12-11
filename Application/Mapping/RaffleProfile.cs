using Application.Actions.RaffleActions;
using Application.Dto;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    class RaffleProfile : Profile
    {
        public RaffleProfile(){
            CreateMap<CreateRaffleCommand, Raffle>();
            CreateMap<Raffle, RaffleQuery>();
            CreateMap<CreateBetCommand, BetAttemptStart>();
            CreateMap<BetAttemptResult, BetAttemptResultQuery>();
            CreateMap<RaffleResult, RaffleResultQuery>();
            CreateMap<RaffleResultsWrapper, RaffleResultsQueryWrapper>();

        }
    }
}
