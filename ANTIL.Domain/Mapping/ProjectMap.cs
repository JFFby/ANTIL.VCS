using ANTIL.Domain.Core.Entities;
using FluentNHibernate.Mapping;

namespace ANTIL.Domain.Mapping
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Table("Projects");
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Owner).Column("UserId");
            HasMany(x => x.Commits).Inverse().Cascade.All();
        }
    }
}
