using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class UserTheme : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SidebarButtonColor { get; set; }
        public string SidebarMode { get; set; }
        public string Mode { get; set; }
    }
}
