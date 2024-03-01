using AutoMapper;
using DCMDNIs.Shared.Models;
using System.Drawing;
using DCMDNIs.Server.Models;

namespace DCMDNIs.Server.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region DNI
            CreateMap<Dni, DniDTO>();

            CreateMap<DniDTO, Dni>();
            #endregion

        }

    }
}
