using PagedList.Core;

namespace cemetery.Models
{
    public class DeedViewModel {

        public StaticPagedList<Deed> Deeds { get; set; }

        // for select list
        public List<Cemetery> Cemeteries { get; set; }

        // for section list
        public List<Section> Sections { get; set; }
    }
}