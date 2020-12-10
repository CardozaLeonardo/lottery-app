using Application.Actions.RaffleActions;
using AutoMapper;
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
        }
    }
}
