using System;
using AutoMapper;
using AutoMapper.Configuration;
using THS.Workshop.Infrastructure.DataAccess.EntityModel;
using THS.Workshop.Infrastructure.DomainModel.Member;

namespace THS.Workshop.Infrastructure
{
    class MemberMapper
    {
        private static readonly Lazy<IMapper> s_mapperLazy = new Lazy<IMapper>(CreateMapper);
        private static          IMapper       s_mapper;

        private static IMapper Mapper => s_mapperLazy.Value;

        public static T Map<T>(object source)
        {
            return Mapper.Map<T>(source);
        }

        public static U Map<T, U>(T source, U target)
        {
            return Mapper.Map(source, target);
        }
        private static IMapper CreateMapper()
        {
            if (s_mapper !=null)
            {
                return s_mapper;
            }

            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<InsertRequest, Member>();
            cfg.CreateMap<UpdateRequest, Member>();

            var mapperConfig = new MapperConfiguration(cfg);
            s_mapper = new Mapper(mapperConfig);

            return s_mapper;
        }
    }
}
